using System;
using System.Globalization;
using System.Text;
using JavaPropertiesParser.Expressions;
using JavaPropertiesParser.Utils;
using Sprache;

namespace JavaPropertiesParser.Parsers
{
    public static class Keys
    {
        private static readonly Parser<char> HexDigit = Parse.Chars("1234567890abcdefABCDEF");

        private static char DecodeUnicodeHex(string hexDigits)
        {
            return (char)int.Parse(hexDigits, NumberStyles.HexNumber);
        }

        private static readonly Parser<StringValue> EscapedUnicodeSequenceParser =
            from u in Parse.Char('u')
            from digits in HexDigit.Repeat(4).Text()
            let decoded = DecodeUnicodeHex(digits)
            select new StringValue(decoded.ToString(), "\\u" + digits);

        private static Parser<StringValue> BuildEscapeSequenceParser(char @char, string logicalValue)
        {
            return
                from r in Parse.Char(@char)
                select new StringValue(logicalValue, "\\" + @char);
        }

        private static readonly Parser<StringValue> EscapedCarriageReturnParser = BuildEscapeSequenceParser('r', "\r");
        private static readonly Parser<StringValue> EscapedLineFeedParser = BuildEscapeSequenceParser('n', "\n");
        private static readonly Parser<StringValue> EscapedTabParser = BuildEscapeSequenceParser('t', "\t");

        private static readonly Parser<StringValue> EscapedPhysicalNewLineParser =
            from newLine in Parse.String("\r\n").Or(Parse.String("\n")).Text()
            from whitespace in Parse.Chars(" \t").Many().Text()
            select new StringValue("", "\\" + newLine + whitespace);

        private static readonly Parser<StringValue> EscapedOtherParser =
            from next in Parse.AnyChar
            select new StringValue(next.ToString(), "\\" + next);

        private static readonly Parser<StringValue> EscapeSequenceParser =
            from slash in Parse.Char('\\')
            from rest in EscapedUnicodeSequenceParser
                .Or(EscapedCarriageReturnParser)
                .Or(EscapedLineFeedParser)
                .Or(EscapedTabParser)
                .Or(EscapedPhysicalNewLineParser)
                .Or(EscapedOtherParser)
            select rest;
        
        private static readonly Parser<StringValue> KeyCharsParser =
            from chars in Parse.CharExcept(":= \t\\\r\n").AtLeastOnce().Text()
            select new StringValue(chars, chars);

        private static readonly Parser<StringValue> KeyComponentParser = KeyCharsParser
            .Or(EscapeSequenceParser);

        public static readonly Parser<KeyExpression> Parser =
            from components in KeyComponentParser.AtLeastOnce()
            select new KeyExpression(components.Join());
    }
}