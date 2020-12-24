using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode
{
    public static class Day19
    {
        private static Dictionary<int, Validation> rules;

        public static void Problem1()
        {
            var input = Utilities.ReadStrings(19, false);
            rules = new Dictionary<int, Validation>();

            foreach (var rule in input.Where(i => i.Contains(':')))
            {
                int index = Convert.ToInt32(rule.Split(':')[0]);
                rules[index] = new Validation(rule.Split(": ")[1]);
            }

            var counter = 0;
            rules[0].BuildOptions();
            foreach (var message in input.Where(i => !i.Contains(':')))
            {
                if (rules[0].ValidOptions.Contains(message))
                {
                    counter++;
                }
            }
            Console.WriteLine(counter);
        }

        public static void Problem2()
        {
            bool useTestData = false;

            var input = Utilities.ReadStrings(19, useTestData);
            rules = new Dictionary<int, Validation>();

            foreach (var rule in input.Where(i => i.Contains(':')))
            {
                int index = Convert.ToInt32(rule.Split(':')[0]);
                rules[index] = new Validation(rule.Split(": ")[1]);
            }

            var counter = 0;
            rules[0].BuildOptions();
            var loopLength = rules[42].ValidOptions.First().Length;
            foreach (var message in input.Where(i => !i.Contains(':')))
            {
                if (rules[0].ValidOptions.Contains(message))
                {
                    counter++;
                }
                else if (message.Length > loopLength * 3 && message.Length % loopLength == 0)
                {
                    bool isMatch = true;
                    int fortyTwos = 0;
                    int thirtyOnes = 0;
                    for (int i = 0; i < message.Length; i += loopLength)
                    {
                        if(thirtyOnes == 0 && rules[42].ValidOptions.Contains(message.Substring(i, loopLength)))
                        {
                            fortyTwos++;
                        }
                        else if (rules[31].ValidOptions.Contains(message.Substring(i, loopLength)))
                        {
                            thirtyOnes++;
                        }
                        else
                        {
                            isMatch = false;
                            break;
                        }
                    }
                    if (isMatch && thirtyOnes >= 1 && fortyTwos > thirtyOnes)
                    {
                        counter++;
                    }
                }
            }
            Console.WriteLine(counter);
        }

        public class Validation
        {
            public string Match { get; set; }
            public int[] Opt1 { get; set; }
            public int[] Opt2 { get; set; }
            public List<string> ValidOptions { get; set; }

            public Validation(string input)
            {
                if (input.Contains('"'))
                {
                    Match = input.Split('"')[1];
                }
                else
                {
                    Opt1 = input.Split(" | ")[0].Split(' ').Select(v => Convert.ToInt32(v)).ToArray();
                    if (input.Contains(" | "))
                    {
                        Opt2 = input.Split(" | ")[1].Split(' ').Select(v => Convert.ToInt32(v)).ToArray();
                    }
                }
            }

            public void BuildOptions()
            {
                if (ValidOptions == null)
                {
                    ValidOptions = new List<string>();
                    if (Match != null)
                    {
                        ValidOptions.Add(Match.ToString());
                    }
                    else
                    {
                        foreach (var rule in Opt1)
                        {
                            rules[rule].BuildOptions();
                        }
                        ColateRules(Opt1);

                        if (Opt2 != null)
                        {
                            foreach (var rule in Opt2)
                            {
                                rules[rule].BuildOptions();
                            }
                            ColateRules(Opt2);
                        }
                    }
                }
            }

            private void ColateRules(IEnumerable<int> subRules, string prefix = "")
            {
                foreach (var rule in rules[subRules.First()].ValidOptions)
                {
                    if (subRules.Count() == 1)
                    {
                        ValidOptions.Add(String.Concat(prefix, rule));
                    }
                    else
                    {
                        ColateRules(subRules.Skip(1), String.Concat(prefix, rule));
                    }
                }
            }
        }
    }
}
