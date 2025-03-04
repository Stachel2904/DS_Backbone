using System;
using System.Linq;
using System.Collections.Generic;

namespace DivineSkies.Tools.Extensions
{
    public static class EnumExtensions
    {
        //<mark=#22222222>
        public static string ToTooltipString(this Enum self) => $"<u><link=\"{self.GetType()}.{self}\">{self}</link></u>";

        public static List<T> GetEnumValueList<T>() where T : Enum => new(Enum.GetValues(typeof(T)).Cast<T>());
        public static List<T> GetEnumValueList<T>(IEnumerable<T> excludes) where T : Enum => new(Enum.GetValues(typeof(T)).Cast<T>().Except(excludes));
        public static List<T> GetEnumValueList<T>(params T[] excludes) where T : Enum => new(Enum.GetValues(typeof(T)).Cast<T>().Except(excludes));
    }
}