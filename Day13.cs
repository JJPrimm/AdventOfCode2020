using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode
{
    public static class Day13
    {
        public static void Problem1()
        {
            var input = Utilities.ReadStrings(13).ToArray();
            var curTime = Convert.ToInt64(input[0]);
            var busses = input[1]
                .Split(',')
                .Where(b => b != "x")
                .Select(b => Convert.ToInt32(b))
                .ToArray();

            var waits = busses.Select(b => b - (curTime % b)).ToArray();
            var minWait = waits.Min();

            var bus = busses[Array.IndexOf(waits, minWait)];

            Console.WriteLine(minWait * bus);
        }

        public static void Problem2()
        {
            var input = Utilities.ReadStrings(13).ToArray()[1].Split(',');
            var busses = input
                .Where(b => b != "x")
                .Select(b => new Bus { number = Convert.ToInt32(b), delay = Array.IndexOf(input, b) })
                .OrderByDescending(b => b.number)
                .ToArray();

            long t = 0 - busses[0].delay;
            long delta = busses[0].number;
            int busPtr = 1;
            while (busPtr < busses.Length)
            {
                t += delta;
                if (busses[busPtr].HasArrival(t))
                {
                    delta *= busses[busPtr].number;
                    busPtr++;
                }

            }
            Console.WriteLine(t);
        }

        public class Bus
        {
            public int number;
            public int delay;

            public bool HasArrival(long time)
            {
                return (time + delay) % number == 0;
            }
        }
    }
}
