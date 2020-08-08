using System.Text;

namespace JavaPropertiesParser.Utils
{
    public static class Encode
    {
        private static string Common(string input)
        {
            var output = input
                .Replace("\\", "\\\\")
                .Replace("\r", "\\r")
                .Replace("\n", "\\n")
                .Replace("\t", "\\t");
            
            var result = new StringBuilder();

            foreach (var ch in output)
            {
                var isIso88591Compatible = ch >= 0x00 && ch < 0xFF;
                if (isIso88591Compatible)
                {
                    result.Append(ch);
                }
                else
                {
                    var hex = ((int)ch).ToString("x4");
                    result.Append($"\\u{hex}");
                }
            }
            
            return result.ToString();
        }

        public static string Key(string input)
        {
            return Common(input)
                .Replace(" ", "\\ ")
                .Replace(":", "\\:")
                .Replace("=", "\\=");
        }
        
        public static string Value(string input)
        {
            var output = Common(input);
            return output.StartsWith(" ") 
                ? "\\ " + output.Substring(1) 
                : output;
        }
    }
}