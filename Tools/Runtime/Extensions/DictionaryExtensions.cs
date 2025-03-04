using System;
using System.Collections.Generic;

namespace DivineSkies.Tools.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddToList<TKey, TValue>(this Dictionary<TKey, List<TValue>> source, TKey key, TValue value)
        {
            if (!source.ContainsKey(key))
            {
                source.Add(key, new List<TValue>());
            }

            source[key].Add(value);
        }

        public static Tuple<T1, T2>[] ToTupleArray<T1, T2>(this Dictionary<T1, T2> dic)
        {
            Tuple<T1, T2>[] result = new Tuple<T1, T2>[dic.Keys.Count];
            int counter = 0;
            foreach (KeyValuePair<T1, T2> pair in dic)
            {
                Tuple<T1, T2> product = new Tuple<T1, T2>(pair.Key, pair.Value);
                result[counter] = product;
                counter++;
            }
            return result;
        }
    }
}