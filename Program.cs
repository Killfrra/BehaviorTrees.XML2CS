using System.Xml.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;

var currentDir = Directory.GetCurrentDirectory();
/*
{
var file = XElement.Load(Path.Combine(currentDir, "SBL.xml"));
Debug.Assert(file.Name == "BlockDefinitions");
var ret = "";
foreach(var block in file.Elements("Block"))
{
    var name = block.Attribute("Name").Value;
    var category = block.Attribute("Category").Value;
    var subCategory = block.Attribute("SubCategory").Value;
    var canHaveChildren = bool.Parse(block.Attribute("CanHaveChildren").Value);
    var numberOfChildren = int.Parse(block.Attribute("NumberOfChildren").Value);
    var description = block.Element("Description").Value;
    var comments = block.Element("Comments").Value;
    var parametersElement = block.Element("Parameters");
    var inpParams = parametersElement.Elements("Parameter").Select(param => BlockDefinitionParam.Parse(param)).ToList();
    var outParams = parametersElement.Elements("OutParameter").Select(param => BlockDefinitionParam.Parse(param)).ToList();

    ret += $"/// <summary>\n";
    ret += $"/// {description}\n";
    ret += $"/// </summary>\n";
    ret += $"/// <remarks>\n";
    ret += $"/// {comments}\n";
    ret += $"/// </remarks>\n";
    foreach(var param in inpParams)
    {
        ret += $"/// <param name=\"{param.Name}\">{param.Description}</param>\n";
    }
    ret += $"public extern static Task<bool> {name}(" +
        string.Join(", ", inpParams.Select(param => param.ToCSharp())) +
    ");\n";
}
ret = "using static SBL;\npublic static class SBL\n" + "{\n" + ret.Indent() + "\n}";
Console.WriteLine(ret);
}
//*/

///*
{
var file = XElement.Load(Path.Combine(currentDir, "GarenBT.xml"));
Debug.Assert(file.Name == "BehaviorTrees");
var BT = BehaviorTrees.Parse(file);
Console.WriteLine(BT.ToCSharp());
}
//*/


class BlockDefinitionParam
{
    public bool Out;
    public string? Name;
    public string? Type;
    public string? Default = "";
    public string? VariableType;
    public string? Description;
    public static BlockDefinitionParam Parse(XElement node)
    {
        return new BlockDefinitionParam()
        {
            Out = node.Name == "OutParameter",
            Name = node.Attribute("Name")?.Value,
            Type = node.Attribute("Type")?.Value,
            Default = node.Attribute("Default")?.Value,
            VariableType = node.Attribute("VariableType")?.Value,
            Description = node.Element("Description")?.Value,
        };
    }

    public string ToCSharp()
    {
        var _type = Type;
        var _name = Name;
        var _def = Default;
        if(_type == "String")
        {
            _type = "string";
            _def = '"' + _def + '"';
        }
        else if(_type == "Int")
        {
            _type = "int";
        }
        else if(_type == "Float")
        {
            _type = "float";
        }
        else if(_type == "Bool")
        {
            _type = "bool";
            _def = _def?.ToLower();
        }
        else if(_type == "Vector")
        {
            _type = "Vector3";
        }
        return Out ? $"out {_type} {_name}" : $"{_type} {_name} = {_def}";
    }
}

public static class StringExtensions
{
    public static string Indent(this string str, int count = 4)
    {
        return Regex.Replace(str, @"^", "".PadRight(count), RegexOptions.Multiline);
    }
}

class BehaviorTrees
{
    public List<BehaviorTree> Trees = new();
    public static BehaviorTrees Parse(XElement file)
    {
        return new BehaviorTrees()
        {
            Trees = file.Elements("BehaviorTree").Select(node => BehaviorTree.Parse(node)).ToList()
        };
    }

    public string ToCSharp()
    {
        return $"class UnnamedBehaviourTree\n" + "{\n" + string.Join("\n\n", Trees.Select(tree => tree.ToCSharp())).Indent() + "\n}";
    }
}

class BehaviorTree
{
    public string? Name;
    public BehaviorTreeNode? Root;

    public static BehaviorTree Parse(XElement node)
    {
        var root = node.Element("Node");
        return new BehaviorTree()
        {
            Name = node.Attribute("Name")?.Value,
            Root = (root != null) ? BehaviorTreeNode.Parse(root) : null,
        };
    }

    public string? ToCSharp()
    {
        return $"async Task<bool> {Name}()\n" + "{\n" + ("return\n" + Root.ToCSharp() + ";").Indent() + "\n}";
    }
}

class BehaviorTreeNode
{
    public string? Type;
    public string? Name;
    public List<BehaviorTreeNodeParameter> Parameters = new();
    public List<BehaviorTreeNodeParameter> OutParameters = new();
    public List<BehaviorTreeNode> Children = new();

    public static BehaviorTreeNode Parse(XElement node)
    {
        var parametersElement = node.Element("Parameters");
        return new BehaviorTreeNode()
        {
            Type = node.Attribute("Type")?.Value,
            Name = node.Attribute("Name")?.Value,
            Parameters = parametersElement?.Elements("Parameter").Select(
                node => BehaviorTreeNodeParameter.Parse(node)
            ).ToList() ?? new(),
            OutParameters = parametersElement?.Elements("OutParameter").Select(
                node => BehaviorTreeNodeParameter.Parse(node)
            ).ToList() ?? new(),
            Children = node.Element("Children")?.Elements("Node").Select(
                node => Parse(node)
            ).ToList() ?? new(),
        };
    }

