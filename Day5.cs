using System;
using System.Linq;

namespace AdventOfCode
{
    public static class Day5
    {
        public static void Problem1()
        {
            var input = Utilities.ReadStringArray(5).ToList();

            var boardingPasses = input
                .Select(p => Convert.ToInt32(new string(p.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1')), 2));

            var max = boardingPasses.Max();

            Console.WriteLine(max);
        }

        public static void Problem2()
        {
            var input = Utilities.ReadStringArray(5).ToList();

            var boardingPasses = input
                .Select(p => Convert.ToInt32(new string(p.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1')), 2));

            for (int seat = 1; seat < 1023; seat++)
            {
                if (!boardingPasses.Contains(seat) && boardingPasses.Contains(seat - 1) && boardingPasses.Contains(seat + 1))
                {
                    Console.WriteLine(seat);
                    return;
                }
            }

            Console.WriteLine("Seat not found");
        }
    }
}
