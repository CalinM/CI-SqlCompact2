using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Common
{
    public static class StringBuilderExtension
    {
        // Adds IndexOf, Substring, AreEqual to the StringBuilder class.
        public static int IndexOf(this StringBuilder theStringBuilder, string value)
        {
            const int NOT_FOUND = -1;

            if (theStringBuilder == null || string.IsNullOrEmpty(value))
                return NOT_FOUND;

            var count = theStringBuilder.Length;
            var len = value.Length;

            if (count < len)
                return NOT_FOUND;

            var loopEnd = count - len + 1;
            for (var loop = 0; loop < loopEnd; loop++)
            {
                var found = true;
                for (var innerLoop = 0; innerLoop < len; innerLoop++)
                {
                    if (theStringBuilder[loop + innerLoop] != value[innerLoop])
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                {
                    return loop;
                }
            }

            return NOT_FOUND;
        }

        public static int IndexOf(this StringBuilder theStringBuilder, string value, int startPosition)
        {
            const int NOT_FOUND = -1;

            if (theStringBuilder == null || string.IsNullOrEmpty(value))
                return NOT_FOUND;

            var count = theStringBuilder.Length;
            var len = value.Length;

            if (count < len)
                return NOT_FOUND;

            var loopEnd = count - len + 1;
            if (startPosition >= loopEnd)
                return NOT_FOUND;

            for (var loop = startPosition; loop < loopEnd; loop++)
            {
                var found = true;
                for (var innerLoop = 0; innerLoop < len; innerLoop++)
                {
                    if (theStringBuilder[loop + innerLoop] != value[innerLoop])
                    {
                        found = false;
                        break;
                    }
                }

                if (found)
                {
                    return loop;
                }
            }

            return NOT_FOUND;
        }

        public static string Substring(this StringBuilder theStringBuilder, int startIndex, int length)
        {
            return theStringBuilder == null ? null : theStringBuilder.ToString(startIndex, length);
        }

        public static bool AreEqual(this StringBuilder theStringBuilder, string compareString)
        {
            if (theStringBuilder == null || compareString == null)
                return compareString == null;

            var len = theStringBuilder.Length;

            if (len != compareString.Length)
                return false;

            for (var loop = 0; loop < len; loop++)
            {
                if (theStringBuilder[loop] != compareString[loop])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Compares one string to part of another string.
        /// </summary>
        /// <param name="haystack"></param>
        /// <param name="needle">Needle to look for</param>
        /// <param name="position">Looks to see if the needle is at position in haystack</param>
        /// <returns>Substring(theStringBuilder,offset,compareString.Length) == compareString</returns>
        public static bool IsStringAt(this StringBuilder haystack, string needle, int position)
        {
            if (haystack == null)
                return needle == null;

            if (needle == null)
                return false;

            var len = haystack.Length;
            var compareLen = needle.Length;

            if (len < compareLen + position)
                return false;

            for (var loop = 0; loop < compareLen; loop++)
            {
                if (haystack[loop + position] != needle[loop])
                {
                    return false;
                }
            }

            return true;
        }

        //this will mess up the non-tags strings placed between <>
        //if neccesarry, replace with a static list of html tags
        public static string StripHtml(this string input)
        {
            // Will this simple expression replace all tags???
            var tagsExpression = new Regex(@"</?.+?>");
            return tagsExpression.Replace(input, " ");
        }
    }
}
