using System.Xml.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;

var currentDir = Directory.GetCurrentDirectory();
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
    var _params = block.Element("Parameters").Elements().Select(param => BlockDefinitionParam.Parse(param)).ToList();

    ret += $"/// <summary>\n";
    ret += $"/// {description}\n";
    ret += $"/// </summary>\n";
    ret += $"/// <remarks>\n";
    ret += $"/// {comments}\n";
    ret += $"/// </remarks>\n";
    foreach(var param in _params)
    {
        ret += $"/// <param name=\"{param.Name}\">{param.Description}</param>\n";
    }
    _params = _params.FindAll(param => param.Out).Concat(_params.FindAll(param => !param.Out)).ToList();
    ret += $"public extern static Task<bool> {name}(" +
        string.Join(", ", _params.Select(param => param.ToCSharp())) +
    ");\n";
    /*
    ret += "{\n";
    ret += "return true;".Indent();
    ret += "\n}";
    ret += "\n";
    */
}
ret = "using static SBL;\npublic static class SBL\n" + "{\n" + ret.Indent() + "\n}";
Console.WriteLine(ret);

///*
file = XElement.Load(Path.Combine(currentDir, "GarenBT.xml"));
Debug.Assert(file.Name == "BehaviorTrees");
var BT = BehaviorTrees.Parse(file);
Console.WriteLine(BT.ToCSharp());
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
    public List<BehaviorTreeNode> Children = new();

    public static BehaviorTreeNode Parse(XElement node)
    {
        return new BehaviorTreeNode()
        {
            Type = node.Attribute("Type")?.Value,
            Name = node.Attribute("Name")?.Value,
            Parameters = node.Element("Parameters")?.Elements().Select(
                node =>
                {
                    Debug.Assert(node.Name == "Parameter" || node.Name == "OutParameter");
                    return BehaviorTreeNodeParameter.Parse(node);
                }
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
            return "(true, " + Children[0].ToCSharp() + ")";
        }
        else
        {
            return $"await {Type}(" +
                string.Join(", ", Parameters.Select(param => param.ToCSharp())) +
            ")";
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

    public string ToCSharp()
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
            ret = '"' + Value + '"';
        }
        return $"{Name}: " + ret;
    }
}