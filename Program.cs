using System.Xml.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;

var currentDir = Directory.GetCurrentDirectory();

var file = XElement.Load(Path.Combine(currentDir, "SBL.xml"));
Debug.Assert(file.Name == "BlockDefinitions");
var BD = new BlockDefinitions(file);
Console.WriteLine(BD.ToCSharp());

    file = XElement.Load(Path.Combine(currentDir, "GarenBT.xml"));
Debug.Assert(file.Name == "BehaviorTrees");
var BT = new BehaviorTrees(BD, file);
Console.WriteLine(BT.ToCSharp());

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
        "using System.Numerics;\n" +
        "using static AllEnumMembers;\n" +
        "public static class SBL\n" +
        "{\n" +
            (string.Join("\n", Definitions.FindAll(
                block => !BehaviorTreeNode.Replacable.All.Contains(block.Name)
            ).Select(
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
    bool IsAsync => BehaviorTreeNode.AsyncBlockNames.Contains(Name);
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
        var filteredParameters = Parameters.FindAll(
            param => param.Name != "ReturnSuccessIf"
        );
        foreach(var param in filteredParameters)
        {
            ret += $"/// <param name=\"{param.Name}\">{param.Description}</param>\n";
        }
        var paramStrings = OutParameters.Concat(filteredParameters).Select(
            param => param.ToCSharp()
        );
        if(CanHaveChildren && NumberOfChildren > 0)
        {
            paramStrings = paramStrings.Concat(
                Enumerable.Range(0, NumberOfChildren)
                    .Select(i => $"Func<Task<bool>>? Child{i} = null")
            );
        }
        ret += $"public extern static {(IsAsync ? "Task<bool>" : "bool")} {Name}(" +
            string.Join(", ", paramStrings) +
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
        return VarDeclFromXMLtoCS(Out, Type, Name, Default);
    }

    //TODO: Move
    public static string VarDeclFromXMLtoCS(bool _out, string type, string name, string? def)
    {
        if(type == "String")
        {
            type = "string";
            def = '"' + def + '"';
        }
        else if(type == "Int")
        {
            type = "int";
        }
        else if(type == "UnsignedInt")
        {
            type = "uint";
        }
        else if(type == "Float")
        {
            type = "float";
        }
        else if(type == "Bool")
        {
            type = "bool";
            def = def?.ToLower();
        }
        else if(type == "Vector")
        {
            type = "Vector3";
            if(def != null)
            {
                var opts = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
                var t = def.Split(';', opts);
                def = (t[0] == "0" && t[1] == "0" && t[2] == "0") ?
                    "Vector3.Zero" :
                    $"new Vector3({t[0]}f, {t[1]}f, {t[2]}f)";
            }
        }
        else if(type == "AttackableUnitCollection")
        {
            type = "IEnumerable<AttackableUnit>";
        }
        else if(type == "Obj_AI_HeroCollection")
        {
            type = "IEnumerable<Champion>";
        }
        else if(type == "Obj_AI_TurretCollection")
        {
            type = "IEnumerable<Turret>";
        }
        else if(type == "TeamEnum")
        {
            type = "TeamID";
        }
        return _out ? $"out {type} {name}" : ($"{type} {name}" + ((def != null && def != "") ? $" = {def}" : ""));
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
    public BehaviorTrees(BlockDefinitions BDs, XElement file)
    {
        Trees = file.Elements("BehaviorTree").Select(
            node => new BehaviorTree(BDs, this, node)
        ).ToList();
    }

    public string ToCSharp()
    {
        Vars vars = new();
        foreach(var tree in Trees)
        {
            vars.MergeWith(tree.Root.GetVariables().Global);
        }
        return $"class UnnamedBehaviourTree\n" + "{\n" +
            (
                vars.ToCSharp() + "\n\n" +
                string.Join("\n\n", Trees.Select(
                    tree => tree.ToCSharp()
                ))
            ).Indent() +
        "\n}";
    }
}

class BehaviorTree
{
    public string? Name;
    public BehaviorTreeNode? Root;
    bool? isAsync = null;
    public bool IsAsync => isAsync ?? (bool)(isAsync = Root.IsAsync);
    public BehaviorTree(BlockDefinitions BDs, BehaviorTrees BTs, XElement node)
    {
        Name = node.Attribute("Name")?.Value;

        var rootElement = node.Element("Node");
        if(rootElement != null)
            Root = new BehaviorTreeNode(BDs, BTs, rootElement);
    }
    public string? ToCSharp()
    {
        return $"{(IsAsync ? "async Task<bool>" : "bool")} {Name}()\n" + "{\n" +
            (
                Root.GetVariables().Tree.ToCSharp() + "\n" +
                "return\n" + Root.ToCSharp() + ";"
            ).Indent() +
        "\n}";
    }
}

class Vars
{
    public Dictionary<string, string?> Dict = new();
    public void MergeWith(Vars vars)
    {
        foreach(var kv in vars.Dict)
        {
            Add(kv.Key, kv.Value);
        }
    }
    public void Add(string name, string? type)
    {
        string? existing;
        if(Dict.TryGetValue(name, out existing))
        {
            if(existing == null)
            {
                Dict[name] = type;
            }
            else if(existing != type && type != null)
            {
                Console.WriteLine($"Incompatible types {existing} and {type} for {name}");
            }
        }
        else
        {
            Dict[name] = type;
        }
    }
    public string ToCSharp()
    {
        return string.Join("\n", Dict.Select(kv => BlockDefinitionParam.VarDeclFromXMLtoCS(false, kv.Value, kv.Key, null) + ";"));
    }
}

class GlobalAndTreeVars
{
    public Vars Global = new();
    public Vars Tree = new();
    public void MergeWith(GlobalAndTreeVars vars)
    {
        Global.MergeWith(vars.Global);
        Tree.MergeWith(vars.Tree);
    }
    public void Add(string Scope, string name, string? type)
    {
        var scopeVars = Global;
        if(Scope == "Tree")
            scopeVars = Tree;
        scopeVars.Add(name, type);
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
    public static string[] AsyncBlockNames =
    {
        "MaskFailure", "DebugAction", "DelayNSecondsBlocking", "PanCameraFromCurrentPositionToPoint", "PlayVOAudioEvent"
    };
    public bool IsAsync =>
        isAsync ?? (bool)(isAsync = 
            AsyncBlockNames.Contains(Type) ||
            (TrySubTree(out var subTreeIsAsync, out _) && subTreeIsAsync) ||
            Children.Find(child => child.IsAsync) != null);
    public WeakReference<BehaviorTrees> BehaviorTrees;
    public WeakReference<BlockDefinitions> BlockDefinitions;
    public BehaviorTreeNode(BlockDefinitions BDs, BehaviorTrees BTs, XElement node)
    {
        BehaviorTrees = new(BTs);
        BlockDefinitions = new(BDs);

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
            node => new BehaviorTreeNode(BDs, BTs, node)
        ).ToList() ?? new();
    }
    bool TrySubTree(out bool async, out string name)
    {
        if(Type == Replacable.SubTree)
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
    public static ReplacableClass Replacable = new();
    public class ReplacableClass
    {
        public string Sequence = "Sequence";
        public string Selector = "Selector";
        public string SubTree = "SubTree";
        /*
        public string MaskFailure = "MaskFailure";
        public string[] Setters =
        {
            "SetVarBool",
            "SetVarAttackableUnit",
            "SetVarInt",
            "SetVarDWORD",
            "SetVarString",
            "SetVarFloat",
            "SetVarVector"
        };
        public string[] Comparers =
        {
            "EqualInt", "NotEqualInt", "LessInt", "LessEqualInt", "GreaterInt", "GreaterEqualInt",
            "AddInt", "SubtractInt", "MultiplyInt", "DivideInt", "ModulusInt",
            "EqualFloat", "NotEqualFloat", "LessFloat", "LessEqualFloat", "GreaterFloat", "GreaterEqualFloat",
            "AddFloat", "SubtractFloat", "MultiplyFloat", "DivideFloat",
            "EqualUnit", "NotEqualUnit",
            "EqualPARType", "NotEqualPARType",
            "EqualUnitType", "NotEqualUnitType",
            "EqualCreatureType", "NotEqualCreatureType",
            "EqualSpellbookType", "NotEqualSpellbookType",
            "EqualUnitTeam", "NotEqualUnitTeam",
            "EqualBool", "NotEqualBool",
            "EqualString", "NotEqualString",

        };
        */
        public List<string> All = new();
        public ReplacableClass()
        {
            All.AddRange(new string[]{ Sequence, Selector, SubTree, /*MaskFailure*/ });
            /*
            All.AddRange(Setters);
            All.AddRange(Comparers);
            */
        }
    }
    public string ToCSharp()
    {
        if(Type == Replacable.Sequence)
        {
            return "(\n" + string.Join(" &&\n", Children.Select(node => node.ToCSharp())).Indent() + "\n)";
        }
        else if(Type == Replacable.Selector)
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
        /*
        else if(Type == Replacable.MaskFailure)
        {
            Debug.Assert(Children.Count == 1);
            return "(" + Children[0].ToCSharp() + ", true)";
        }
        */
        else
        {
            bool invert = Parameters.Find(
                param => param.Name == "ReturnSuccessIf" && param.Value == "false"
            ) != null;
            /*
            if(Replacable.Setters.Contains(Type))
            {
                var isStr = Type.Contains("String");
                var input = Parameters.Find(param => param.Name == "Input")!.ToCSharp(false, isStr);
                var ret = input;
                
                var param = OutParameters[0];
                return $"({param.ScopeDotValue()} = {ret}, true)";
            }
            else if(Replacable.Comparers.Contains(Type))
            {
                var isStr = Type.Contains("String");
                var left = Parameters.Find(param => param.Name == "LeftHandSide")!.ToCSharp(false, isStr);
                var right = Parameters.Find(param => param.Name == "RightHandSide")!.ToCSharp(false, isStr);
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
                    return $"({param.ScopeDotValue()} = {ret}, true)";
                }
                else
                    return ret;
            }
            else
            */
            {
                BlockDefinitions.TryGetTarget(out var blockDefinitions);
                Debug.Assert(blockDefinitions != null);
                var bd = blockDefinitions.Definitions.Find(block => block.Name == Type);
                Debug.Assert(bd != null);

                bool includeParameterNames = true;
                List<(BehaviorTreeNodeParameter, bool)> allParametersSorted = new();
                var allParametersA = bd.OutParameters.Concat(bd.Parameters).ToList();
                var allParametersB = OutParameters.Concat(Parameters).ToList();
                if(allParametersA.Count == allParametersB.Count)
                {
                    includeParameterNames = false;
                    foreach(var paramA in allParametersA)
                    {
                        var corresp = allParametersB.Find(paramB => paramB.Name == paramA.Name);
                        if(corresp == null)
                        {
                            includeParameterNames = true;
                        }
                        else if(corresp.Name != "ReturnSuccessIf")
                        {
                            allParametersSorted.Add((corresp, paramA.Type == "String"));
                        }
                    }
                }

                var ret = "";
                if(invert) ret += "!";
                if(IsAsync) ret += "await ";
                ret += Type + "(" +
                    string.Join(", ", allParametersSorted.Select(
                        (tuple) => tuple.Item1.ToCSharp(includeParameterNames, tuple.Item2)
                    ).Concat(Children.Select(
                        child => $"async () => {child.ToCSharp()}"
                    )));
                ret += ")";
                return ret;
            }
        }
    }

    GlobalAndTreeVars? vars = null;
    public GlobalAndTreeVars GetVariables()
    {
        if(vars != null)
            return vars;
        vars = new();
        foreach(var param in Parameters)
        {
            if(param.VariableType == "Reference")
            {
                vars.Add(param.Scope, param.Value, param.ReferenceType);
            }
        }
        foreach(var param in OutParameters)
        {
            vars.Add(param.Scope, param.Value, null);
        }
        foreach(var child in Children)
        {
            vars.MergeWith(child.GetVariables());
        }
        return vars;
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

    public string ScopeDotValue()
    {
        var _scope = Scope;
        if(_scope == "Global")
            return $"this.{Value}";
        else //if(_scope == "Tree")
            return Value;
    }

    public string ToCSharp(bool includeName, bool isString)
    {
        string ret;
        if(Out)
        {
            ret = $"out {ScopeDotValue()}";
        }
        else if(VariableType == "Reference")
        {
            ret = $"{ScopeDotValue()}";
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