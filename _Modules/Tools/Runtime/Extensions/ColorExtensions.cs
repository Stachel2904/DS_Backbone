using System;
using UnityEngine;

namespace DivineSkies.Tools.Extensions
{
    public static class ColorExtensions
    {
        /// <summary>
        /// simple convertation from <see cref="Color"/> to <see cref="Color32"/>
        /// </summary>
        public static Color32 To32(this Color source) => new((byte)(source.r * 255), (byte)(source.g * 255), (byte)(source.b * 255), (byte)(source.a * 255));

        /// <summary>
        /// Creates Color from Hex Code
        /// </summary>
        public static Color32 ToColorFromHex(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                throw new NullReferenceException(source + " is null/empty");
            }

            if (source[0] != '#')
            {
                source = "#" + source;
            }

            if (!ColorUtility.TryParseHtmlString(source, out var raw))
            {
                throw new ArgumentException(source + " is no valid hex string");
            }

            return raw.To32();
        }
    }
}