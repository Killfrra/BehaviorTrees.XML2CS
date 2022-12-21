using System.Xml.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;

var currentDir = Directory.GetCurrentDirectory();
{
    var file = XElement.Load(Path.Combine(currentDir, "SBL.xml"));
    Debug.Assert(file.Name == "BlockDefinitions");
    var BD = new BlockDefinitions(file);
    Console.WriteLine(BD.ToCSharp());
}
{
    var file = XElement.Load(Path.Combine(currentDir, "GarenBT.xml"));
    Debug.Assert(file.Name == "BehaviorTrees");
    var BT = new BehaviorTrees(file);
    Console.WriteLine(BT.ToCSharp());
}

class BlockDefinitions
{
    public List<BlockDefinition> Definitions = new();
    public BlockDefinitions(XElement file)
    {
        Definitions = file.Elements("Block").Select(
            block => new BlockDefinition(block)
        ).ToList();
    }
    public string ToCSharp()
    {
        return
        "using static SBL;\n" +
        "public static class SBL\n" +
        "{\n" +
            (string.Join("\n", Definitions.Select(
                block => block.ToCSharp()
            ))).Indent() +
        "\n}";
    }
}

class BlockDefinition
{
    public string? Name;
    public string? Category;
    public string? SubCategory = "";
    public bool CanHaveChildren = false;
    public int NumberOfChildren = -1;
    public string? Description;
    public string? Comments;
    public List<BlockDefinitionParam> Parameters = new();
    public List<BlockDefinitionParam> OutParameters = new();
    public BlockDefinition(XElement block)
    {        
        Name = block.Attribute("Name")?.Value;
        Category = block.Attribute("Category")?.Value;
        SubCategory = block.Attribute("SubCategory")?.Value;
        CanHaveChildren = bool.Parse(block.Attribute("CanHaveChildren").Value);
        NumberOfChildren = int.Parse(block.Attribute("NumberOfChildren").Value);
        Description = block.Element("Description")?.Value;
        Comments = block.Element("Comments")?.Value;

        var parametersElement = block.Element("Parameters");
        Parameters = parametersElement?.Elements("Parameter").Select(
            param => new BlockDefinitionParam(param)
        ).ToList() ?? new();
        OutParameters = parametersElement?.Elements("OutParameter").Select(
            param => new BlockDefinitionParam(param)
        ).ToList() ?? new();
    }

    public string ToCSharp()
    {
        var ret = "";
        ret += $"/// <summary>\n";
        ret += $"/// {Description}\n";
        ret += $"/// </summary>\n";
        if(
            Description?.ToLower().Replace(" ", "") !=
            Comments?.ToLower().Replace(" ", "")
        ){
            ret += $"/// <remarks>\n";
            ret += $"/// {Comments}\n";
            ret += $"/// </remarks>\n";
        }
        foreach(var param in Parameters)
        {
            ret += $"/// <param name=\"{param.Name}\">{param.Description}</param>\n";
        }
        ret += $"public extern static Task<bool> {Name}(" +
            string.Join(", ", Parameters.Select(param => param.ToCSharp())) +
        ");";
        return ret;
    }
}

