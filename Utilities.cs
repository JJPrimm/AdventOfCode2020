using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace AdventOfCode
{
    public static class Utilities
    {
        public static IEnumerable<int> ReadInts(int day, bool useTest = false)
        {
            
            return File.ReadAllLines(fileName(day, useTest)).Select(x => Convert.ToInt32(x));
        }

        public static IEnumerable<long> ReadLongs(int day, bool useTest = false)
        {
            return File.ReadAllLines(fileName(day, useTest)).Select(x => Convert.ToInt64(x));
        }

        public static IEnumerable<string> ReadStrings(int day, bool useTest = false)
        {
            return File.ReadAllLines(fileName(day, useTest));
        }

        private static string fileName(int day, bool useTest)
        {
            var dayString = (useTest) ? day.ToString("00t") : day.ToString("00");
            return @$"..\..\..\input\Day{dayString}.txt";
        }
    }
}
