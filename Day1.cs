using System;
using System.Linq;

namespace AdventOfCode
{
    public static class Day1
    {
        public static void Problem1()
        {
            var expenses = Utilities.ReadIntArray(1).ToArray();
            for (int outer = 0; outer < expenses.Count(); outer++)
            {
                for (int inner = 0; inner < expenses.Count(); inner++)
                {
                    if (inner != outer && (expenses[inner] + expenses[outer] == 2020))
                    {
                        var answer = expenses[inner] * expenses[outer];
                        Console.WriteLine(answer);
                        return;
                    }
                }
            }
            Console.WriteLine("No answer found.");
        }
        public static void Problem2()
        {
            var expenses = Utilities.ReadIntArray(1).ToArray();
            for (int outer = 0; outer < expenses.Count(); outer++)
            {
                for (int mid = 0; mid < expenses.Count(); mid++)
                {
                    for (int inner = 0; inner < expenses.Count(); inner++)
                    {
                        if (inner != outer && (expenses[inner] + expenses[mid] + expenses[outer] == 2020))
                        {
                            var answer = expenses[inner] * expenses[outer] * expenses[mid];
                            Console.WriteLine(answer);
                            return;
                        }
                    }
                }
            }
            Console.WriteLine("No answer found.");
        }
    }
}
