using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode
{
    public static class Day15
    {
        public static void Problem1()
        {
            var input = Utilities.ReadStringArray(15).First().Split(',').Select(i => Convert.ToInt32(i));

            int turn = 1;
            int lastNum = 0;
            Dictionary<int, List<int>> answers = new Dictionary<int, List<int>>();
            foreach (int num in input)
            {
                answers[num] = new List<int> { turn };
                lastNum = num;
                turn++;
            }


            while (true)
            {
                int newAnswer = 0;
                if (answers[lastNum].Count() > 1)
                {
                    newAnswer = turn - 1 - answers[lastNum].Where(t => t != turn - 1).Max();
                }

                if (answers.ContainsKey(newAnswer))
                {
                    answers[newAnswer].Add(turn);
                }
                else
                {
                    answers[newAnswer] = new List<int> { turn };
                }

                if (turn == 2020)
                {
                    Console.WriteLine(newAnswer);
                    return;
                }
                lastNum = newAnswer;
                turn++;
            }
        }

        public static void Problem2()
        {
            var input = Utilities.ReadStringArray(15).First().Split(',').Select(i => Convert.ToInt32(i));

            int turn = 1;
            int lastNum = 0;
            Dictionary<int, int> answers = new Dictionary<int, int>();
            foreach (int num in input)
            {
                answers[num] =  turn;
                lastNum = num;
                turn++;
            }


            while (true)
            {
                int newAnswer = 0;
                if (answers.ContainsKey(lastNum))
                {
                    newAnswer = turn - 1 - answers[lastNum];
                }
                answers[lastNum] = turn - 1;

                if (turn == 30000000)
                {
                    Console.WriteLine(newAnswer);
                    return;
                }
                lastNum = newAnswer;
                turn++;
            }
        }
    }
}
