using System;
using System.Linq;
using System.Collections.Generic;

namespace DivineSkies.Tools.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Creates a tmp link to allow tooltip over this enum value (also underlines it)
        /// </summary>
        public static string ToTooltipString(this Enum self) => $"<u><link=\"{self.GetType()}.{self}\">{self}</link></u>";

        /// <summary>
        /// Creates a List of all enum values
        /// </summary>
        public static List<T> GetEnumValueList<T>() where T : Enum => new(Enum.GetValues(typeof(T)).Cast<T>());

        /// <summary>
        /// Creates a List of all enum values
        /// </summary>
        public static List<T> GetEnumValueList<T>(IEnumerable<T> excludes) where T : Enum => new(Enum.GetValues(typeof(T)).Cast<T>().Except(excludes));

        /// <summary>
        /// Creates a List of all enum values
        /// </summary>
        public static List<T> GetEnumValueList<T>(params T[] excludes) where T : Enum => new(Enum.GetValues(typeof(T)).Cast<T>().Except(excludes));
    }
}