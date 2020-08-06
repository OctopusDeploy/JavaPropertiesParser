namespace JavaPropertiesParser
{
    internal enum TokenExpectation
    {
        WhitespaceOrKeyOrComment = 1,
        SeparatorOrKey = 2,
        Value = 3
    }
}