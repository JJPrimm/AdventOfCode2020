﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace AdventOfCode
{
    public static class Utilities
    {
        public static IEnumerable<int> ReadIntArray(int day)
        {
            var fileName = @$"..\..\..\input\Day{day}.txt";
            return File.ReadAllLines(fileName).Select(x => Convert.ToInt32(x));
        }
    }
}
