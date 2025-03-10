using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;

namespace DivineSkies.Tools.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Parse string to int safely
        /// </summary>
        public static int ToInt(this string value)
        {
            if (int.TryParse(value, out int result))
            {
                return result;
            }

            Debug.LogError("Cannot Parse " + value + " as integer");
            return default(int);
        }

        /// <summary>
        /// Parse strings to bytes
        /// </summary>
        public static byte[] ToByteArray(this string[] value)
        {
            byte[] result = new byte[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                result[i] = (byte)value[i].ToInt();
            }

            return result;
        }

        /// <summary>
        /// Parse string to float safely
        /// </summary>
        public static float ToFloat(this string value)
        {
            if (float.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out float result))
            {
                return result;
            }

            Debug.LogError("Cannot Parse " + value + " as float");
            return default(float);
        }

        /// <summary>
        /// Parse string to Enum Value
        /// </summary>
        public static T ToEnum<T>(this string value) where T : Enum
        {
            if (Enum.TryParse(typeof(T), value == string.Empty ? "None" : value, true, out object result))
            {
                return (T)result;
            }

            Debug.LogError("Cannot Parse " + value + " as " + typeof(T));
            return default(T);
        }

        /// <summary>
        /// Split string and parse to Enum Array
        /// </summary>
        public static T[] ToEnumArray<T>(this string value, char splitChar = ',') where T : Enum
        {
            string[] values = value.Split(splitChar);
            T[] results = new T[values.Length];

            object result;
            for (int i = 0; i < values.Length; i++)
            {
                if (Enum.TryParse(typeof(T), values[i] == string.Empty ? "None" : values[i], out result))
                {
                    results[i] = (T)result;
                }
                else
                {
                    Debug.LogError("Cannot Parse " + values[i] + " as " + typeof(T));
                }
            }

            return results;
        }

        /// <summary>
        /// Split string and parse to Enum Array value with "!" in front will be put to negative list
        /// </summary>
        public static T[] ToEnumArray<T>(this string value, List<T> negativeResults, char splitChar = ',') where T : Enum
        {
            string[] values = value.Split(splitChar);
            List<T> results = new List<T>();

            object result;
            for (int i = 0; i < values.Length; i++)
            {
                if (Enum.TryParse(typeof(T), values[i] == string.Empty ? "None" : values[i], out result)) //try parsing
                {
                    results.Add((T)result);
                }
                else if (values[i].Length > 0 && values[i][0] == '!' && Enum.TryParse(typeof(T), values[i].Remove(0, 1), out result)) //try parsing negative
                {
                    negativeResults.Add((T)result);
                }
                else
                {
                    Debug.LogError("Cannot Parse " + values[i] + " as " + typeof(T));
                }
            }

            return results.ToArray();
        }

        /// <summary>
        /// Splits string in pascal case. So PascalCase will be Pascal Case
        /// </summary>
        public static string SplitPascalCase(this string source) => Regex.Replace(source, "([a-z](?=[A-Z]|[0-9])|[A-Z](?=[A-Z][a-z]|[0-9])|[0-9](?=[^0-9]))", "$1 ");
    }
}