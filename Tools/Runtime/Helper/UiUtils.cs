using System.Linq;

namespace DivineSkies.Tools.Helper
{
    public static class UiUtils
    {
        public static string WrapText(string source, int maxCharPerLine)
        {
            if (source.Length <= maxCharPerLine)
                return source;

            string[] words = source.Split(' ');

            int longestWordLength = words.Max((s) => s.Length);
            if (longestWordLength > maxCharPerLine)
                maxCharPerLine = longestWordLength;

            string currentLine = string.Empty;
            string result = string.Empty;
            for (int i = 0; i < words.Length; i++)
            {
                if (currentLine.Length == 0 || (currentLine + words[i]).Length < maxCharPerLine)
                {
                    currentLine += words[i] + " ";
                }
                else
                {
                    if (result.Length != 0)
                        result += "\n";

                    result += currentLine;
                    currentLine = words[i] + " ";
                }
            }
            if (result.Length != 0)
                result += "\n";

            result += currentLine;

            return result;
        }
    }
}