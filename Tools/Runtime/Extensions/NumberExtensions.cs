using UnityEngine;

namespace DivineSkies.Tools.Extensions
{
    public static class NumberExtensions
    {
        private static string[] ColorTags = { "#ff0000", "#ffffff", "#00ff00" };

        /// <summary>
        /// returns with sign like '+3' or '-4' or '0'
        /// </summary>
        public static string ToSignedString(this int value)
        {
            return (value > 0 ? "+" : "") + value.ToString();
        }

        /// <summary>
        /// Like <see cref="ToSignedString"/> but adds tmp color tag (red for negative, green for positive, white for 0)
        /// </summary>
        public static string ToSignedStringWithColorTag(this int value)
        {
            string[] sign = { "-", " ", "+" };
            int index = Mathf.Clamp(value, -1, 1) + 1;
            return (sign[index] + "" + Mathf.Abs(value)).ToColoredString(ColorTags[index]);
        }

        /// <summary>
        /// Like <see cref="ToSignedString"/> but adds tmp color tag (red for negative, green for positive, white for 0), also cuts float decimals after 2 (like 42.69)
        /// </summary>
        public static string ToSignedStringWithColorTag(this float value)
        {
            string[] sign = { "-", " ", "+" };
            int index = value switch
            {
                _ when value < 0 => 0,
                _ when value > 0 => 2,
                _ => 1
            };
            return (sign[index] + "" + $"{Mathf.Abs(value):F2}").ToColoredString(ColorTags[index]);
        }

        /// <summary>
        /// Returns value in color from red to green
        /// </summary>
        public static Color ToColor(this int source, int min, int max) => ((float)source).ToColor(min, max);

        /// <summary>
        /// Returns value in color from red to green
        /// </summary>
        public static Color ToColor(this float source, float min, float max) => source.ToNormalizedRange(min, max).ToColor();

        /// <summary>
        /// Returns value in color from red to green (clamped 0-1)
        /// </summary>
        public static Color ToColor(this float source)
        {
            source = Mathf.Clamp01(source);
            return Color.Lerp(Color.Lerp(Color.green, Color.red, source), Color.black, 0.5f);
        }

        /// <summary>
        /// Returns source in 0-1 range but with same offset as with min and max
        /// </summary>
        public static float ToNormalizedRange(this float source, float min, float max)
        {
            float range = max - min;
            float scale = 1 / range;
            float offsettedSource = source - min;
            float scaledSource = offsettedSource * scale;
            return scaledSource;
        }

        /// <summary>
        /// Returns seconds stringified up to days
        /// </summary>
        public static string ToTimeString(this double source)
        {
            int days = (int)source;
            double left = source - days;
            int minutes = (int)(24 * 60 * left);
            int hours = minutes / 60;
            minutes %= 60;
            return $"Day {days + 1} {hours:D2}:{minutes:D2}";
        }

        /// <summary>
        /// Returns object to dring with color tags (false is red, true is green)
        /// </summary>
        public static string ToColoredString(this object obj, bool value) => obj.ToColoredString(ColorTags[value ? 2 : 0]);

        /// <summary>
        /// Returns object to dring with custom hex color tag
        /// </summary>
        public static string ToColoredString(this object obj, string colorTag) => $"<color={colorTag}>{obj}</color>";
    }
}