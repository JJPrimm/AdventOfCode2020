using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode
{
    public static class Day14
    {
        public static void Problem1()
        {
            var input = Utilities.ReadStringArray(14);

            string mask = "";
            var memory = new List<Memory>();
            foreach(var line in input)
            {
                if (line.StartsWith("mask"))
                {
                    mask = line.Replace("mask = ", "");
                }
                else
                {
                    var address = Convert.ToInt32(line.Split('[')[1].Split(']')[0]);
                    var value = Convert.ToInt64(line.Split(' ')[2]);
                    if (!memory.Any(m => m.Address == address))
                    {
                        memory.Add(new Memory { Address = address, Value = value });
                    }
                    else
                    {
                        memory.Find(m => m.Address == address).Value = value;
                    }
                    memory.Find(m => m.Address == address).ApplyMask(mask);
                }
            }
            Console.WriteLine(memory.Sum(m => m.Value));
        }

        public static void Problem2()
        {
            var input = Utilities.ReadStringArray(14);

            string mask = "";
            var memory = new List<Memory>();
            foreach (var line in input)
            {
                if (line.StartsWith("mask"))
                {
                    mask = line.Replace("mask = ", "");
                }
                else
                {
                    var addressInput = Convert.ToInt64(line.Split('[')[1].Split(']')[0]);
                    var addresses = ApplyMask(addressInput, mask);
                    var value = Convert.ToInt64(line.Split(' ')[2]);

                    foreach (var address in addresses)
                    {
                        if (!memory.Any(m => m.Address == address))
                        {
                            memory.Add(new Memory { Address = address, Value = value });
                        }
                        else
                        {
                            memory.Find(m => m.Address == address).Value = value;
                        }
                    }
                }
            }
            Console.WriteLine(memory.Sum(m => m.Value));
        }

        private static IEnumerable<long> ApplyMask(long value, string mask)
        {
            List<StringBuilder> newValues = new List<StringBuilder>();
            newValues.Add(new StringBuilder());
            var binValue = Convert.ToString(value, 2).PadLeft(36, '0');
            for (int i = 0; i < 36; i++)
            {
                switch (mask[i])
                {
                    case '0':
                        foreach (var newValue in newValues)
                        {
                            newValue.Append(binValue[i]);
                        }
                        break;
                    case '1':
                        foreach (var newValue in newValues)
                        {
                            newValue.Append('1');
                        }
                        break;
                    case 'X':
                        List<StringBuilder> newValues1s = new List<StringBuilder>();
                        foreach (var newValue in newValues)
                        {
                            var newValue1 = new StringBuilder(newValue.ToString());
                            newValue1.Append('1');
                            newValues1s.Add(newValue1);
                            newValue.Append('0');
                        }
                        newValues.AddRange(newValues1s);
                        break;
                }
            }
            return newValues.Select(v => Convert.ToInt64(v.ToString(), 2));
        }
    }

    public class Memory
    {
        public long Address { get; set; }
        public long Value { get; set; }

        public void ApplyMask(string mask)
        {
            var binValue = Convert.ToString(Value, 2).PadLeft(36, '0');
            StringBuilder newValue = new StringBuilder();
            for (int i = 0; i < 36; i++)
            {
                newValue.Append((mask[i] != 'X') ? mask[i] : binValue[i]);
            }
            Value = Convert.ToInt64(newValue.ToString(), 2);
        }
    }
}
