namespace TextParser.Utils.Serializers
{
    using Parsers.TextParser;

    public interface ISerializeTextObject
    {
        string Serialize(Text text);
    }
}