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

namespace CCreative
{
    public static class Data
    {
        //conversion

        ///<summary>
        ///Returns a converted float number.
        ///</summary>
        public static float Float<T>(T arg)
        {
            return float.Parse(arg.ToString());
        }

        ///<summary>
        ///Returns a converted interger number.
        ///</summary>
        public static int Int<T>(T arg)
        {
            return int.Parse(arg.ToString());
        }

        ///<summary>
        ///Returns a converted string.
        ///</summary>
        public static string str<T>(T arg)
        {
            return arg.ToString();
        }

        ///<summary>
        ///Returns a converted boolean.
        ///</summary>
        public static bool Boolean<T>(T arg)
        {
            return bool.Parse(arg.ToString());
        }

        ///<summary>
        ///Returns a converted byte number.
        ///</summary>
        public static byte Byte<T>(T arg)
        {
            return byte.Parse(arg.ToString());
        }

        ///<summary>
        ///Returns a char array from the conversion
        ///</summary>
        public static char Char<T>(T arg)
        {
            return char.Parse(arg.ToString());
        }

        ///<summary>
        ///Returns a converted hex value
        ///</summary>
        public static string Hex(int number, int maxNumbers = 8)
        {
            return number.ToString("X" + maxNumbers.ToString());
        }

        ///<summary>
        ///Convert hex to a interger
        ///</summary>
        public static int unHex(string hexValue)
        {
            return int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
        }

        //String Function

        ///<summary>
        ///Returns a new array that is joined
        ///</summary>
        public static string join(string[] list, string separator)
        {
            return string.Join(separator, list);
        }

        public static string[] split(string value, string delim)
        {
            Regex regex = new Regex(delim);
            return regex.Split(value);
        }

        public static string[] splitToken(string value, string delim = null)
        {
            Regex regex = new Regex(delim + @"\t\n\r\f");
            return regex.Split(value);
        }

        //Array Functions
        public static Array append<T>(Array array, T value)
        {
            List<T> temp = array.Cast<T>().ToList();
            temp.Add(value);
            return temp.ToArray();
        }
    }
}
