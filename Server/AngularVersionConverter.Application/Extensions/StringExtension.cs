﻿using System.Text;
using System.Text.RegularExpressions;

namespace AngularVersionConverter.Application.Extensions
{
    public static class StringExtension
    {
        public static IEnumerable<string> SplitAngularFileStringIntoLines(this string file)
        {
            var separatedTsFileInFunctions = file.Split('\n').SelectMany(line => line.Split(";"));

            return separatedTsFileInFunctions;
        }

        public static bool IsMatchFor(this string stringToMatch, string regexString)
        {
            var regex = new Regex(regexString);
            return regex.IsMatch(stringToMatch);
        }

        public static bool IsMatchFor(this string stringToMatch, Regex regex)
        {
            return regex.IsMatch(stringToMatch);
        }

        public static bool IsNotMatchFor(this string stringToMatch, string regexString)
        {
            return !stringToMatch.IsMatchFor(regexString);
        }

        public static Stream ToStream(this string file)
        {
            byte[] byteArray = Encoding.ASCII.GetBytes(file);
            return new MemoryStream(byteArray);
        }

    }

}
