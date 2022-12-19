using System.Xml.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;

var filename = "GarenBT.xml";
var currentDir = Directory.GetCurrentDirectory();
var filepath = Path.Combine(currentDir, filename);

XElement file = XElement.Load(filepath);

Debug.Assert(file.Name == "BehaviorTrees");
var BehaviorTrees = new List<BehaviorTree>();
foreach(var node in file.Elements("BehaviorTree"))
{
    BehaviorTrees.Add(BehaviorTree.Parse(node));
}

Console.WriteLine(BehaviorTrees.Find(tree => tree.Name == "GarenInit").ToCSharp());

public static class StringExtensions
{
    public static string Indent(this string str, int count = 4)
    {
        return Regex.Replace(str, @"^", "".PadRight(count), RegexOptions.Multiline);
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
    public BehaviorTreeNodeParameter? OutParameter;
    public List<BehaviorTreeNode> Children = new();

    public static BehaviorTreeNode Parse(XElement node)
    {
        var parameters = node.Element("Parameters");
        var outParameter = parameters?.Element("OutParameter");
        return new BehaviorTreeNode()
        {
            Type = node.Attribute("Type")?.Value,
            Name = node.Attribute("Name")?.Value,
            Parameters = parameters?.Elements("Parameter").Select(
                Node => BehaviorTreeNodeParameter.Parse(Node)
            ).ToList() ?? new(),
            OutParameter = (outParameter != null) ? BehaviorTreeNodeParameter.Parse(
                outParameter
            ) : null,
            Children = node.Element("Children")?.Elements("Node").Select(
                Node => Parse(Node)
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
            return "(\n" + ("true," + Children[0].ToCSharp()).Indent() + "\n)";
        }
        else
        {
            string ret = $"await {Type}(" +
                string.Join(", ", Parameters.Select(param => param.ToCSharp(false)));
            if(OutParameter != null)
            {
                if(Parameters.Count > 0)
                {
                    ret += ", ";
                }
                ret += OutParameter.ToCSharp(true);
            }
            ret += ")";
            return ret;
        }
    }
}

class BehaviorTreeNodeParameter
{
    public string? Name;
    public string? Value;
    public string? Scope; // "Tree" | "Global"
    public string? VariableType; // "Value" | "Reference"
    public string? ReferenceType; // "AttackableUnit" | "AttackableUnitCollection" | "UnitType" | "TeamEnum" | "Bool" | "Int" | "Float" | "Vector"

    public static BehaviorTreeNodeParameter Parse(XElement node)
    {
        return new BehaviorTreeNodeParameter()
        {
            Name = node.Attribute("Name")?.Value,
            Value = node.Attribute("Value")?.Value,
            Scope = node.Attribute("Scope")?.Value,
            VariableType = node.Attribute("VariableType")?.Value,
            ReferenceType = node.Attribute("ReferenceType")?.Value,
        };
    }

    public string ToCSharp(bool outParam)
    {
        string ret;
        if(outParam)
        {
            ret = $"out {Scope}.{Value}";
        }
        else if(VariableType == "Reference")
        {
            ret = $"{Scope}.{Value}";
        }
        else
        {
            ret = $"{Value}";
        }
        return $"{Name}: " + ret;
    }
}