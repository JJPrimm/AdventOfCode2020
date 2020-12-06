using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day6
    {
        public static void Problem1()
        {
            var input = Utilities.ReadStringArray(6);
            int sum = 0;
            var groupAnswers = new List<char>();
            foreach(var line in input)
            {
                if (line != string.Empty)
                {
                    groupAnswers.AddRange(line);
                }
                else
                {
                    sum += groupAnswers.Distinct().Count();
                    groupAnswers = new List<char>();
                }
            }
            sum += groupAnswers.Distinct().Count();

            Console.WriteLine(sum);
        }

        public static void Problem2()
        {
            var input = Utilities.ReadStringArray(6);
            int sum = 0;
            var groupAnswers = new List<char>();
            bool newGroup = true;
            foreach (var line in input)
            {
                if (line != string.Empty)
                {
                    if (newGroup)
                    {
                        groupAnswers.AddRange(line);
                        newGroup = false;
                    }
                    else
                    {
                        groupAnswers = groupAnswers.Intersect(line).ToList();
                    }
                }
                else
                {
                    sum += groupAnswers.Distinct().Count();
                    groupAnswers = new List<char>();
                    newGroup = true;
                }
            }
            sum += groupAnswers.Distinct().Count();

            Console.WriteLine(sum);
        }
    }

}
