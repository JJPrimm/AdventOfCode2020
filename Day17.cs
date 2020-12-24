using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode
{
    public static class Day17
    {
        public static void Problem1()
        {
            const int cycles = 6;

            var input = Utilities.ReadStrings(17).ToArray();

            List<Point> points = new List<Point>();
            for (int y = -cycles; y < input.Count() + cycles; y++)
            {
                for (int x = -cycles; x < input[0].Length + cycles; x++)
                {
                    for (int z = -cycles; z <= cycles; z++)
                    {
                        Point point = new Point { x = x, y = y, z = z };
                        if (z == 0 && x >= 0 && x < input[0].Length && y >= 0 && y < input.Length)
                        {
                            point.CurrentState = (input[y][x] == '#') ? true : false;
                        }
                        points.Add(point);
                    }
                }
            }

            Console.WriteLine(points.Count(p => p.CurrentState));
            for (int cycle = 0; cycle < cycles; cycle++)
            {
                points.ForEach(p => p.Cycle(points));
                points.ForEach(p => p.Init());
                Console.WriteLine(points.Count(p => p.CurrentState));
            }
        }

        public static void Problem2()
        {
            const int cycles = 6;

            var input = Utilities.ReadStrings(17).ToArray();

            List<Point> points = new List<Point>();
            for (int y = -cycles; y < input.Count() + cycles; y++)
            {
                for (int x = -cycles; x < input[0].Length + cycles; x++)
                {
                    for (int z = -cycles; z <= cycles; z++)
                    {
                        for (int w = -cycles; w <= cycles; w++)
                        {
                            Point point = new Point { x = x, y = y, z = z, w = w };
                            if (w == 0 && z == 0 && x >= 0 && x < input[0].Length && y >= 0 && y < input.Length)
                            {
                                point.CurrentState = (input[y][x] == '#') ? true : false;
                            }
                            points.Add(point);
                        }
                    }
                }
            }

            Console.WriteLine($"Cycle 0 - {points.Count(p => p.CurrentState)}");
            for (int cycle = 0; cycle < cycles; cycle++)
            {
                points.ForEach(p => p.Cycle(points));
                points.ForEach(p => p.Init());
                Console.WriteLine($"Cycle {cycle + 1} - {points.Count(p => p.CurrentState)}");
            }
        }

        public class Point
        {
            public int x { get; set; }
            public int y { get; set; }
            public int z { get; set; }
            public int w { get; set; }
            public bool CurrentState { get; set; } = false;
            private bool newState { get; set; }

            public void Cycle(IEnumerable<Point> points)
            {
                var adjacentActive = points.Where(p => Math.Abs(x - p.x) <= 1 && Math.Abs(y - p.y) <= 1 && Math.Abs(z - p.z) <= 1 && Math.Abs(w - p.w) <= 1).Count(p => p.CurrentState);
                newState = (CurrentState && (adjacentActive == 3 || adjacentActive == 4))
                    || (!CurrentState && adjacentActive == 3);
            }

            public void Init()
            {
                CurrentState = newState;
            }
        }
    }
}
