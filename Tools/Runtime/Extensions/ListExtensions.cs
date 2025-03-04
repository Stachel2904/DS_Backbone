using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DivineSkies.Tools.Extensions
{
    public static class ListExtensions
    {
        public static bool TryFind<T>(this IList<T> list, Func<T, bool> match, out T result)
        {
            result = list.Where(match).FirstOrDefault();
            return result != null;
        }

        public static bool TryGetLast<T>(this IList<T> list, out T result)
        {
            if (list.Count > 0)
            {
                result = list[list.Count - 1];
                return true;
            }
            result = default(T);
            return false;
        }

        public static T[] DrawRandom<T>(this IList<T> list, int amount)
        {
            var result = new T[amount];
            for (int i = 0; i < amount; i++)
            {
                result[i] = list.DrawRandom();
            }
            return result;
        }

        public static T DrawRandom<T>(this IList<T> list)
        {
            if (list.Count == 0)
            {
                throw new ArgumentException("Couldn't draw random from empty List");
            }

            return list[Random.Range(0, list.Count)];
        }

        public static T[] DrawRandom<T>(this List<T> list, int amount)
        {
            return list.DrawRandom(amount, false);
        }

        public static T[] DrawRandom<T>(this List<T> list, int amount, bool includeDoubles)
        {
            T[] result = new T[amount];
            if (includeDoubles)
            {
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = list.DrawRandom();
                }
            }
            else
            {
                List<T> listCopy = new List<T>();
                listCopy.AddRange(list);
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = listCopy.DrawRandom();
                    listCopy.Remove(result[i]);
                }
            }

            return result;
        }

        public static void AddMultiple<T>(this List<T> list, T addedData, int amount)
        {
            for (int i = 0; i < amount; i++)
                list.Add(addedData);
        }

        public static void RemoveLast<T>(this IList<T> list)
        {
            if (list.Count > 0)
                list.RemoveAt(list.Count - 1);
        }

        public static bool ContainsAll<T>(this T[] array, T[] other)
        {
            for (int i = 0; i < other.Length; i++)
                if (!array.Contains(other[i]))
                    return false;
            return true;
        }
        public static bool ContainsAny<T>(this T[] array, T[] other)
        {
            for (int i = 0; i < other.Length; i++)
                if (array.Contains(other[i]))
                    return true;
            return false;
        }

        public static void ForEach<T>(this IList<T> self, Action<T> operation)
        {
            for (int i = 0; i < self.Count; i++)
                operation(self[i]);
        }

        public static void ReverseForEach<T>(this IList<T> self, Action<T> operation)
        {
            for (int i = self.Count() - 1; i >= 0; i--)
                operation(self[i]);
        }

        public static void Shuffle<T>(this IList<T> self)
        {
            for (int switchIndexA = self.Count - 1; switchIndexA > 1; switchIndexA--)
            {
                int switchIndexB = Random.Range(0, switchIndexA - 1);
                T movingElement = self[switchIndexB];
                self[switchIndexB] = self[switchIndexA];
                self[switchIndexA] = movingElement;
            }
        }

        public static IList<TEnum> ReFillValues<TEnum>(this IList<TEnum> self, int amount = 0, bool excludeDoubles = true, Func<TEnum, bool> additionalCheck = null) where TEnum : Enum
        {
            if (amount == 0)
                amount = self.Count;
            if (!self.IsReadOnly)
                self.Clear();
            else if (self is TEnum[])
                self = Array.Empty<TEnum>();
            return self.FillValues(amount, excludeDoubles, additionalCheck);
        }

        public static IList<TEnum> FillValues<TEnum>(this IList<TEnum> self, int amount, bool excludeDoubles = true, Func<TEnum, bool> additionalCheck = null) where TEnum : Enum
        {
            List<TEnum> values = new(Enum.GetValues(typeof(TEnum)) as IEnumerable<TEnum>);

            if (additionalCheck != null)
                values = values.Where(v => additionalCheck(v)).ToList();

            if (excludeDoubles && values.Count < amount)
            {
                Debug.LogWarning("Could not exclude doubles for filling list because there are not enough values.");
                excludeDoubles = false;
            }

            List<TEnum> result;
            if (excludeDoubles && values.Count == amount)
            {
                values.Shuffle();
                result = values;
            }
            else
            {
                result = new List<TEnum>(values.DrawRandom(amount, !excludeDoubles));
            }

            if (self is List<TEnum> selfList)
                selfList.AddRange(result);
            if (self is TEnum[] selfArray)
                return selfArray.Concat(result).ToArray();
            else
                result.ForEach(v => self.Add(v));

            return self;
        }

        public static string ToContentString<T>(this IList<T> self) where T : Enum => self.ToContentString(t => t.ToTooltipString(), ", ");

        public static string ToContentString<T>(this IList<T> self, Func<T, string> toString, string seperator)
        {
            if (self.Count == 0)
                return string.Empty;

            string result = toString(self[0]);

            for (int i = 1; i < self.Count; i++)
                result += seperator + toString(self[i]);

            return result;
        }
    }
}