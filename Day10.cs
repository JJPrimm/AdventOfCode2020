using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode
{
    public static class Day10
    {
        public static void Problem1()
        {
            var adapters = Utilities.ReadInts(10).OrderBy(a => a).ToArray();

            int builtIn = adapters.Max() + 3;
            adapters = adapters.Prepend(0).Append(builtIn).ToArray();
            int ones = 0;
            int threes = 0;
            for (int i = 0; i < adapters.Count() - 1; i++)
            {
                if (adapters[i+1] - adapters[i] == 1)
                {
                    ones++;
                }
                if (adapters[i + 1] - adapters[i] == 3)
                {
                    threes++;
                }
            }
            adapters.ToList().ForEach(a => Console.WriteLine(a));

            Console.WriteLine($"{ones} ones times {threes} threes is {ones * threes}.");
        }

        public static void Problem2()
        {
            var adapters = Utilities.ReadInts(10).OrderBy(a => a).ToArray();

            int builtIn = adapters.Max() + 3;
            adapters = adapters.Prepend(0).Append(builtIn).ToArray();

            DateTime start = DateTime.Now;

            long[] paths = { 1, 1 };
            for (int ptr = 2; ptr < adapters.Length; ptr++)
            {
                long branches = paths[ptr - 1];
                if (adapters[ptr - 2] >= adapters[ptr] - 3)
                {
                    branches += paths[ptr - 2];
                    if (ptr >= 3 && adapters[ptr - 3] >= adapters[ptr] - 3)
                    {
                        branches += paths[ptr - 3];
                    }
                }
                paths = paths.Append(branches).ToArray();
            }
            var time = DateTime.Now - start;
            Console.WriteLine(paths.Max());
            Debug.WriteLine(time.TotalMilliseconds);
        }

        //Ended up not using this. It was just too slow.
        public static long FindPaths(int joltage, IEnumerable<int> adapters)
        {
            if (joltage == adapters.Max())
            {
                return 1;
            }
            long paths = 0;
            foreach (var validAdapter in adapters.Where(a => a > joltage && a <= joltage + 3))
            {
                paths += FindPaths(validAdapter, adapters);
            }
            return paths;
        }
    }
}
