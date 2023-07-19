namespace SqlFormatter.Model;

public class Statistics
{
    public int ElapsedMicroseconds { get; set; }
    public int TotalLinesOfCode { get; set; }
    public int NonEmptyLinesOfCode { get; set; }
}

public class Value
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
    public List<Token> Tokens { get; set; }
    public Statistics Statistics { get; set; }
}
