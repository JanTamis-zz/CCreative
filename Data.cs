using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CCreative.Colors;
using static CCreative.Data;
using static CCreative.General;
using static CCreative.Math;
using static CCreative.Drawing;
using System.Text.RegularExpressions;
using System.IO.Ports;

namespace CCreative
{
    public static class Data
    {
        //conversion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Converts a value to its floating point representation. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="arg">  The value to convert. </param>
        ///
        /// <returns>   The parsed float. </returns>

        public static float Float<T>(T arg)
        {
            return float.Parse(arg.ToString());
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Converts a boolean, string, or float to its integer representation. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="arg">  The value to convert. </param>
        ///
        /// <returns>   The parsed interger. </returns>

        public static int Int<T>(T arg)
        {
            return int.Parse(arg.ToString());
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Converts a boolean, string or number to its string representation. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="arg">  The value to convert. </param>
        ///
        /// <returns>   The parsed string. </returns>

        public static string str<T>(T arg)
        {
            return arg.ToString();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Converts a number or string to its boolean representation. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="arg">  The value to convert. </param>
        ///
        /// <returns>   The parsed boolean. </returns>

        public static bool Boolean<T>(T arg)
        {
            return bool.Parse(arg.ToString());
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Converts a number, string or boolean to its byte representation. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="arg">  The value to convert. </param>
        ///
        /// <returns>   The parsed byte. </returns>

        public static byte Byte<T>(T arg)
        {
            return byte.Parse(arg.ToString());
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Converts a number or string to its corresponding single-character string representation. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="arg">  The value to convert. </param>
        ///
        /// <returns>   The converted char. </returns>

        public static char Char<T>(T arg)
        {
            return char.Parse(arg.ToString());
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Converts a number to a string in its equivalent hexadecimal notation. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="number">       value to parse. </param>
        /// <param name="maxNumbers">   (Optional) The maximum numbers. </param>
        ///
        /// <returns>   A string. </returns>

        public static string Hex(int number, int maxNumbers = 8)
        {
            return number.ToString("X" + maxNumbers.ToString());
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Converts a string representation of a hexadecimal number to its equivalent integer value. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="hexValue"> The hexadecimal value. </param>
        ///
        /// <returns>   An int. </returns>

        public static int unHex(string hexValue)
        {
            return int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
        }

        //String Function

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Combines an array of Strings into one String, each separated by the character(s) used for the separator parameter. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="list">         Array of Strings to be joined. </param>
        /// <param name="separator">    String to be placed between each item. </param>
        ///
        /// <returns>   The combined string. </returns>

        public static string join(string[] list, string separator)
        {
            return string.Join(separator, list);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   The split() function breaks a String into pieces using a character or string as the delimiter. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="value">    The String to be split. </param>
        /// <param name="delim">    The String used to separate the data. </param>
        ///
        /// <returns>   The string array that holds the strings. </returns>

        public static string[] split(string value, string delim)
        {
            Regex regex = new Regex(delim);
            return regex.Split(value);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   The splitTokens() function splits a String at one or many character delimiters or "tokens". </summary>
        ///
        ///
        /// <param name="value">    The String to be split. </param>
        /// <param name="delim">    (Optional) List of individual Strings that will be used as separators. </param>
        ///
        /// <returns>   A string[]. </returns>

        public static string[] splitToken(string value, string delim = null)
        {
            Regex regex = new Regex(delim + @"\t\n\r\f");
            return regex.Split(value);
        }

        //Array Functions

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Appends an value to the given array. </summary>
        ///
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="array">    The array. </param>
        /// <param name="value">    The value. </param>
        ///
        /// <returns>the new array Array. </returns>

        public static T[] append<T>(T value, params T[] array)
        {
            List<T> temp = array.Cast<T>().ToList();
            temp.Add(value);
            return temp.ToArray();
        }

        public static void printArray<T>(params T[] array)
        {
            if (array.Length == 0)
            {
                println("Array is empty!");
                return;
            }

            for (int i = 0; i < array.Length; i++)
            {
                println(string.Format("[{0}]: {1}", i, array[i]));
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Swaps 2 indexes of an arrray. </summary>
        ///
        ///
        /// <typeparam name="T">    The type of the array. </typeparam>
        /// <param name="array">    The array to swap the indexes from. </param>
        /// <param name="i">    The first index. </param>
        /// <param name="j">    The second index. </param>

        public static void swap<T>(T[] array, int i, int j)
        {
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        public static T[] Splice<T>(this T[] source, int index, int count)
        {
            var items = source.ToList().GetRange(index, count);
            source.ToList().RemoveRange(index, count);
            return items.ToArray();
        }

        public static void append<T>(this T[] source, T[] toAdd)
        {
            List<T> termsList = source.ToList();

            foreach (T item in toAdd)
            {
                termsList.Add(item);
            }

            // You can convert it back to an array if you would like to
            T[] terms = termsList.ToArray();

        }

        /// <summary>
        /// Gets the average number of the array
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns> Returns the average number.</returns>
        public static double average(params double[] source)
        {
            double average = 0;

            foreach (double item in source)
            {
                average += item;
            }

            return average / source.Length;
        }

        /// <summary>
        /// Shuffles the specified array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array to shuffle.</param>
        /// <returns> The shuffled array.</returns>
        public static T[] shuffle<T>(params T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = round(random(n--));
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
            return array;
        }
    }
}
