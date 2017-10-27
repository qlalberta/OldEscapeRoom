using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace EscapeText
{

    public static class StringExtension // you can add .CapsUp() or .CapsDown() to the end of a string to change the case
    {
        public static string CapsDown(this string source)
        {
            if (source.Length > 0) return (source.Substring(0, 1).ToLower() + source.Substring(1));
            else return source;
        }

        public static string CapsUp(this string source)
        {
            if (source.Length > 0) return (source.Substring(0, 1).ToUpper() + source.Substring(1));
            else return source;
        }

        public static string CapsA(this string source)
        {
            source = IsolateSubject(source);
            char first = source.ToLower()[0];
            if (new char[] { 'a', 'e', 'i', 'o', 'u' }.Contains(first)) return ("An " + source);
            return "A " + source;
        }

        public static string LowA(this string source)
        {
            source = IsolateSubject(source);
            char first = source.ToLower()[0];
            if (new char[] { 'a', 'e', 'i', 'o', 'u' }.Contains(first)) return ("an " + source);
            return "a " + source;
        }

        public static string LowThe(this string source)
        {
            return "the " + IsolateSubject(source);
        }

        public static string CapThe(this string source)
        {
            return "The " + IsolateSubject(source);
        }

        public static string IsolateSubject(this string s)
        {
            try
            {
                if (s.ToLower().Substring(0, 2) == "a ") return s.Substring(2);
                if (s.ToLower().Substring(0, 3) == "an ") return s.Substring(3);
                if (s.ToLower().Substring(0, 4) == "the ") return s.Substring(4);
            }
            catch {; }
            return s;
        }
    }
}