class BlockDefinitionParam
{
    public bool Out;
    public string? Name;
    public string? Type;
    public string? Default = "";
    public string? VariableType;
    public string? Description;
    public BlockDefinitionParam(XElement node)
    {
        Out = node.Name == "OutParameter";
        Name = node.Attribute("Name")?.Value;
        Type = node.Attribute("Type")?.Value;
        Default = node.Attribute("Default")?.Value;
        VariableType = node.Attribute("VariableType")?.Value;
        Description = node.Element("Description")?.Value;
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
    public BehaviorTrees(XElement file)
    {
        Trees = file.Elements("BehaviorTree").Select(
            node => new BehaviorTree(this, node)
        ).ToList();
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
    bool? isAsync = null;
    public bool IsAsync => isAsync ?? (bool)(isAsync = Root.IsAsync);
    public BehaviorTree(BehaviorTrees BTs, XElement node)
    {
        Name = node.Attribute("Name")?.Value;

        var rootElement = node.Element("Node");
        if(rootElement != null)
            Root = new BehaviorTreeNode(BTs, rootElement);
    }
    public string? ToCSharp()
    {
        return $"{(IsAsync ? "async Task<bool>" : "bool")} {Name}()\n" + "{\n" +
            ("return\n" + Root.ToCSharp() + ";").Indent() +
        "\n}";
    }
}

class BehaviorTreeNode
{
    public string? Type;
    public string? Name;
    public List<BehaviorTreeNodeParameter> Parameters = new();
    public List<BehaviorTreeNodeParameter> OutParameters = new();
    public List<BehaviorTreeNode> Children = new();
    bool? isAsync = null;
    public bool IsAsync =>
        isAsync ?? (bool)(isAsync = 
            Type is "DebugAction" or "DelayNSecondsBlocking" or "PanCameraFromCurrentPositionToPoint" or "PlayVOAudioEvent" ||
            (TrySubTree(out var subTreeIsAsync, out _) && subTreeIsAsync) ||
            Children.Find(child => child.IsAsync) != null);
    public WeakReference<BehaviorTrees> BehaviorTrees;
    public BehaviorTreeNode(BehaviorTrees BTs, XElement node)
    {
        BehaviorTrees = new(BTs);
        Type = node.Attribute("Type")?.Value;
        Name = node.Attribute("Name")?.Value;
        
        var parametersElement = node.Element("Parameters");
        Parameters = parametersElement?.Elements("Parameter").Select(
            node => new BehaviorTreeNodeParameter(node)
        ).ToList() ?? new();
        OutParameters = parametersElement?.Elements("OutParameter").Select(
            node => new BehaviorTreeNodeParameter(node)
        ).ToList() ?? new();

        Children = node.Element("Children")?.Elements("Node").Select(
            node => new BehaviorTreeNode(BTs, node)
        ).ToList() ?? new();
    }
    bool TrySubTree(out bool async, out string name)
    {
        if(Type == "SubTree")
        {
            var treeName = Parameters.Find(param => param.Name == "TreeName").Value!;
            BehaviorTrees.TryGetTarget(out var behaviorTrees);
            Debug.Assert(behaviorTrees != null);
            var bt = behaviorTrees.Trees.Find(tree => tree.Name == treeName);
            Debug.Assert(bt != null);
            async = bt.IsAsync;
            name = treeName;
            return true;
        }
        else
        {
            async = false;
            name = "";
            return false;
        }
    }
    public string ToCSharp()
    {
        if(Type == "Sequence")
        {
            return "(\n" + string.Join(" &&\n", Children.Select(node => node.ToCSharp())).Indent() + "\n)";
        }
        else if(Type == "Selector")
        {
            return "(\n" + string.Join(" ||\n", Children.Select(node => node.ToCSharp())).Indent() + "\n)";
        }
        else if(TrySubTree(out var isAsync, out var treeName))
        {
            var ret = "";
            if(isAsync) ret += "await ";
            ret += treeName + "()";
            return ret;
        }
        else if(Type == "MaskFailure")
        {
            Debug.Assert(Children.Count == 1);
            return "(" + Children[0].ToCSharp() + ", true)";
        }
        else
        {
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
                var ret = input;
                
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
                var ret = $"{left} {op} {right}";
                
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
                var ret = "";
                if(invert) ret += "!";
                if(IsAsync) ret += "await ";
                ret += Type + "(" +
                    string.Join(", ", filtered.Concat(OutParameters).Select(param => param.ToCSharp(true, true))) +
                ")";
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

    public BehaviorTreeNodeParameter(XElement node)
    {
        Out = node.Name == "OutParameter";
        Name = node.Attribute("Name")?.Value;
        Value = node.Attribute("Value")?.Value;
        Scope = node.Attribute("Scope")?.Value;
        VariableType = node.Attribute("VariableType")?.Value;
        ReferenceType = node.Attribute("ReferenceType")?.Value;
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