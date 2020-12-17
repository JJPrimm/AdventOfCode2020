using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class Day11
    {
        public static void Problem1()
        {
            var input = Utilities.ReadStrings(11).ToArray();
            List<Seat> seats = new List<Seat>();
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[0].Length; x++)
                {
                    seats.Add(new Seat(input[y][x], x, y));
                }
            }
            //WriteSeats(seats);

            while (true)
            {
                seats.ForEach(s => s.ChangeState1(seats));
                if (!seats.Any(seats => seats.StateChanged))
                {
                    Console.WriteLine(seats.Count(s => s.IsOccupied));
                    return;
                }
                seats.ForEach(s => s.Reset());
                //WriteSeats(seats);
            }
        }
        public static void Problem2()
        {
            var input = Utilities.ReadStrings(11).ToArray();
            List<Seat> seats = new List<Seat>();
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[0].Length; x++)
                {
                    seats.Add(new Seat(input[y][x], x, y));
                }
            }
            //WriteSeats(seats);

            while (true)
            {
                seats.ForEach(s => s.ChangeState2(seats));
                if (!seats.Any(seats => seats.StateChanged))
                {
                    Console.WriteLine(seats.Count(s => s.IsOccupied));
                    return;
                }
                seats.ForEach(s => s.Reset());
                //WriteSeats(seats);
            }
        }

        public static void WriteSeats(List<Seat> seats)
        {
            int xMax = seats.Max(seats => seats.X);
            int yMax = seats.Max(seats => seats.Y);
            for (int y = 0; y <= yMax; y++)
            {
                for (int x = 0; x <= xMax; x++)
                {
                    Console.Write(seats.Find(s => s.X == x && s.Y == y).GetChar());
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

    public class Seat
    {
        public bool IsFloor { get; set; }
        public bool IsOccupied { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool StateChanged = false;
        public bool NewValue;

        public Seat(char state, int x, int y)
        {
            IsFloor = state == '.';
            IsOccupied = state == '#';
            X = x;
            Y = y;
            NewValue = IsOccupied;
        }

        public char GetChar()
        {
            if (IsFloor)
            {
                return '.';
            }
            if (IsOccupied)
            {
                return '#';
            }
            return 'L';
        }

        public void ChangeState1(List<Seat> grid)
        {
            if (IsFloor)
            {
                return;
            }

            var adjacent = grid.Where(s => s.X >= X - 1 && s.X <= X + 1 && s.Y >= Y - 1 && s.Y <= Y + 1);
            if (IsOccupied && adjacent.Count(a => a.IsOccupied) >= 5)
            {
                NewValue = false;
                StateChanged = true;
            }
            if (adjacent.Count(adjacent => adjacent.IsOccupied) == 0)
            {
                NewValue = true;
                StateChanged = true;
            }
        }

        public void ChangeState2(List<Seat> grid)
        {
            if (IsFloor)
            {
                return;
            }
            int i = 1;
            int maxX = grid.Max(s => s.X);
            int maxY = grid.Max(s => s.Y);
            bool nFound = false;
            bool neFound = false;
            bool eFound = false;
            bool seFound = false;
            bool sFound = false;
            bool swFound = false;
            bool wFound = false;
            bool nwFound = false;
            int occupied = 0;
            while (!nFound || !neFound || !eFound || !seFound || !sFound || !swFound || !wFound || ! nwFound)
            {
                if (!sFound && Y + i <= maxY && !grid.Find(s => s.X == X && s.Y == Y + i).IsFloor)
                {
                    sFound = true;
                    occupied += (grid.Find(s => s.X == X && s.Y == Y + i).IsOccupied) ? 1 : 0;
                }
                else if (Y + i > maxY)
                {
                    sFound = true;
                }

                if (!seFound && Y + i <= maxY && X + i <= maxX && !grid.Find(s => s.X == X + i && s.Y == Y + i).IsFloor)
                {
                    seFound = true;
                    occupied += (grid.Find(s => s.X == X + i && s.Y == Y + i).IsOccupied) ? 1 : 0;
                }
                else if (Y + i > maxY || X + i > maxX)
                {
                    seFound = true;
                }

                if (!eFound && X + i <= maxX && !grid.Find(s => s.X == X + i && s.Y == Y).IsFloor)
                {
                    eFound = true;
                    occupied += (grid.Find(s => s.X == X + i && s.Y == Y).IsOccupied) ? 1 : 0;
                }
                else if (X + i > maxX)
                {
                    eFound = true;
                }

                if (!neFound && X + i <= maxX && Y - i >= 0 && !grid.Find(s => s.X == X + i && s.Y == Y - i).IsFloor)
                {
                    neFound = true;
                    occupied += (grid.Find(s => s.X == X + i && s.Y == Y - i).IsOccupied) ? 1 : 0;
                }
                else if (X + i > maxX || Y - i < 0)
                {
                    neFound = true;
                }

                if (!nFound && Y - i >= 0 && !grid.Find(s => s.X == X && s.Y == Y - i).IsFloor)
                {
                    nFound = true;
                    occupied += (grid.Find(s => s.X == X && s.Y == Y - i).IsOccupied) ? 1 : 0;
                }
                else if (Y - i < 0)
                {
                    nFound = true;
                }

                if (!nwFound && X - i >= 0 && Y - i >= 0 && !grid.Find(s => s.X == X - i && s.Y == Y - i).IsFloor)
                {
                    nwFound = true;
                    occupied += (grid.Find(s => s.X == X - i && s.Y == Y - i).IsOccupied) ? 1 : 0;
                }
                else if (X - i < 0 || Y - i < 0)
                {
                    nwFound = true;
                }

                if (!wFound && X - i >= 0 && !grid.Find(s => s.X == X - i && s.Y == Y).IsFloor)
                {
                    wFound = true;
                    occupied += (grid.Find(s => s.X == X - i && s.Y == Y).IsOccupied) ? 1 : 0;
                }
                else if (X - i < 0)
                {
                    wFound = true;
                }

                if (!swFound && X - i >= 0 && Y + i <= maxY && !grid.Find(s => s.X == X - i && s.Y == Y + i).IsFloor)
                {
                    swFound = true;
                    occupied += (grid.Find(s => s.X == X - i && s.Y == Y + i).IsOccupied) ? 1 : 0;
                }
                else if (X - i < 0 || Y + i > maxY)
                {
                    swFound = true;
                }

                if (!IsOccupied && occupied > 0)
                {
                    return;
                }
                if (IsOccupied && occupied >= 5)
                {
                    NewValue = false;
                    StateChanged = true;
                    return;
                }
                i++;
            }
            if (!IsOccupied && occupied == 0)
            {
                NewValue = true;
                StateChanged = true;
                return;
            }
        }

        public void Reset()
        {
            IsOccupied = NewValue;
            StateChanged = false;
        }
    }
}
