namespace SqlFormatter.Model;

public enum TokenType
{
    Literal,
    Whitespace,
    ReservedKeyword,
    Semicolon,
    Operator,
    Identifier,
    Comment,
    Comma,
    OpenParen,
    CloseParen,
}

public class Token
{
    public string Content { get; set; }

    public TokenType Type { get; set; }
    public int Start { get; set; }
    public int End { get; set; }
    public int Length => End - Start;
}

public class TokenValue : Value
{
    public List<Token> Tokens { get; set; }
}

public class TokenList
{
    public TokenValue Value { get; set; }
    public List<object> Formatters { get; set; }
    public List<object> ContentTypes { get; set; }
    public object DeclaredType { get; set; }
    public int StatusCode { get; set; }
}
