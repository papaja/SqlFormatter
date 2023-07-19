using Microsoft.AspNetCore.Mvc;
using SqlFormatter.Model;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace SqlFormatter.Controllers;
[ApiController]
[Route("[controller]")]
public class SqlFormatterController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public SqlFormatterController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    [HttpPost]
    public async Task<string> Format(Query query)
    {
        SyntaxTree? syntaxTree = null;
        List<TokenList> tokens = new();

        syntaxTree = await GetSyntaxTree(query.Sql);
        if (syntaxTree is null) return "Error while parsing SQL";

        foreach (var node in syntaxTree.Value.Nodes)
        {
            var tokenList = await GetTokenList(query.Sql.Substring(node.Start, node.End - node.Start));
            if (tokenList is not null)
                tokens.Add(tokenList);
        }

        StringBuilder sb = new StringBuilder();
        foreach (var tokenList in tokens)
        {
            if (sb.Length > 0)
                sb.AppendLine();

            sb.Append(Formatter.Format(tokenList.Value.Tokens, query.Options));
        }

        return sb.ToString();
    }

    private async Task<SyntaxTree?> GetSyntaxTree(string sql)
    {
        var sqlJson = new StringContent(
            $$"""{"Dialect": "sqlserver", "Script": "{{sql}}" }""",
            Encoding.UTF8,
            Application.Json);

        var syntaxResponseMessage = await _httpClient.PostAsync("https://sqlparsedev.azurewebsites.net/api/AbstractSyntaxTree", sqlJson);


        return await syntaxResponseMessage.Content.ReadFromJsonAsync<SyntaxTree>();
    }

    private async Task<TokenList?> GetTokenList(string sql)
    {
        var sqlJson = new StringContent(
            $$"""{"Dialect": "sqlserver", "Script": "{{sql}}" }""",
            Encoding.UTF8,
            Application.Json);
        var tokensResponseMessage = await _httpClient.PostAsync("https://sqlparsedev.azurewebsites.net/api/TokenList", sqlJson);


        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters =
            {
                new TokenTypeEnumConverter()
            }
        };

        var tokenListString = await tokensResponseMessage.Content.ReadAsStringAsync();
        var tokenList = await tokensResponseMessage.Content.ReadFromJsonAsync<TokenList>(options);

        if (tokenList?.Value?.Tokens is not null)
            tokenList.Value.Tokens = tokenList.Value.Tokens.Where(t => t.Type != TokenType.Whitespace).ToList();

        var temp = JsonSerializer.Serialize(tokenList, options);
        return tokenList;
    }
}
