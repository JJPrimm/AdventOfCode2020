using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    public class Day02
    {
        public static void Problem1()
        {
            var input = Utilities.ReadStrings(2);
            var counter = 0;
            foreach (var pwdRule in input)
            {
                int ruleMin = Int32.Parse(pwdRule.Split('-')[0]);
                int ruleMax = Int32.Parse(pwdRule.Split(' ')[0].Split('-')[1]);
                char letter = pwdRule[pwdRule.IndexOf(':') - 1];
                string pwd = pwdRule.Split(' ')[2];

                int occurrences = pwd.Length - pwd.Replace(letter.ToString(), "").Length;
                if (occurrences >= ruleMin && occurrences <= ruleMax)
                {
                    counter++;
                }
            }
            Console.WriteLine(counter);
        }

        public static void Problem2()
        {
            var input = Utilities.ReadStrings(2);
            var counter = 0;
            foreach (var pwdRule in input)
            {
                int ptr1 = Int32.Parse(pwdRule.Split('-')[0]) - 1;
                int ptr2 = Int32.Parse(pwdRule.Split(' ')[0].Split('-')[1]) - 1;
                char letter = pwdRule[pwdRule.IndexOf(':') - 1];
                string pwd = pwdRule.Split(' ')[2];

                if ((pwd[ptr1] == letter && pwd[ptr2] != letter) || (pwd[ptr1] != letter && pwd[ptr2] == letter))
                {
                    counter++;
                }
            }
            Console.WriteLine(counter);
        }
    }
}
