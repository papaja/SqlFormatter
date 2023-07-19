using SqlFormatter.Model;
using System.Text;

namespace SqlFormatter;

public class Formatter
{
    public static StringBuilder Format(List<Token> tokens, FormatOptions options, bool? shouldStartNewLine = false)
    {
        StringBuilder sb = new();
        bool shouldAppendWhiteSpace = false;

        Stack<int> offsetStack = new();
        offsetStack.Push(0);
        HashSet<string> offsetWords = new() { "select", "set" };
        int equalSignOffset = 0;

        for (int i = 0; i < tokens.Count; i++)
        {
            var token = tokens[i];

            if (token.Type == TokenType.ReservedKeyword)
            {
                if (token.Content == "from")
                    offsetStack.Pop();

                if (shouldStartNewLine == true)
                {
                    sb.AppendLine();
                    if (options.AlignIdentifiers && offsetStack.Peek() > 0)
                        sb.Append(' ', offsetStack.Peek() + (options.CommaFirst ? 0 : 1));
                }
                else
                {
                    if (shouldAppendWhiteSpace == true)
                        sb.Append(' ');
                }
                sb.Append(token.Content);

                shouldAppendWhiteSpace = true;
                shouldStartNewLine = true;

                if (offsetStack.Count == 1 && offsetWords.TryGetValue(token.Content, out string? _))
                {
                    offsetStack.Push(token.Length);
                }

                if (options.AlignEqualSign == true && token.Content == "set")
                {
                    var j = i + 1;
                    equalSignOffset = tokens[j++].Length + (options.AlignIdentifiers ? 0 : 4);

                    while (tokens[j].Type != TokenType.Semicolon && tokens[j].Type != TokenType.ReservedKeyword && j < tokens.Count - 1)
                    {
                        if (tokens[j].Type == TokenType.Identifier)
                            equalSignOffset = Math.Max(equalSignOffset, tokens[j].Length);
                        j++;
                    }
                }
                else
                {
                    equalSignOffset = 0;
                }
            }
            else if (token.Type == TokenType.Operator)
            {
                sb.Append(' ');

                if (token.Content == "=")
                {
                    int setOffset = 0;
                    if (!options.AlignIdentifiers && tokens[i - 2].Type == TokenType.ReservedKeyword)
                        setOffset = 4 - (options.CommaFirst ? 1 : 0);

                    sb.Append(' ', Math.Max(0, equalSignOffset - tokens[i - 1].Length - setOffset));
                }
                sb.Append(token.Content);
            }
            else if (token.Type == TokenType.Comma)
            {
                if (options.CommaFirst)
                {
                    sb.AppendLine();
                    if (options.AlignIdentifiers)
                        sb.Append(' ', offsetStack.Peek());
                    sb.Append(token.Content);
                }
                else
                {
                    sb.Append(token.Content);
                    sb.AppendLine();
                    if (options.AlignIdentifiers)
                        sb.Append(' ', offsetStack.Peek() + 1);
                }

                shouldAppendWhiteSpace = false;
            }
            else if (token.Type == TokenType.Semicolon)
            {
                sb.Append(token.Content);
            }
            else if (token.Type == TokenType.OpenParen)
            {
                // Assumption: if previous token is comma it is subquery, otherwise a function
                if (shouldAppendWhiteSpace == true && tokens[i - 1].Type == TokenType.Comma)
                    sb.Append(' ');

                sb.Append(token.Content);
                shouldAppendWhiteSpace = false;
                shouldStartNewLine = false;

                if (tokens[i - 1].Type == TokenType.Comma)
                    offsetStack.Push(offsetStack.Peek() + tokens[i + 1].Length + 2);
            }
            else if (token.Type == TokenType.CloseParen)
            {
                sb.Append(token.Content);
                shouldAppendWhiteSpace = true;
                shouldStartNewLine = false;
            }
            else
            {
                if (shouldAppendWhiteSpace == true)
                    sb.Append(' ');

                shouldAppendWhiteSpace = true;
                sb.Append(token.Content);
            }
        }

        return sb;
    }
}
