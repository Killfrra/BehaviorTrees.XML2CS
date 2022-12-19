using System.Xml.Linq;
using System.Diagnostics;

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

Console.WriteLine();

class BehaviorTree
{
    public string? Name;
    public List<BehaviorTreeNode> Children = new();

    public static BehaviorTree Parse(XElement node)
    {
        return new BehaviorTree()
        {
            Name = node.Attribute("Name")?.Value,
            Children = node.Elements("Node").Select(
                Node => BehaviorTreeNode.Parse(Node)
            ).ToList(),
        };
    }

    public void Execute()
    {
        foreach(var node in Children)
        {
            node.Execute();
        }
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
            ) : new(),
            Children = node.Element("Children")?.Elements("Node").Select(
                Node => Parse(Node)
            ).ToList() ?? new(),
        };
    }

    public void Execute()
    {
        Console.WriteLine($"// {Name}\n{Type}()");
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
}