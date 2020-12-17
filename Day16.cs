using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode
{
    public static class Day16
    {
        public static void Problem1()
        {
            var input = Utilities.ReadStringArray(16).ToArray();

            int ptr = 0;
            List<Rule> rules = new List<Rule>();
            while (input[ptr] != "")
            {
                rules.Add(new Rule(input[ptr]));
                ptr++;
            }

            ptr += 5;
            long failures = 0;
            while (ptr < input.Length)
            {
                var nums = input[ptr].Split(',').Select(n => Convert.ToInt32(n));
                foreach (var num in nums)
                {
                    if (!rules.Any(r => r.InRange(num)))
                    {
                        failures += num;
                    }
                }
                ptr++;
            }

            Console.WriteLine(failures);
        }

        public static void Problem2()
        {
            var input = Utilities.ReadStringArray(16).ToArray();

            int ptr = 0;
            List<Rule> rules = new List<Rule>();
            while (input[ptr] != "")
            {
                rules.Add(new Rule(input[ptr]));
                ptr++;
            }

            ptr +=2;
            var myTicket = input[ptr].Split(',').Select(n => Convert.ToInt32(n)).ToArray();

            ptr += 3;
            while (ptr < input.Length)
            {
                var nums = input[ptr].Split(',').Select(n => Convert.ToInt32(n)).ToArray();
                for (int i = 0; i < nums.Length; i++)
                {
                    if (rules.Any(r => r.InRange(nums[i])))
                    {
                        rules.ForEach(r => r.CheckValidPositions(nums[i], i));
                    }
                }
                ptr++;
            }

            List<int> foundPositions = new List<int>();
            while (rules.Exists(r => r.Position == -1))
            {
                for (int i = 0; i < myTicket.Length; i++)
                {
                    if (!foundPositions.Any(p => p == i))
                    {
                        rules.Where(r => r.InvalidPositions.Count == myTicket.Length - 1 && !r.InvalidPositions.Any(p => p == i))
                            .ToList()
                            .ForEach(r => r.Position = i);

                        if (rules.Any(r => r.Position == i))
                        {
                            foundPositions.Add(i);
                            rules.Where(r => r.Position == -1)
                                .ToList()
                                .ForEach(r => r.AddInvalidPosition(i));
                        }
                    }
                }
            }

            long answer = 1;

            for (int i = 0; i < myTicket.Length; i++)
            {
                var rule = rules.Find(r => r.Position == i);
                if (rule.Name.StartsWith("departure"))
                {
                    answer *= myTicket[i];
                }
            }

            Console.WriteLine(answer);
        }
    }

    public class Rule
    {
        public string Name { get; set; }
        public int Min1 { get; set; }
        public int Max1 { get; set; }
        public int Min2 { get; set; }
        public int Max2 { get; set; }
        public List<int> InvalidPositions { get; set; } = new List<int>();
        public int Position { get; set; } = -1;
        public Rule(string input)
        {
            Name = input.Split(':')[0];
            Min1 = Convert.ToInt32(input.Split(": ")[1].Split('-')[0]);
            Max1 = Convert.ToInt32(input.Split(": ")[1].Split('-')[1].Split(' ')[0]);
            Min2 = Convert.ToInt32(input.Split(" or ")[1].Split('-')[0]);
            Max2 = Convert.ToInt32(input.Split(" or ")[1].Split('-')[1]);
        }

        public bool InRange(int num)
        {
            return (num >= Min1 && num <= Max1) || (num >= Min2 && num <= Max2);
        }

        public void CheckValidPositions(int num, int position)
        {
            var inRange = (num >= Min1 && num <= Max1) || (num >= Min2 && num <= Max2);
            if (!inRange)
            {
                AddInvalidPosition(position);
            }
            return;
        }

        public void AddInvalidPosition(int position)
        {
            if (!InvalidPositions.Any(p => p == position))
            {
                InvalidPositions.Add(position);
            }
        }
    }
}
