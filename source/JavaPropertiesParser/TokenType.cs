namespace JavaPropertiesParser
{
    public enum TokenType
    {
        Comment = 1,
        KeyChars = 2,
        KeyEscapeSequence = 3,
        KeyPhysicalNewLine = 4,
        KeyUnicodeEscapeSequence = 5,
        Separator = 6,
        ValueChars = 7,
        ValueEscapeSequence = 8,
        ValuePhysicalNewLine = 9,
        ValueUnicodeEscapeSequence = 10,
        Whitespace = 11
    }
}