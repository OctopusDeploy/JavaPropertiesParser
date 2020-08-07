using System.Globalization;
using JavaPropertiesParser.Utils;
using Sprache;

namespace JavaPropertiesParser.Parsers
{
    public static class Common
    {
        private static char DecodeUnicodeHex(string hexDigits)
        {
            return (char)int.Parse(hexDigits, NumberStyles.HexNumber);
        }

        private static readonly Parser<char> HexDigit = Parse.Chars("1234567890abcdefABCDEF");

        private static readonly Parser<StringValue> EscapedCarriageReturnParser = BuildEscapeSequenceParser('r', "\r");
        private static readonly Parser<StringValue> EscapedLineFeedParser = BuildEscapeSequenceParser('n', "\n");
        private static readonly Parser<StringValue> EscapedTabParser = BuildEscapeSequenceParser('t', "\t");

        private static readonly Parser<StringValue> EscapedPhysicalNewLineParser =
            from newLine in Parse.String("\r\n").XOr(Parse.String("\n")).Text()
            from whitespace in Parse.Chars(" \t").XMany().Text()
            select new StringValue("", "\\" + newLine + whitespace);

        private static readonly Parser<StringValue> EscapedUnicodeSequenceParser =
            from u in Parse.Char('u')
            from digits in HexDigit.Repeat(4).Text()
            let decoded = DecodeUnicodeHex(digits)
            select new StringValue(decoded.ToString(), "\\u" + digits);

        private static readonly Parser<StringValue> EscapedOtherParser =
            from next in Parse.CharExcept("rntu")
            select new StringValue(next.ToString(), "\\" + next);

        public static readonly Parser<StringValue> EscapeSequenceParser =
            from slash in Parse.Char('\\')
            from rest in EscapedUnicodeSequenceParser
                .XOr(EscapedCarriageReturnParser)
                .XOr(EscapedLineFeedParser)
                .XOr(EscapedTabParser)
                .XOr(EscapedPhysicalNewLineParser)
                .XOr(EscapedOtherParser)
            select rest;

        private static Parser<StringValue> BuildEscapeSequenceParser(char @char, string logicalValue)
        {
            return
                from r in Parse.Char(@char)
                select new StringValue(logicalValue, "\\" + @char);
        }
    }
}