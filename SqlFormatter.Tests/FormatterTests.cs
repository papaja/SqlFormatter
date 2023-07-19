using SqlFormatter.Model;
using System.Text.Json;

namespace SqlFormatter.Tests;

public class FormatterTests
{
    private JsonSerializerOptions _jsonOptions;
    public FormatterTests()
    {
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters =
            {
                new TokenTypeEnumConverter()
            }
        };
    }
    
    [Fact]
    public void Select_WithoutOptions()
    {
        // sql: select a, b, c from x where a = y;

        TokenList? tokens = JsonSerializer.Deserialize<TokenList>("{\"Value\":{\"Tokens\":[{\"Content\":\"select\",\"Type\":\"ReservedKeyword\",\"Start\":0,\"End\":6,\"Length\":6},{\"Content\":\"a\",\"Type\":\"Identifier\",\"Start\":7,\"End\":8,\"Length\":1},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":8,\"End\":9,\"Length\":1},{\"Content\":\"b\",\"Type\":\"Identifier\",\"Start\":10,\"End\":11,\"Length\":1},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":11,\"End\":12,\"Length\":1},{\"Content\":\"c\",\"Type\":\"Identifier\",\"Start\":13,\"End\":14,\"Length\":1},{\"Content\":\"from\",\"Type\":\"ReservedKeyword\",\"Start\":15,\"End\":19,\"Length\":4},{\"Content\":\"x\",\"Type\":\"Identifier\",\"Start\":20,\"End\":21,\"Length\":1},{\"Content\":\"where\",\"Type\":\"ReservedKeyword\",\"Start\":22,\"End\":27,\"Length\":5},{\"Content\":\"a\",\"Type\":\"Identifier\",\"Start\":28,\"End\":29,\"Length\":1},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":30,\"End\":31,\"Length\":1},{\"Content\":\"y\",\"Type\":\"Identifier\",\"Start\":32,\"End\":33,\"Length\":1},{\"Content\":\";\",\"Type\":\"Semicolon\",\"Start\":33,\"End\":34,\"Length\":1}],\"Success\":true,\"ErrorMessage\":\"\",\"Statistics\":{\"ElapsedMicroseconds\":38,\"TotalLinesOfCode\":1,\"NonEmptyLinesOfCode\":1}},\"Formatters\":[],\"ContentTypes\":[],\"DeclaredType\":null,\"StatusCode\":200}", _jsonOptions);
        FormatOptions options = new();

        var actual = Formatter.Format(tokens.Value.Tokens, options).ToString();

        Assert.Equal("select a,\r\nb,\r\nc\r\nfrom x\r\nwhere a = y;", actual);
    }

    [Fact]
    public void Select_WithCommaFirst()
    {
        // sql: select a, b, c from x where a = y;

        TokenList? tokens = JsonSerializer.Deserialize<TokenList>("{\"Value\":{\"Tokens\":[{\"Content\":\"select\",\"Type\":\"ReservedKeyword\",\"Start\":0,\"End\":6,\"Length\":6},{\"Content\":\"a\",\"Type\":\"Identifier\",\"Start\":7,\"End\":8,\"Length\":1},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":8,\"End\":9,\"Length\":1},{\"Content\":\"b\",\"Type\":\"Identifier\",\"Start\":10,\"End\":11,\"Length\":1},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":11,\"End\":12,\"Length\":1},{\"Content\":\"c\",\"Type\":\"Identifier\",\"Start\":13,\"End\":14,\"Length\":1},{\"Content\":\"from\",\"Type\":\"ReservedKeyword\",\"Start\":15,\"End\":19,\"Length\":4},{\"Content\":\"x\",\"Type\":\"Identifier\",\"Start\":20,\"End\":21,\"Length\":1},{\"Content\":\"where\",\"Type\":\"ReservedKeyword\",\"Start\":22,\"End\":27,\"Length\":5},{\"Content\":\"a\",\"Type\":\"Identifier\",\"Start\":28,\"End\":29,\"Length\":1},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":30,\"End\":31,\"Length\":1},{\"Content\":\"y\",\"Type\":\"Identifier\",\"Start\":32,\"End\":33,\"Length\":1},{\"Content\":\";\",\"Type\":\"Semicolon\",\"Start\":33,\"End\":34,\"Length\":1}],\"Success\":true,\"ErrorMessage\":\"\",\"Statistics\":{\"ElapsedMicroseconds\":47,\"TotalLinesOfCode\":1,\"NonEmptyLinesOfCode\":1}},\"Formatters\":[],\"ContentTypes\":[],\"DeclaredType\":null,\"StatusCode\":200}\r\n", _jsonOptions);
        FormatOptions options = new () { CommaFirst = true };

        var actual = Formatter.Format(tokens.Value.Tokens, options).ToString();

        Assert.Equal("select a\r\n,b\r\n,c\r\nfrom x\r\nwhere a = y;", actual);
    }


    [Fact]
    public void Select_WithAlignIdentifiers()
    {
        // sql: select a, b, c from x where a = y;

        TokenList? tokens = JsonSerializer.Deserialize<TokenList>("{\"Value\":{\"Tokens\":[{\"Content\":\"select\",\"Type\":\"ReservedKeyword\",\"Start\":0,\"End\":6,\"Length\":6},{\"Content\":\"a\",\"Type\":\"Identifier\",\"Start\":7,\"End\":8,\"Length\":1},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":8,\"End\":9,\"Length\":1},{\"Content\":\"b\",\"Type\":\"Identifier\",\"Start\":10,\"End\":11,\"Length\":1},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":11,\"End\":12,\"Length\":1},{\"Content\":\"c\",\"Type\":\"Identifier\",\"Start\":13,\"End\":14,\"Length\":1},{\"Content\":\"from\",\"Type\":\"ReservedKeyword\",\"Start\":15,\"End\":19,\"Length\":4},{\"Content\":\"x\",\"Type\":\"Identifier\",\"Start\":20,\"End\":21,\"Length\":1},{\"Content\":\"where\",\"Type\":\"ReservedKeyword\",\"Start\":22,\"End\":27,\"Length\":5},{\"Content\":\"a\",\"Type\":\"Identifier\",\"Start\":28,\"End\":29,\"Length\":1},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":30,\"End\":31,\"Length\":1},{\"Content\":\"y\",\"Type\":\"Identifier\",\"Start\":32,\"End\":33,\"Length\":1},{\"Content\":\";\",\"Type\":\"Semicolon\",\"Start\":33,\"End\":34,\"Length\":1}],\"Success\":true,\"ErrorMessage\":\"\",\"Statistics\":{\"ElapsedMicroseconds\":49,\"TotalLinesOfCode\":1,\"NonEmptyLinesOfCode\":1}},\"Formatters\":[],\"ContentTypes\":[],\"DeclaredType\":null,\"StatusCode\":200}\r\n", _jsonOptions);
        FormatOptions options = new() { AlignIdentifiers = true };

        var actual = Formatter.Format(tokens.Value.Tokens, options).ToString();

        Assert.Equal("select a,\r\n       b,\r\n       c\r\nfrom x\r\nwhere a = y;", actual);
    }


    [Fact]
    public void Select_WithAlignIdentifiersAndCommaFirst()
    {
        // sql: select a, b, c from x where a = y;

        TokenList? tokens = JsonSerializer.Deserialize<TokenList>("{\"Value\":{\"Tokens\":[{\"Content\":\"select\",\"Type\":\"ReservedKeyword\",\"Start\":0,\"End\":6,\"Length\":6},{\"Content\":\"a\",\"Type\":\"Identifier\",\"Start\":7,\"End\":8,\"Length\":1},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":8,\"End\":9,\"Length\":1},{\"Content\":\"b\",\"Type\":\"Identifier\",\"Start\":10,\"End\":11,\"Length\":1},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":11,\"End\":12,\"Length\":1},{\"Content\":\"c\",\"Type\":\"Identifier\",\"Start\":13,\"End\":14,\"Length\":1},{\"Content\":\"from\",\"Type\":\"ReservedKeyword\",\"Start\":15,\"End\":19,\"Length\":4},{\"Content\":\"x\",\"Type\":\"Identifier\",\"Start\":20,\"End\":21,\"Length\":1},{\"Content\":\"where\",\"Type\":\"ReservedKeyword\",\"Start\":22,\"End\":27,\"Length\":5},{\"Content\":\"a\",\"Type\":\"Identifier\",\"Start\":28,\"End\":29,\"Length\":1},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":30,\"End\":31,\"Length\":1},{\"Content\":\"y\",\"Type\":\"Identifier\",\"Start\":32,\"End\":33,\"Length\":1},{\"Content\":\";\",\"Type\":\"Semicolon\",\"Start\":33,\"End\":34,\"Length\":1}],\"Success\":true,\"ErrorMessage\":\"\",\"Statistics\":{\"ElapsedMicroseconds\":167,\"TotalLinesOfCode\":1,\"NonEmptyLinesOfCode\":1}},\"Formatters\":[],\"ContentTypes\":[],\"DeclaredType\":null,\"StatusCode\":200}\r\n", _jsonOptions);
        FormatOptions options = new() { CommaFirst = true, AlignIdentifiers = true };

        var actual = Formatter.Format(tokens.Value.Tokens, options).ToString();

        Assert.Equal("select a\r\n      ,b\r\n      ,c\r\nfrom x\r\nwhere a = y;", actual);
    }

    [Fact]
    public void Select_WithFunctionAndNestedSelectsAndAlignIdentifiersAndCommaFirst()
    {
        // sql: select a, b, c, d, max(e), (select f, g, h, (select i, j from l where j = a) as m from n where g = a) as o from p where a = 'robert';

        TokenList? tokens = JsonSerializer.Deserialize<TokenList>("{\"Value\":{\"Tokens\":[{\"Content\":\"select\",\"Type\":\"ReservedKeyword\",\"Start\":0,\"End\":6,\"Length\":6},{\"Content\":\"a\",\"Type\":\"Identifier\",\"Start\":7,\"End\":8,\"Length\":1},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":8,\"End\":9,\"Length\":1},{\"Content\":\"b\",\"Type\":\"Identifier\",\"Start\":10,\"End\":11,\"Length\":1},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":11,\"End\":12,\"Length\":1},{\"Content\":\"c\",\"Type\":\"Identifier\",\"Start\":13,\"End\":14,\"Length\":1},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":14,\"End\":15,\"Length\":1},{\"Content\":\"d\",\"Type\":\"Identifier\",\"Start\":16,\"End\":17,\"Length\":1},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":17,\"End\":18,\"Length\":1},{\"Content\":\"max\",\"Type\":\"Identifier\",\"Start\":19,\"End\":22,\"Length\":3},{\"Content\":\"(\",\"Type\":\"OpenParen\",\"Start\":22,\"End\":23,\"Length\":1},{\"Content\":\"e\",\"Type\":\"Identifier\",\"Start\":23,\"End\":24,\"Length\":1},{\"Content\":\")\",\"Type\":\"CloseParen\",\"Start\":24,\"End\":25,\"Length\":1},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":25,\"End\":26,\"Length\":1},{\"Content\":\"(\",\"Type\":\"OpenParen\",\"Start\":27,\"End\":28,\"Length\":1},{\"Content\":\"select\",\"Type\":\"ReservedKeyword\",\"Start\":28,\"End\":34,\"Length\":6},{\"Content\":\"f\",\"Type\":\"Identifier\",\"Start\":35,\"End\":36,\"Length\":1},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":36,\"End\":37,\"Length\":1},{\"Content\":\"g\",\"Type\":\"Identifier\",\"Start\":38,\"End\":39,\"Length\":1},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":39,\"End\":40,\"Length\":1},{\"Content\":\"h\",\"Type\":\"Identifier\",\"Start\":41,\"End\":42,\"Length\":1},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":42,\"End\":43,\"Length\":1},{\"Content\":\"(\",\"Type\":\"OpenParen\",\"Start\":44,\"End\":45,\"Length\":1},{\"Content\":\"select\",\"Type\":\"ReservedKeyword\",\"Start\":45,\"End\":51,\"Length\":6},{\"Content\":\"i\",\"Type\":\"Identifier\",\"Start\":52,\"End\":53,\"Length\":1},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":53,\"End\":54,\"Length\":1},{\"Content\":\"j\",\"Type\":\"Identifier\",\"Start\":55,\"End\":56,\"Length\":1},{\"Content\":\"from\",\"Type\":\"ReservedKeyword\",\"Start\":57,\"End\":61,\"Length\":4},{\"Content\":\"l\",\"Type\":\"Identifier\",\"Start\":62,\"End\":63,\"Length\":1},{\"Content\":\"where\",\"Type\":\"ReservedKeyword\",\"Start\":64,\"End\":69,\"Length\":5},{\"Content\":\"j\",\"Type\":\"Identifier\",\"Start\":70,\"End\":71,\"Length\":1},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":72,\"End\":73,\"Length\":1},{\"Content\":\"a\",\"Type\":\"Identifier\",\"Start\":74,\"End\":75,\"Length\":1},{\"Content\":\")\",\"Type\":\"CloseParen\",\"Start\":75,\"End\":76,\"Length\":1},{\"Content\":\"as\",\"Type\":\"ReservedKeyword\",\"Start\":77,\"End\":79,\"Length\":2},{\"Content\":\"m\",\"Type\":\"Identifier\",\"Start\":80,\"End\":81,\"Length\":1},{\"Content\":\"from\",\"Type\":\"ReservedKeyword\",\"Start\":82,\"End\":86,\"Length\":4},{\"Content\":\"n\",\"Type\":\"Identifier\",\"Start\":87,\"End\":88,\"Length\":1},{\"Content\":\"where\",\"Type\":\"ReservedKeyword\",\"Start\":89,\"End\":94,\"Length\":5},{\"Content\":\"g\",\"Type\":\"Identifier\",\"Start\":95,\"End\":96,\"Length\":1},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":97,\"End\":98,\"Length\":1},{\"Content\":\"a\",\"Type\":\"Identifier\",\"Start\":99,\"End\":100,\"Length\":1},{\"Content\":\")\",\"Type\":\"CloseParen\",\"Start\":100,\"End\":101,\"Length\":1},{\"Content\":\"as\",\"Type\":\"ReservedKeyword\",\"Start\":102,\"End\":104,\"Length\":2},{\"Content\":\"o\",\"Type\":\"Identifier\",\"Start\":105,\"End\":106,\"Length\":1},{\"Content\":\"from\",\"Type\":\"ReservedKeyword\",\"Start\":107,\"End\":111,\"Length\":4},{\"Content\":\"p\",\"Type\":\"Identifier\",\"Start\":112,\"End\":113,\"Length\":1},{\"Content\":\"where\",\"Type\":\"ReservedKeyword\",\"Start\":114,\"End\":119,\"Length\":5},{\"Content\":\"a\",\"Type\":\"Identifier\",\"Start\":120,\"End\":121,\"Length\":1},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":122,\"End\":123,\"Length\":1},{\"Content\":\"\\u0027robert\\u0027\",\"Type\":\"Literal\",\"Start\":124,\"End\":132,\"Length\":8},{\"Content\":\";\",\"Type\":\"Semicolon\",\"Start\":132,\"End\":133,\"Length\":1}],\"Success\":true,\"ErrorMessage\":\"\",\"Statistics\":{\"ElapsedMicroseconds\":481,\"TotalLinesOfCode\":1,\"NonEmptyLinesOfCode\":1}},\"Formatters\":[],\"ContentTypes\":[],\"DeclaredType\":null,\"StatusCode\":200}\r\n", _jsonOptions);
        FormatOptions options = new() { CommaFirst = true, AlignIdentifiers = true };

        var actual = Formatter.Format(tokens.Value.Tokens, options).ToString();

        Assert.Equal("select a\r\n      ,b\r\n      ,c\r\n      ,d\r\n      ,max(e)\r\n      ,(select f\r\n              ,g\r\n              ,h\r\n              ,(select i\r\n                      ,j\r\n              from l\r\n              where j = a) as m\r\n      from n\r\n      where g = a) as o\r\nfrom p\r\nwhere a = 'robert';", actual);
    }

    [Fact]
    public void Update_WithoutOptions()
    {
        // sql: update students set grade = 4, last_name = 'Bond', first_name = 'James';

        TokenList? tokens = JsonSerializer.Deserialize<TokenList>("{\"Value\":{\"Tokens\":[{\"Content\":\"update\",\"Type\":\"ReservedKeyword\",\"Start\":0,\"End\":6,\"Length\":6},{\"Content\":\"students\",\"Type\":\"Identifier\",\"Start\":7,\"End\":15,\"Length\":8},{\"Content\":\"set\",\"Type\":\"ReservedKeyword\",\"Start\":16,\"End\":19,\"Length\":3},{\"Content\":\"grade\",\"Type\":\"Identifier\",\"Start\":20,\"End\":25,\"Length\":5},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":26,\"End\":27,\"Length\":1},{\"Content\":\"4\",\"Type\":\"Operator\",\"Start\":28,\"End\":29,\"Length\":1},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":29,\"End\":30,\"Length\":1},{\"Content\":\"last_name\",\"Type\":\"Identifier\",\"Start\":31,\"End\":40,\"Length\":9},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":41,\"End\":42,\"Length\":1},{\"Content\":\"\\u0027Bond\\u0027\",\"Type\":\"Literal\",\"Start\":43,\"End\":49,\"Length\":6},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":49,\"End\":50,\"Length\":1},{\"Content\":\"first_name\",\"Type\":\"Identifier\",\"Start\":51,\"End\":61,\"Length\":10},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":62,\"End\":63,\"Length\":1},{\"Content\":\"\\u0027James\\u0027\",\"Type\":\"Literal\",\"Start\":64,\"End\":71,\"Length\":7},{\"Content\":\";\",\"Type\":\"Semicolon\",\"Start\":71,\"End\":72,\"Length\":1}],\"Success\":true,\"ErrorMessage\":\"\",\"Statistics\":{\"ElapsedMicroseconds\":47,\"TotalLinesOfCode\":1,\"NonEmptyLinesOfCode\":1}},\"Formatters\":[],\"ContentTypes\":[],\"DeclaredType\":null,\"StatusCode\":200}\r\n", _jsonOptions);
        FormatOptions options = new();

        var actual = Formatter.Format(tokens.Value.Tokens, options).ToString();

        Assert.Equal("update students\r\nset grade = 4,\r\nlast_name = 'Bond',\r\nfirst_name = 'James';", actual);
    }


    [Fact]
    public void Update_WithCommaFirst()
    {
        // sql: update students set grade = 4, last_name = 'Bond', first_name = 'James';

        TokenList? tokens = JsonSerializer.Deserialize<TokenList>("{\"Value\":{\"Tokens\":[{\"Content\":\"update\",\"Type\":\"ReservedKeyword\",\"Start\":0,\"End\":6,\"Length\":6},{\"Content\":\"students\",\"Type\":\"Identifier\",\"Start\":7,\"End\":15,\"Length\":8},{\"Content\":\"set\",\"Type\":\"ReservedKeyword\",\"Start\":16,\"End\":19,\"Length\":3},{\"Content\":\"grade\",\"Type\":\"Identifier\",\"Start\":20,\"End\":25,\"Length\":5},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":26,\"End\":27,\"Length\":1},{\"Content\":\"4\",\"Type\":\"Operator\",\"Start\":28,\"End\":29,\"Length\":1},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":29,\"End\":30,\"Length\":1},{\"Content\":\"last_name\",\"Type\":\"Identifier\",\"Start\":31,\"End\":40,\"Length\":9},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":41,\"End\":42,\"Length\":1},{\"Content\":\"\\u0027Bond\\u0027\",\"Type\":\"Literal\",\"Start\":43,\"End\":49,\"Length\":6},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":49,\"End\":50,\"Length\":1},{\"Content\":\"first_name\",\"Type\":\"Identifier\",\"Start\":51,\"End\":61,\"Length\":10},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":62,\"End\":63,\"Length\":1},{\"Content\":\"\\u0027James\\u0027\",\"Type\":\"Literal\",\"Start\":64,\"End\":71,\"Length\":7},{\"Content\":\";\",\"Type\":\"Semicolon\",\"Start\":71,\"End\":72,\"Length\":1}],\"Success\":true,\"ErrorMessage\":\"\",\"Statistics\":{\"ElapsedMicroseconds\":42,\"TotalLinesOfCode\":1,\"NonEmptyLinesOfCode\":1}},\"Formatters\":[],\"ContentTypes\":[],\"DeclaredType\":null,\"StatusCode\":200}\r\n", _jsonOptions);
        FormatOptions options = new() { CommaFirst = true };

        var actual = Formatter.Format(tokens.Value.Tokens, options).ToString();

        Assert.Equal("update students\r\nset grade = 4\r\n,last_name = 'Bond'\r\n,first_name = 'James';", actual);
    }

    [Fact]
    public void Update_WithAlignIdentifiers()
    {
        // sql: update students set grade = 4, last_name = 'Bond', first_name = 'James';

        TokenList? tokens = JsonSerializer.Deserialize<TokenList>("{\"Value\":{\"Tokens\":[{\"Content\":\"update\",\"Type\":\"ReservedKeyword\",\"Start\":0,\"End\":6,\"Length\":6},{\"Content\":\"students\",\"Type\":\"Identifier\",\"Start\":7,\"End\":15,\"Length\":8},{\"Content\":\"set\",\"Type\":\"ReservedKeyword\",\"Start\":16,\"End\":19,\"Length\":3},{\"Content\":\"grade\",\"Type\":\"Identifier\",\"Start\":20,\"End\":25,\"Length\":5},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":26,\"End\":27,\"Length\":1},{\"Content\":\"4\",\"Type\":\"Operator\",\"Start\":28,\"End\":29,\"Length\":1},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":29,\"End\":30,\"Length\":1},{\"Content\":\"last_name\",\"Type\":\"Identifier\",\"Start\":31,\"End\":40,\"Length\":9},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":41,\"End\":42,\"Length\":1},{\"Content\":\"\\u0027Bond\\u0027\",\"Type\":\"Literal\",\"Start\":43,\"End\":49,\"Length\":6},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":49,\"End\":50,\"Length\":1},{\"Content\":\"first_name\",\"Type\":\"Identifier\",\"Start\":51,\"End\":61,\"Length\":10},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":62,\"End\":63,\"Length\":1},{\"Content\":\"\\u0027James\\u0027\",\"Type\":\"Literal\",\"Start\":64,\"End\":71,\"Length\":7},{\"Content\":\";\",\"Type\":\"Semicolon\",\"Start\":71,\"End\":72,\"Length\":1}],\"Success\":true,\"ErrorMessage\":\"\",\"Statistics\":{\"ElapsedMicroseconds\":241,\"TotalLinesOfCode\":1,\"NonEmptyLinesOfCode\":1}},\"Formatters\":[],\"ContentTypes\":[],\"DeclaredType\":null,\"StatusCode\":200}\r\n", _jsonOptions);
        FormatOptions options = new() { AlignIdentifiers = true };

        var actual = Formatter.Format(tokens.Value.Tokens, options).ToString();

        Assert.Equal("update students\r\nset grade = 4,\r\n    last_name = 'Bond',\r\n    first_name = 'James';", actual);
    }


    [Fact]
    public void Update_WithAlignEqualSign()
    {
        // sql: update students set grade = 4, last_name = 'Bond', first_name = 'James';

        TokenList? tokens = JsonSerializer.Deserialize<TokenList>("{\"Value\":{\"Tokens\":[{\"Content\":\"update\",\"Type\":\"ReservedKeyword\",\"Start\":0,\"End\":6,\"Length\":6},{\"Content\":\"students\",\"Type\":\"Identifier\",\"Start\":7,\"End\":15,\"Length\":8},{\"Content\":\"set\",\"Type\":\"ReservedKeyword\",\"Start\":16,\"End\":19,\"Length\":3},{\"Content\":\"grade\",\"Type\":\"Identifier\",\"Start\":20,\"End\":25,\"Length\":5},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":26,\"End\":27,\"Length\":1},{\"Content\":\"4\",\"Type\":\"Operator\",\"Start\":28,\"End\":29,\"Length\":1},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":29,\"End\":30,\"Length\":1},{\"Content\":\"last_name\",\"Type\":\"Identifier\",\"Start\":31,\"End\":40,\"Length\":9},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":41,\"End\":42,\"Length\":1},{\"Content\":\"\\u0027Bond\\u0027\",\"Type\":\"Literal\",\"Start\":43,\"End\":49,\"Length\":6},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":49,\"End\":50,\"Length\":1},{\"Content\":\"first_name\",\"Type\":\"Identifier\",\"Start\":51,\"End\":61,\"Length\":10},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":62,\"End\":63,\"Length\":1},{\"Content\":\"\\u0027James\\u0027\",\"Type\":\"Literal\",\"Start\":64,\"End\":71,\"Length\":7},{\"Content\":\";\",\"Type\":\"Semicolon\",\"Start\":71,\"End\":72,\"Length\":1}],\"Success\":true,\"ErrorMessage\":\"\",\"Statistics\":{\"ElapsedMicroseconds\":65,\"TotalLinesOfCode\":1,\"NonEmptyLinesOfCode\":1}},\"Formatters\":[],\"ContentTypes\":[],\"DeclaredType\":null,\"StatusCode\":200}\r\n", _jsonOptions);
        FormatOptions options = new() { AlignEqualSign = true };

        var actual = Formatter.Format(tokens.Value.Tokens, options).ToString();

        Assert.Equal("update students\r\nset grade  = 4,\r\nlast_name  = 'Bond',\r\nfirst_name = 'James';", actual);
    }

    [Fact]
    public void Update_WithCommaFirstAndAlignIdentifiers()
    {
        // sql: update students set grade = 4, last_name = 'Bond', first_name = 'James';

        TokenList? tokens = JsonSerializer.Deserialize<TokenList>("{\"Value\":{\"Tokens\":[{\"Content\":\"update\",\"Type\":\"ReservedKeyword\",\"Start\":0,\"End\":6,\"Length\":6},{\"Content\":\"students\",\"Type\":\"Identifier\",\"Start\":7,\"End\":15,\"Length\":8},{\"Content\":\"set\",\"Type\":\"ReservedKeyword\",\"Start\":16,\"End\":19,\"Length\":3},{\"Content\":\"grade\",\"Type\":\"Identifier\",\"Start\":20,\"End\":25,\"Length\":5},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":26,\"End\":27,\"Length\":1},{\"Content\":\"4\",\"Type\":\"Operator\",\"Start\":28,\"End\":29,\"Length\":1},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":29,\"End\":30,\"Length\":1},{\"Content\":\"last_name\",\"Type\":\"Identifier\",\"Start\":31,\"End\":40,\"Length\":9},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":41,\"End\":42,\"Length\":1},{\"Content\":\"\\u0027Bond\\u0027\",\"Type\":\"Literal\",\"Start\":43,\"End\":49,\"Length\":6},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":49,\"End\":50,\"Length\":1},{\"Content\":\"first_name\",\"Type\":\"Identifier\",\"Start\":51,\"End\":61,\"Length\":10},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":62,\"End\":63,\"Length\":1},{\"Content\":\"\\u0027James\\u0027\",\"Type\":\"Literal\",\"Start\":64,\"End\":71,\"Length\":7},{\"Content\":\";\",\"Type\":\"Semicolon\",\"Start\":71,\"End\":72,\"Length\":1}],\"Success\":true,\"ErrorMessage\":\"\",\"Statistics\":{\"ElapsedMicroseconds\":42,\"TotalLinesOfCode\":1,\"NonEmptyLinesOfCode\":1}},\"Formatters\":[],\"ContentTypes\":[],\"DeclaredType\":null,\"StatusCode\":200}\r\n", _jsonOptions);
        FormatOptions options = new() { CommaFirst = true, AlignIdentifiers = true };

        var actual = Formatter.Format(tokens.Value.Tokens, options).ToString();

        Assert.Equal("update students\r\nset grade = 4\r\n   ,last_name = 'Bond'\r\n   ,first_name = 'James';", actual);
    }

    [Fact]
    public void Update_WithCommaFirstAndAlignEqualSign()
    {
        // sql: update students set grade = 4, last_name = 'Bond', first_name = 'James';

        TokenList? tokens = JsonSerializer.Deserialize<TokenList>("{\"Value\":{\"Tokens\":[{\"Content\":\"update\",\"Type\":\"ReservedKeyword\",\"Start\":0,\"End\":6,\"Length\":6},{\"Content\":\"students\",\"Type\":\"Identifier\",\"Start\":7,\"End\":15,\"Length\":8},{\"Content\":\"set\",\"Type\":\"ReservedKeyword\",\"Start\":16,\"End\":19,\"Length\":3},{\"Content\":\"grade\",\"Type\":\"Identifier\",\"Start\":20,\"End\":25,\"Length\":5},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":26,\"End\":27,\"Length\":1},{\"Content\":\"4\",\"Type\":\"Operator\",\"Start\":28,\"End\":29,\"Length\":1},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":29,\"End\":30,\"Length\":1},{\"Content\":\"last_name\",\"Type\":\"Identifier\",\"Start\":31,\"End\":40,\"Length\":9},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":41,\"End\":42,\"Length\":1},{\"Content\":\"\\u0027Bond\\u0027\",\"Type\":\"Literal\",\"Start\":43,\"End\":49,\"Length\":6},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":49,\"End\":50,\"Length\":1},{\"Content\":\"first_name\",\"Type\":\"Identifier\",\"Start\":51,\"End\":61,\"Length\":10},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":62,\"End\":63,\"Length\":1},{\"Content\":\"\\u0027James\\u0027\",\"Type\":\"Literal\",\"Start\":64,\"End\":71,\"Length\":7},{\"Content\":\";\",\"Type\":\"Semicolon\",\"Start\":71,\"End\":72,\"Length\":1}],\"Success\":true,\"ErrorMessage\":\"\",\"Statistics\":{\"ElapsedMicroseconds\":42,\"TotalLinesOfCode\":1,\"NonEmptyLinesOfCode\":1}},\"Formatters\":[],\"ContentTypes\":[],\"DeclaredType\":null,\"StatusCode\":200}\r\n", _jsonOptions);
        FormatOptions options = new() { CommaFirst = true, AlignEqualSign = true };

        var actual = Formatter.Format(tokens.Value.Tokens, options).ToString();

        Assert.Equal("update students\r\nset grade   = 4\r\n,last_name  = 'Bond'\r\n,first_name = 'James';", actual);
    }

    [Fact]
    public void Update_WithCommaFirstAndAlignIdentifiersAlignEqualSign()
    {
        // sql: update students set grade = 4, last_name = 'Bond', first_name = 'James';

        TokenList? tokens = JsonSerializer.Deserialize<TokenList>("{\"Value\":{\"Tokens\":[{\"Content\":\"update\",\"Type\":\"ReservedKeyword\",\"Start\":0,\"End\":6,\"Length\":6},{\"Content\":\"students\",\"Type\":\"Identifier\",\"Start\":7,\"End\":15,\"Length\":8},{\"Content\":\"set\",\"Type\":\"ReservedKeyword\",\"Start\":16,\"End\":19,\"Length\":3},{\"Content\":\"grade\",\"Type\":\"Identifier\",\"Start\":20,\"End\":25,\"Length\":5},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":26,\"End\":27,\"Length\":1},{\"Content\":\"4\",\"Type\":\"Operator\",\"Start\":28,\"End\":29,\"Length\":1},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":29,\"End\":30,\"Length\":1},{\"Content\":\"last_name\",\"Type\":\"Identifier\",\"Start\":31,\"End\":40,\"Length\":9},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":41,\"End\":42,\"Length\":1},{\"Content\":\"\\u0027Bond\\u0027\",\"Type\":\"Literal\",\"Start\":43,\"End\":49,\"Length\":6},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":49,\"End\":50,\"Length\":1},{\"Content\":\"first_name\",\"Type\":\"Identifier\",\"Start\":51,\"End\":61,\"Length\":10},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":62,\"End\":63,\"Length\":1},{\"Content\":\"\\u0027James\\u0027\",\"Type\":\"Literal\",\"Start\":64,\"End\":71,\"Length\":7},{\"Content\":\";\",\"Type\":\"Semicolon\",\"Start\":71,\"End\":72,\"Length\":1}],\"Success\":true,\"ErrorMessage\":\"\",\"Statistics\":{\"ElapsedMicroseconds\":42,\"TotalLinesOfCode\":1,\"NonEmptyLinesOfCode\":1}},\"Formatters\":[],\"ContentTypes\":[],\"DeclaredType\":null,\"StatusCode\":200}\r\n", _jsonOptions);
        FormatOptions options = new() { CommaFirst = true, AlignIdentifiers = true, AlignEqualSign = true };

        var actual = Formatter.Format(tokens.Value.Tokens, options).ToString();

        Assert.Equal("update students\r\nset grade      = 4\r\n   ,last_name  = 'Bond'\r\n   ,first_name = 'James';", actual);
    }

    [Fact]
    public void Update_AlignIdentifiersAndAlignEqualSign()
    {
        // sql: update students set grade = 4, last_name = 'Bond', first_name = 'James';

        TokenList? tokens = JsonSerializer.Deserialize<TokenList>("{\"Value\":{\"Tokens\":[{\"Content\":\"update\",\"Type\":\"ReservedKeyword\",\"Start\":0,\"End\":6,\"Length\":6},{\"Content\":\"students\",\"Type\":\"Identifier\",\"Start\":7,\"End\":15,\"Length\":8},{\"Content\":\"set\",\"Type\":\"ReservedKeyword\",\"Start\":16,\"End\":19,\"Length\":3},{\"Content\":\"grade\",\"Type\":\"Identifier\",\"Start\":20,\"End\":25,\"Length\":5},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":26,\"End\":27,\"Length\":1},{\"Content\":\"4\",\"Type\":\"Operator\",\"Start\":28,\"End\":29,\"Length\":1},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":29,\"End\":30,\"Length\":1},{\"Content\":\"last_name\",\"Type\":\"Identifier\",\"Start\":31,\"End\":40,\"Length\":9},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":41,\"End\":42,\"Length\":1},{\"Content\":\"\\u0027Bond\\u0027\",\"Type\":\"Literal\",\"Start\":43,\"End\":49,\"Length\":6},{\"Content\":\",\",\"Type\":\"Comma\",\"Start\":49,\"End\":50,\"Length\":1},{\"Content\":\"first_name\",\"Type\":\"Identifier\",\"Start\":51,\"End\":61,\"Length\":10},{\"Content\":\"=\",\"Type\":\"Operator\",\"Start\":62,\"End\":63,\"Length\":1},{\"Content\":\"\\u0027James\\u0027\",\"Type\":\"Literal\",\"Start\":64,\"End\":71,\"Length\":7},{\"Content\":\";\",\"Type\":\"Semicolon\",\"Start\":71,\"End\":72,\"Length\":1}],\"Success\":true,\"ErrorMessage\":\"\",\"Statistics\":{\"ElapsedMicroseconds\":42,\"TotalLinesOfCode\":1,\"NonEmptyLinesOfCode\":1}},\"Formatters\":[],\"ContentTypes\":[],\"DeclaredType\":null,\"StatusCode\":200}\r\n", _jsonOptions);
        FormatOptions options = new() { AlignIdentifiers = true, AlignEqualSign = true };

        var actual = Formatter.Format(tokens.Value.Tokens, options).ToString();

        Assert.Equal("update students\r\nset grade      = 4,\r\n    last_name  = 'Bond',\r\n    first_name = 'James';", actual);
    }
}