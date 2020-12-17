using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class Day12
    {
        public static void Problem1()
        {
            var input = Utilities.ReadStrings(12)
                .Select(i => new Instruction { Direction = i[0], Amplitude = Convert.ToInt32(i.Substring(1)) })
                .ToList();
            var ship = new Ship('E');

            input.ForEach(i => ship.Move1(i));
            Console.WriteLine(Math.Abs(ship.X) + Math.Abs(ship.Y));
        }

        public static void Problem2()
        {
            var input = Utilities.ReadStrings(12)
                .Select(i => new Instruction { Direction = i[0], Amplitude = Convert.ToInt32(i.Substring(1)) })
                .ToList();
            var ship = new Ship('E', 10, 1);

            input.ForEach(i => ship.Move2(i));
            Console.WriteLine(Math.Abs(ship.X) + Math.Abs(ship.Y));
        }
    }

    public class Ship
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int WptX { get; set; }
        public int WptY { get; set; }

        private int dirPtr { get; set; }
        private char[] directions = { 'E', 'S', 'W', 'N' };

        public Ship(char direction, int wptX = 0, int wptY = 0)
        {
            dirPtr = Array.FindIndex(directions, d => d == direction);
            WptX = wptX;
            WptY = wptY;
        }

        public void Move1 (Instruction instruction)
        {
            char directionToMove = (instruction.Direction == 'F') ? directions[dirPtr] : instruction.Direction;
            if (directions.Contains(directionToMove))
            {
                switch (directionToMove)
                {
                    case 'E':
                        X += instruction.Amplitude;
                        break;
                    case 'S':
                        Y -= instruction.Amplitude;
                        break;
                    case 'W':
                        X -= instruction.Amplitude;
                        break;
                    case 'N':
                        Y += instruction.Amplitude;
                        break;
                }

            }
            else
            {
                dirPtr = (instruction.Direction == 'R')
                    ? (4 + dirPtr + (instruction.Amplitude / 90)) % 4
                    : (4 + dirPtr - (instruction.Amplitude / 90)) % 4;
            }
        }

        public void Move2(Instruction instruction)
        {
            switch (instruction.Direction)
            {
                case 'E':
                    WptX += instruction.Amplitude;
                    break;
                case 'S':
                    WptY -= instruction.Amplitude;
                    break;
                case 'W':
                    WptX -= instruction.Amplitude;
                    break;
                case 'N':
                    WptY += instruction.Amplitude;
                    break;
                case 'F':
                    X += WptX * instruction.Amplitude;
                    Y += WptY * instruction.Amplitude;
                    break;
                case 'L':
                    RotateLeft(instruction.Amplitude / 90);
                    break;
                case 'R':
                    RotateRight((instruction.Amplitude / 90));
                    break;
            }
        }

        private void RotateRight(int turns)
        {
            for (int t = 1; t <= turns; t++)
            {
                int temp = WptX;
                WptX = WptY;
                WptY = -temp;
            }
        }

        private void RotateLeft(int turns)
        {
            for (int t = 1; t <= turns; t++)
            {
                int temp = WptX;
                WptX = -WptY;
                WptY = temp;
            }
        }
    }

    public class Instruction
    {
        public char Direction { get; set; }
        public int Amplitude { get; set; }
    }
}
