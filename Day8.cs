using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode
{
    public static class Day8
    {
        public static void Problem1()
        {
            var Lines = Utilities.ReadStringArray(8)
                .Select(i => new Line(i))
                .ToArray();

            List<int> executedLines = new List<int> ();
            int ptr = 0;
            int accumulator = 0;

            while (!executedLines.Contains(ptr))
            {
                executedLines.Add(ptr);
                switch (Lines[ptr].Cmd)
                {
                    case "acc":
                        accumulator += Lines[ptr].Offset;
                        ptr++;
                        break;
                    case "jmp":
                        ptr += Lines[ptr].Offset;
                        break;
                    case "nop":
                        ptr++;
                        break;
                }
            }
            Console.WriteLine(accumulator);
        }

        public static void Problem2()
        {
            var Lines = Utilities.ReadStringArray(8)
                .Select(i => new Line(i))
                .ToArray();

            int fixPtr = 0;

            while (fixPtr < Lines.Length)
            {
                while (Lines[fixPtr].Cmd != "nop" && Lines[fixPtr].Cmd != "jmp")
                {
                    fixPtr++;
                }

                int ptr = 0;
                int accumulator = 0;
                List<int> executedLines = new List<int>();

                while (!executedLines.Contains(ptr))
                {
                    executedLines.Add(ptr);
                    switch (Lines[ptr].Cmd)
                    {
                        case "acc":
                            accumulator += Lines[ptr].Offset;
                            ptr++;
                            break;
                        case "jmp":
                            ptr += (ptr == fixPtr) ? 1 : Lines[ptr].Offset;
                            break;
                        case "nop":
                            ptr += (ptr == fixPtr) ? Lines[ptr].Offset : 1;
                            break;
                    }
                    if (ptr == Lines.Length)
                    {
                        Console.WriteLine(accumulator);
                        return;
                    }
                }
                fixPtr++;
            }
        }
    }

    public class Line
    {
        public string Cmd { get; set; }
        public int Offset { get; set; }
        public Line(string input)
        {
            Cmd = input.Split(' ')[0];
            Offset = Convert.ToInt32(input.Split(' ')[1].Replace("+", ""));
        }
    }
}