    public string ToCSharp()
    {
        if(Type == "Sequence")
        {
            return "(\n" + string.Join(" &&\n", Children.Select(node => node.ToCSharp())).Indent() + "\n)";
        }
        else if(Type == "Selector")
        {
            return "(\n" + string.Join("\n||\n", Children.Select(node => node.ToCSharp())).Indent() + "\n)";
        }
        else if(Type == "SubTree")
        {
            var treeName = Parameters.Find(param => param.Name == "TreeName").Value;
            return $"await {treeName}()";
        }
        else if(Type == "MaskFailure")
        {
            Debug.Assert(Children.Count == 1);
            return "(" + Children[0].ToCSharp() + ", true)";
        }
        else
        {
            var ret = "";
            bool invert = false;
            var filtered = Parameters.FindAll(param =>
            {
                var ret = param.Name == "ReturnSuccessIf";
                invert = invert || ret && param.Value == "false";
                return !ret;
            });
            if
            (
                Type is "SetVarBool" or "SetVarAttackableUnit" or "SetVarInt" or "SetVarDWORD" or "SetVarString" or "SetVarFloat" or "SetVarVector"
            )
            {
                var isStr = Type.Contains("String");
                var input = filtered.Find(param => param.Name == "Input")!.ToCSharp(false, isStr);
                ret = input;
                
                var param = OutParameters[0];
                return $"({param.Scope}.{param.Value} = {ret}, true)";
            }
            else if
            (
                Type is "EqualUnitTeam" or "NotEqualUnitTeam"
                    or "EqualBool" or "NotEqualBool"
                    or "EqualString" or "NotEqualString"
                    or "EqualInt" or "NotEqualInt" or "LessInt" or "LessEqualInt" or "GreaterInt" or "GreaterEqualInt"
                    or "AddInt" or "SubtractInt" or "MultiplyInt" or "DivideInt" or "ModulusInt"
                    or "EqualFloat" or "NotEqualFloat" or "LessFloat" or "LessEqualFloat" or "GreaterFloat" or "GreaterEqualFloat"
                    or "AddFloat" or "SubtractFloat" or "MultiplyFloat" or "DivideFloat"
                    or "EqualUnit" or "NotEqualUnit"
            )
            {
                var isStr = Type.Contains("String");
                var left = filtered.Find(param => param.Name == "LeftHandSide")!.ToCSharp(false, isStr);
                var right = filtered.Find(param => param.Name == "RightHandSide")!.ToCSharp(false, isStr);
                var op = "";

                if(Type.StartsWith("Add")) op += "+";
                else if(Type.StartsWith("Subtract")) op += "-";
                else if(Type.StartsWith("Multiply")) op += "*";
                else if(Type.StartsWith("Divide")) op += "/";
                else if(Type.StartsWith("Modulus")) op += "%";
                else
                {
                    var eq = Type.Contains("Equal");
                    if(Type.StartsWith("NotEqual")) op += "!";
                    else if(Type.Contains("Less")) op += "<";
                    else if(Type.Contains("Greater")) op += ">";
                    else if(eq) op += "=";
                    if(eq) op += "=";
                }
                ret = $"{left} {op} {right}";
                
                if(OutParameters.Count >= 1)
                {
                    var param = OutParameters[0];
                    return $"({param.Scope}.{param.Value} = {ret}, true)";
                }
                else
                    return ret;
            }
            else
            {
                ret = $"await {Type}(" +
                    string.Join(", ", filtered.Concat(OutParameters).Select(param => param.ToCSharp(true, true))) +
                ")";
                if(invert)
                {
                    ret = $"!{ret}";
                }
                return ret;
            }
        }
    }
}

class BehaviorTreeNodeParameter
{
    public bool Out;
    public string? Name;
    public string? Value;
    public string? Scope; // "Tree" | "Global"
    public string? VariableType; // "Value" | "Reference"
    public string? ReferenceType; // "AttackableUnit" | "AttackableUnitCollection" | "UnitType" | "TeamEnum" | "Bool" | "Int" | "Float" | "Vector"

    public static BehaviorTreeNodeParameter Parse(XElement node)
    {
        return new BehaviorTreeNodeParameter()
        {
            Out = node.Name == "OutParameter",
            Name = node.Attribute("Name")?.Value,
            Value = node.Attribute("Value")?.Value,
            Scope = node.Attribute("Scope")?.Value,
            VariableType = node.Attribute("VariableType")?.Value,
            ReferenceType = node.Attribute("ReferenceType")?.Value,
        };
    }

    public string ToCSharp(bool includeName, bool isString)
    {
        string ret;
        if(Out)
        {
            ret = $"out {Scope}.{Value}";
        }
        else if(VariableType == "Reference")
        {
            ret = $"{Scope}.{Value}";
        }
        else
        {
            if(isString)
                ret = '"' + Value + '"';
            else
                ret = Value;
        }
        if(includeName)
            return $"{Name}: " + ret;
        else
            return ret;
    }
}