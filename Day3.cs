using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public class Day3
    {
        public static void Problem1()
        {
            var slope = Utilities.ReadStringArray(3).ToArray();
            
            Console.WriteLine(TreeCount(slope, 3, 1));
        }

        public static void Problem2()
        {
            var slope = Utilities.ReadStringArray(3).ToArray();
            long trees = TreeCount(slope, 1, 1);
            trees *= TreeCount(slope, 3, 1);
            trees *= TreeCount(slope, 5, 1);
            trees *= TreeCount(slope, 7, 1);
            trees *= TreeCount(slope, 1, 2);

            Console.WriteLine(trees);
        }

        private static int TreeCount(string[] slope, int r, int d)
        {
            int col = 0;
            int trees = 0;
            int row = 0;
            int width = slope[0].Length;
            while (row < slope.Count())
            {
                if (slope[row][col] == '#')
                {
                    trees++;
                }
                row += d;
                col = (col + r) % width;
            }
            return trees;
        }
    }
}
