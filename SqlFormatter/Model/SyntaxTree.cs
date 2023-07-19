namespace SqlFormatter.Model;

public class Node
{
    public string Name { get; set; }
    public string Type { get; set; }
    public int Start { get; set; }
    public int End { get; set; }
    public List<Node> Children { get; set; }
    public string Value { get; set; }
}

public class TreeValue : Value
{
    public List<Node> Nodes { get; set; }
}

public class SyntaxTree
{
    public TreeValue Value { get; set; }
    public List<object> Formatters { get; set; }
    public List<object> ContentTypes { get; set; }
    public object DeclaredType { get; set; }
    public int StatusCode { get; set; }
}