using System;
using Superpower.Model;

namespace JavaPropertiesParser.Expressions
{
    // TODO: rename class
    public static class KeyComponents
    {
        public static StringValue GetStringValue(Token<TokenType> token)
        {
            var tokenString = token.ToStringValue();
            
            switch (token.Kind)
            {
                case TokenType.KeyChars:
                case TokenType.ValueChars:
                    return new StringValue(tokenString, tokenString);
                
                case TokenType.KeyPhysicalNewLine:
                case TokenType.ValuePhysicalNewLine:
                    return tokenString.Contains("\r\n")
                        ? new StringValue(tokenString, "\r\n")
                        : new StringValue(tokenString, "\n");
                
                case TokenType.KeyEscapeSequence:
                case TokenType.ValueEscapeSequence:
                    return new StringValue(tokenString, UnescapeString(tokenString));
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(token), $"Can't handle token of type '{token.Kind}'.");
            }
        }

        private static string UnescapeString(string input)
        {
            switch (input)
            {
                case "\\r":
                    return "\r";
                case "\\n":
                    return "\n";
                case "\\t":
                    return "\t";
                default:
                    if (input[1] == 'u')
                    {
                        // TODO: handle unicode escape.
                        throw new NotImplementedException("Unicode escape sequences aren't implemented.");
                    }
                    else
                    {
                        return input.Substring(1);
                    }
            }
        }
    }
}