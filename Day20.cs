using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode
{
    public static class Day20
    {
        public static void Problem1()
        {
            var input = Utilities.ReadStrings(20).ToArray();
            var edgeLength = input.Skip(1).First().Length;

            Tile newTile;
            List<Tile> tiles = new List<Tile>();
            StringBuilder w;
            StringBuilder e;
            int ptr = 0;
            while (ptr < input.Length)
            {
                w = new StringBuilder();
                e = new StringBuilder();
                newTile = new Tile();
                newTile.Id = Convert.ToInt32(input[ptr].Replace("Tile ", "").Replace(":", ""));
                newTile.EdgeN = ToBitString(input[ptr + 1]);
                newTile.EdgeS = ToBitString(input[ptr + edgeLength]);
                for (int i = ptr + 1; i <= ptr + edgeLength; i++)
                {
                    w.Append(input[i][0]);
                    e.Append(input[i][edgeLength - 1]);
                }
                newTile.EdgeE = ToBitString(e.ToString());
                newTile.EdgeW = ToBitString(w.ToString());
                tiles.Add(newTile);

                ptr += edgeLength + 2;
            }

            tiles.ForEach(t => t.FindAdjacentTiles(tiles));

            var corners = tiles.Where(t => t.IsCorner).ToList();

            long product = 1;
            corners.ForEach(c => product *= c.Id);

            Console.WriteLine(product);
        }

        public static void Problem2()
        {
            bool useTestData = false;

            var input = Utilities.ReadStrings(20, useTestData).ToArray();
            var edgeLength = input.Skip(1).First().Length;

            Tile newTile;
            List<Tile> tiles = new List<Tile>();
            StringBuilder w;
            StringBuilder e;
            int ptr = 0;
            while (ptr < input.Length)
            {
                w = new StringBuilder();
                e = new StringBuilder();
                newTile = new Tile();
                newTile.Id = Convert.ToInt32(input[ptr].Replace("Tile ", "").Replace(":", ""));
                newTile.EdgeN = ToBitString(input[ptr + 1]);
                newTile.EdgeS = ToBitString(input[ptr + edgeLength]);
                newTile.Image = new List<Point>();
                for (int i = ptr + 1; i <= ptr + edgeLength; i++)
                {
                    w.Append(input[i][0]);
                    e.Append(input[i][edgeLength - 1]);
                    if (i != ptr + 1 && i < ptr + edgeLength)
                    {
                        for (int x = 0; x < edgeLength - 2; x++)
                        {
                            newTile.Image.Add(new Point { X = x, Y = i - ptr - 2, State = input[i][x + 1] });
                        }
                    }
                }
                newTile.EdgeE = ToBitString(e.ToString());
                newTile.EdgeW = ToBitString(w.ToString());
                tiles.Add(newTile);

                ptr += edgeLength + 2;
            }

            tiles.ForEach(t => t.FindAdjacentTiles(tiles));
            AlignTiles(tiles);
            DisplayTiles(tiles);
            var image = BuildImage(tiles);

            List<Point> seaMonster = new List<Point>
            {
                new Point { X = 0, Y = 0 },
                new Point { X = 1, Y = 1 },
                new Point { X = 4, Y = 1 },
                new Point { X = 5, Y = 0 },
                new Point { X = 6, Y = 0 },
                new Point { X = 7, Y = 1 },
                new Point { X = 10, Y = 1 },
                new Point { X = 11, Y = 0 },
                new Point { X = 12, Y = 0 },
                new Point { X = 13, Y = 1 },
                new Point { X = 16, Y = 1 },
                new Point { X = 17, Y = 0 },
                new Point { X = 18, Y = -1 },
                new Point { X = 18, Y = 0 },
                new Point { X = 19, Y = 0 }
            };
            
            var chop = image.Where(p => p.State == '#').ToList();
            int chopCount = chop.Count();

            for (int rotations = 0; rotations < 4; rotations++)
            {
                chop = GetChop(image, seaMonster, chop);
                if (chopCount != chop.Count())
                {
                    Console.WriteLine($"Chop total is {chop.Count()}");
                    return;
                }

                var imageToCheck = FlipH(image);
                chop = imageToCheck.Where(p => p.State == '#').ToList();
                chopCount = chop.Count();
                chop = GetChop(imageToCheck, seaMonster, chop);
                if (chopCount != chop.Count())
                {
                    Console.WriteLine($"Chop total is {chop.Count()}");
                    return;
                }

                imageToCheck = FlipV(image);
                chop = imageToCheck.Where(p => p.State == '#').ToList();
                chopCount = chop.Count();
                chop = GetChop(imageToCheck, seaMonster, chop);
                if (chopCount != chop.Count())
                {
                    Console.WriteLine($"Chop total is {chop.Count()}");
                    return;
                }

                imageToCheck = FlipV(image);
                imageToCheck = FlipH(imageToCheck);
                chop = imageToCheck.Where(p => p.State == '#').ToList();
                chopCount = chop.Count();
                chop = GetChop(imageToCheck, seaMonster, chop);
                if (chopCount != chop.Count())
                {
                    Console.WriteLine($"Chop total is {chop.Count()}");
                    return;
                }

                image = Rotate(image);
            }

            Console.WriteLine("No Sea Monsters Found.");
        }

        private static List<Point> FlipH(List<Point> image)
        {
            List<Point> flippedImage = new List<Point>();
            var imageHeight = image.Max(p => p.Y);
            image.ForEach(p => flippedImage.Add(new Point { X = p.X, Y = imageHeight - p.Y, State = p.State }));
            return flippedImage;
        }

        private static List<Point> FlipV(List<Point> image)
        {
            List<Point> flippedImage = new List<Point>();
            var imageWidth = image.Max(p => p.X);
            image.ForEach(p => flippedImage.Add(new Point { X = imageWidth - p.X, Y = p.Y, State = p.State }));
            return flippedImage;
        }

        private static List<Point> Rotate(List<Point> image)
        {
            List<Point> rotatedImage = new List<Point>();
            var imageHeight = image.Max(p => p.Y);
            image.ForEach(p => rotatedImage.Add(new Point { X = imageHeight = p.Y, Y = p.X, State = p.State }));
            return rotatedImage;
        }

        public static List<Point> GetChop(List<Point> image, List<Point> seaMonster, List<Point> chop)
        {

            foreach (var imagePoint in image)
            {
                bool seaMonsterFound = true;
                foreach (var seaMonsterPoint in seaMonster)
                {
                    var point = image.Find(p => p.X == imagePoint.X + seaMonsterPoint.X && p.Y == imagePoint.Y + seaMonsterPoint.Y);
                    if (point == null || point.State != '#')
                    {
                        seaMonsterFound = false;
                        break;
                    }
                }
                if (seaMonsterFound)
                {
                    foreach (var seaMonsterPoint in seaMonster)
                    {
                        chop = chop.Where(p => !(p.X == imagePoint.X + seaMonsterPoint.X && p.Y == imagePoint.Y + seaMonsterPoint.Y)).ToList();
                    }
                }
            }

            return chop;
        }

        public static void DisplayTiles(List<Tile> tiles)
        {
            for (int y = 0; y <= tiles.Max(t => t.Y); y++)
            {
                for (int x = 0; x <= tiles.Max(t => t.X); x++)
                {
                    Console.Write(tiles.Find(t => t.X == x && t.Y == y).Id.ToString().PadRight(6, ' '));
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static void DisplayImage(List<Point> image)
        { 
            for (int y = 0; y <= image.Max(p => p.Y); y++)
            {
                for (int x = 0; x <= image.Max(p => p.X); x++)
                {
                    Console.Write(image.Find(p => p.X == x && p.Y == y).State);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static string ToBitString(string input)
        {
            return input.Replace('#', '1').Replace('.', '0');
        }

        public static void AlignTiles(IEnumerable<Tile> tiles)
        {
            var width = (int)Math.Sqrt(tiles.Count());

            var nw = tiles.Where(t => t.North == null && t.West == null);
            var curTile = tiles.Where(t => t.North == null && t.West == null).First();
            curTile.X = 0;
            curTile.Y = 0;
            for (int y = 0; y < width; y++)
            {
                if (y != 0)
                {
                    //align south tile
                    curTile = tiles.Where(t => t.X == 0 && t.Y == y - 1).First();
                    var southTile = curTile.South;
                    if (curTile.EdgeS == southTile.EdgeE || curTile.EdgeS == new string(southTile.EdgeE.Reverse().ToArray()))
                    {
                        southTile.Rotate(3);
                    }
                    else if (curTile.EdgeS == southTile.EdgeS || curTile.EdgeS == new string(southTile.EdgeS.Reverse().ToArray()))
                    {
                        southTile.Rotate(2);
                    }
                    else if (curTile.EdgeS == southTile.EdgeW || curTile.EdgeS == new string(southTile.EdgeW.Reverse().ToArray()))
                    {
                        southTile.Rotate(1);
                    }
                    if (curTile.EdgeS == new string(southTile.EdgeN.Reverse().ToArray()))
                    {
                        southTile.FlipOnVerticalAxis();
                    }
                    southTile.X = 0;
                    southTile.Y = y;
                    curTile = southTile;
                }
                for (int x = 0; x < width - 1; x++)
                {
                    //align east tile
                    var eastTile = curTile.East;
                    if (curTile.EdgeE == eastTile.EdgeN || curTile.EdgeE == new string(eastTile.EdgeN.Reverse().ToArray()))
                    {
                        eastTile.Rotate(3);
                    }
                    else if (curTile.EdgeE == eastTile.EdgeE || curTile.EdgeE == new string(eastTile.EdgeE.Reverse().ToArray()))
                    {
                        eastTile.Rotate(2);
                    }
                    else if (curTile.EdgeE == eastTile.EdgeS || curTile.EdgeE == new string(eastTile.EdgeS.Reverse().ToArray()))
                    {
                        eastTile.Rotate(1);
                    }
                    if (curTile.EdgeE == new string(eastTile.EdgeW.Reverse().ToArray()))
                    {
                        eastTile.FlipOnHorizontalAxis();
                    }
                    eastTile.X = x + 1;
                    eastTile.Y = y;
                    curTile = eastTile;
                }
            }
        }

        public static List<Point> BuildImage (List<Tile> tiles)
        {
            int tileSideLength = tiles.First().Image.Max(i => i.X) - tiles.First().Image.Min(i => i.X) + 1;
            List<Point> image = new List<Point>();
            for (int ty = 0; ty <= tiles.Max(t => t.Y); ty++)
            {
                for (int tx = 0; tx <= tiles.Max(t => t.X); tx++)
                {
                    var tile = tiles.Find(t => t.Y == ty && t.X == tx);
                    for (int y = 0; y < tileSideLength; y++)
                    {
                        for (int x = 0; x < tileSideLength; x++)
                        {
                            var point = new Point
                            {
                                X = (tx * tileSideLength) + x,
                                Y = (ty * tileSideLength) + y,
                                State = tile.Image.Find(p => p.X == x && p.Y == y).State
                            };
                            image.Add(point);
                        }
                    }
                }
            }

            return image;
        }

        public class Point
        {
            public int X { get; set; }
            public int Y { get; set; }
            public char State { get; set; }
        }

        public class Tile
        {
            public int Id { get; set; }
            public int? X { get; set; } = null;
            public int? Y { get; set; } = null;
            public Tile North { get; set; }
            public Tile South { get; set; }
            public Tile West { get; set; }
            public Tile East { get; set; }

            public string EdgeN { get; set; }
            public string EdgeE { get; set; }
            public string EdgeS { get; set; }
            public string EdgeW { get; set; }

            public List<Point> Image { get; set; } = new List<Point>();

            public void Rotate(int rotations)
            {
                for (int i = 0; i < rotations; i++)
                {
                    var imageHeight = Image.Max(p => p.Y);
                    foreach (var point in Image)
                    {
                        int temp = point.Y;
                        point.Y = point.X;
                        point.X = imageHeight - temp;
                    }
                    Tile tempTile = North;
                    North = West;
                    West = South;
                    South = East;
                    East = tempTile;
                    string tempEdge = EdgeN;
                    EdgeN = new string(EdgeW.Reverse().ToArray());
                    EdgeW = EdgeS;
                    EdgeS = new string(EdgeE.Reverse().ToArray());
                    EdgeE = tempEdge;
                }
            }

            public void FlipOnHorizontalAxis()
            {
                var imageHeight = Image.Max(p => p.Y);
                Image.ForEach(p => p.Y = imageHeight - p.Y);
                Tile tempTile = North;
                North = South;
                South = tempTile;
                string tempEdge = EdgeN;
                EdgeN = EdgeS;
                EdgeS = tempEdge;
                EdgeE = new string(EdgeE.Reverse().ToArray());
                EdgeW = new string(EdgeW.Reverse().ToArray());
            }

            public void FlipOnVerticalAxis()
            {
                var imageWidth = Image.Max(p => p.X);
                Image.ForEach(p => p.X = imageWidth - p.X);
                Tile tempTile = West;
                West = East;
                East = tempTile;
                string tempEdge = EdgeW;
                EdgeW = EdgeE;
                EdgeE = tempEdge;
                EdgeN = new string(EdgeN.Reverse().ToArray());
                EdgeS = new string(EdgeS.Reverse().ToArray());
            }

            public void FindAdjacentTiles(IEnumerable<Tile> tiles)
            {
                var norths = tiles.Where(t => t.Id != Id && t.Edges.Contains(Convert.ToInt32(EdgeN, 2))).ToList();
                norths.AddRange(tiles.Where(t => t.Id != Id && !norths.Contains(t) && t.FlippedEdges.Contains(Convert.ToInt32(EdgeN, 2))));
                North = norths.FirstOrDefault();

                var souths = tiles.Where(t => t.Id != Id && t.Edges.Contains(Convert.ToInt32(EdgeS, 2))).ToList();
                souths.AddRange(tiles.Where(t => t.Id != Id && !souths.Contains(t) && t.FlippedEdges.Contains(Convert.ToInt32(EdgeS, 2))));
                South = souths.FirstOrDefault();

                var easts = tiles.Where(t => t.Id != Id && t.Edges.Contains(Convert.ToInt32(EdgeE, 2))).ToList();
                easts.AddRange(tiles.Where(t => t.Id != Id && !easts.Contains(t) && t.FlippedEdges.Contains(Convert.ToInt32(EdgeE, 2))));
                East = easts.FirstOrDefault();

                var wests = tiles.Where(t => t.Id != Id && t.Edges.Contains(Convert.ToInt32(EdgeW, 2))).ToList();
                wests.AddRange(tiles.Where(t => t.Id != Id && !wests.Contains(t) && t.FlippedEdges.Contains(Convert.ToInt32(EdgeW, 2))));
                West = wests.FirstOrDefault();

                if (norths.Count() > 1 || souths.Count() > 1 || easts.Count() > 1 || wests.Count() > 1)
                {
                    Console.WriteLine("This solutions isn't going to work.");
                }
            }

            public bool IsCorner
            {
                get
                {
                    int adjacentTiles = 0;
                    adjacentTiles += (North == null) ? 0 : 1;
                    adjacentTiles += (South == null) ? 0 : 1;
                    adjacentTiles += (East == null) ? 0 : 1;
                    adjacentTiles += (West == null) ? 0 : 1;
                    return adjacentTiles == 2;
                }
            }

            public List<int> Edges
            {
                get
                {
                    return new List<int>
                    {
                        Convert.ToInt32(EdgeN, 2),
                        Convert.ToInt32(EdgeE, 2),
                        Convert.ToInt32(EdgeS, 2),
                        Convert.ToInt32(EdgeW, 2)
                    };
                }
            }

            public List<int> FlippedEdges
            {
                get
                {
                    return new List<int> {
                        Convert.ToInt32(new string(EdgeN.Reverse().ToArray()), 2),
                        Convert.ToInt32(new string(EdgeE.Reverse().ToArray()), 2),
                        Convert.ToInt32(new string(EdgeW.Reverse().ToArray()), 2),
                        Convert.ToInt32(new string(EdgeS.Reverse().ToArray()), 2)
                    };
                }
            }
        }
    }
}
