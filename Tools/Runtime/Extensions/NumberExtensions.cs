using UnityEngine;

namespace DivineSkies.Tools.Extensions
{
    public static class NumberExtensions
    {
        private static string[] ColorTags = { "#ff0000", "#ffffff", "#00ff00" };

        public static string ToSignedString(this int value)
        {
            return (value > 0 ? "+" : "") + value.ToString();
        }
        public static string ToSignedStringWithColorTag(this int value)
        {
            string[] sign = { "-", " ", "+" };
            int index = Mathf.Clamp(value, -1, 1) + 1;
            return (sign[index] + "" + Mathf.Abs(value)).ToColoredString(ColorTags[index]);
        }
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

        public static Color ToColor(this int source, int min, int max) => ((float)source).ToColor(min, max);
        public static Color ToColor(this float source, float min, float max) => source.ToNormalizedRange(min, max).ToColor();
        public static Color ToColor(this float source)
        {
            source = Mathf.Clamp01(source);
            return Color.Lerp(Color.Lerp(Color.green, Color.red, source), Color.black, 0.5f);
        }

        public static float ToNormalizedRange(this float source, float min, float max)
        {
            float range = max - min;
            float scale = 1 / range;
            float offsettedSource = source - min;
            float scaledSource = offsettedSource * scale;
            return scaledSource;
        }

        public static string ToTimeString(this double source)
        {
            int days = (int)source;
            double left = source - days;
            int minutes = (int)(24 * 60 * left);
            int hours = minutes / 60;
            minutes %= 60;
            return $"Day {days + 1} {hours:D2}:{minutes:D2}";
        }

        public static string ToColoredString(this object obj, bool value) => obj.ToColoredString(ColorTags[value ? 2 : 0]);

        public static string ToColoredString(this object obj, string colorTag) => $"<color={colorTag}>{obj}</color>";
    }
}