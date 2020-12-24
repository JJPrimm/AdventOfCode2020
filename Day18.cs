using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode
{
    public static class Day18
    {
        public static void Problem1()
        {
            var input = Utilities.ReadStrings(18).ToList();
            long sum = 0;

            input.ForEach(p => sum += DoMath1(p));

            Console.WriteLine(sum);
        }

        public static void Problem2()
        {
            bool test = false;

            var input = Utilities.ReadStrings(18, test).ToList();
            long sum = 0;

            input.ForEach(p =>
            {
                long answer = DoMath2(p);
                sum += answer;
                Console.WriteLine(String.Concat(answer.ToString().PadRight(25, ' '), sum.ToString()));

            });

            Console.WriteLine(sum);
        }

        public static long DoMath1(string problem)
        {
            long total = 0;
            long argument = 0;
            char operation = '+';
            int ptr = 0;
            while (ptr < problem.Length)
            {
                bool skipMath = false;
                switch (problem[ptr])
                {
                    case ' ':
                        skipMath = true;
                        ptr++;
                        break;
                    case '+':
                        operation = '+';
                        skipMath = true;
                        ptr++;
                        break;
                    case '*':
                        operation = '*';
                        skipMath = true;
                        ptr++;
                        break;
                    case '(':
                        StringBuilder subset = new StringBuilder();
                        int i = ptr + 1;
                        int p = 1;
                        while (true)
                        {
                            if (problem[i] == ')' && p == 1)
                            {
                                break;
                            }
                            if (problem[i] == ')')
                            {
                                p -= 1;
                            }
                            if (problem[i] == '(')
                            {
                                p += 1;
                            }
                            subset.Append(problem[i]);
                            i++;
                        }
                        argument = DoMath1(subset.ToString());
                        ptr += subset.Length + 2;
                        break;
                    default:
                        var argumentString = problem.Substring(ptr).Split(' ')[0];
                        argument = Convert.ToInt64(argumentString);
                        ptr += argumentString.Length;
                        break;
                }
                
                if (!skipMath)
                {
                    total = (operation == '+') ? total + argument : total * argument;
                }
            }
            return total;
        }

        public static long DoMath2(string problem)
        {
            if (!problem.Contains('('))
            {
                return DoSimpleMath(problem);
            }

            StringBuilder simplifiedProblem = new StringBuilder();
            int ptr = 0;
            while (ptr < problem.Length)
            {
                if (problem[ptr] == '(')
                {
                    StringBuilder subset = new StringBuilder();
                    var i = ptr + 1;
                    int p = 1;
                    while (true)
                    {
                        if (problem[i] == ')' && p == 1)
                        {
                            break;
                        }
                        if (problem[i] == ')')
                        {
                            p -= 1;
                        }
                        if (problem[i] == '(')
                        {
                            p += 1;
                        }
                        subset.Append(problem[i]);
                        i++;
                    }
                    simplifiedProblem.Append(DoMath2(subset.ToString()));
                    ptr += subset.Length + 2; 
                }
                else
                {
                    simplifiedProblem.Append(problem[ptr]);
                    ptr++;
                }
            }
            return DoSimpleMath(simplifiedProblem.ToString());
        }

        public static long DoSimpleMath(string problem)
        {
            while (problem.Contains('+'))
            {
                var problemAry = problem.Split(' ');
                var plusIndex = Array.IndexOf(problemAry, "+");
                var additionProblem = String.Concat(problemAry[plusIndex - 1], " ", problemAry[plusIndex], " ", problemAry[plusIndex + 1]);
                var sum = Convert.ToInt64(problemAry[plusIndex - 1]) + Convert.ToInt64(problemAry[plusIndex + 1]);
                var additionProblemIndex = problem.IndexOf(additionProblem);
                problem = String.Concat(problem.Substring(0, additionProblemIndex), sum.ToString(), problem.Substring(additionProblemIndex + additionProblem.Length)); 
            }

            long total = 1;

            problem.Split(" * ").Select(a => Convert.ToInt64(a)).ToList().ForEach(a => total *= a);

            return total;
        }
    }
}
