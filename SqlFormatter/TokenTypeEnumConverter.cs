using System.Text.Json.Serialization;
using System.Text.Json;
using SqlFormatter.Model;

namespace SqlFormatter;

public class TokenTypeEnumConverter : JsonConverter<TokenType>
{
    public override TokenType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string enumValue = reader.GetString();
        if (enumValue == "<UNHANDLED TOKEN TYPE>")
            return TokenType.Comma;
        return Enum.Parse<TokenType>(enumValue);
    }

    public override void Write(Utf8JsonWriter writer, TokenType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(Enum.GetName(value));
    }
}