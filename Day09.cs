using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode
{
    public static class Day09
    {
        public static void Problem1()
        {
            var input = Utilities.ReadLongs(9).ToArray();
            int preamble = 25;

            var sumPtr = preamble;
            while (sumPtr < input.Count())
            {
                bool validFound = false;
                for (int outer = sumPtr - preamble; outer < sumPtr - 1; outer++)
                {
                    for (int inner = sumPtr - preamble + 1; inner < sumPtr; inner++)
                    {
                        if (inner != outer)
                        {
                            if (input[inner] + input[outer] == input[sumPtr])
                            {
                                validFound = true;
                                break;
                            }
                        }
                    }
                    if (validFound)
                    {
                        break;
                    }
                }
                if (!validFound)
                {
                    Console.WriteLine(input[sumPtr]);
                }
                sumPtr++;
            }
        }


        public static void Problem2()
        {
            var input = Utilities.ReadLongs(9).ToArray();
            long target = 20874512; // answer to Problem1

            for (int minPtr = 0; minPtr < input.Length - 1; minPtr++)
            {
                long sum = input[minPtr];
                long smallest = input[minPtr];
                long largest = input[minPtr];
                for (int maxPtr = minPtr + 1; maxPtr < input.Length; maxPtr++)
                {
                    sum += input[maxPtr];
                    smallest = (input[maxPtr] < smallest) ? input[maxPtr] : smallest;
                    largest = (input[maxPtr] > largest) ? input[maxPtr] : largest;
                    if (sum == target)
                    {
                        var minPlusMax = smallest + largest;
                        Console.WriteLine(minPlusMax);
                        return;
                    }
                }
            }
            Console.WriteLine("Not Found");
        }
    }
}
