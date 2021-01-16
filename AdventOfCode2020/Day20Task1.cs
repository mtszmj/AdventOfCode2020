using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day20Task1
    {
        public long Solve(string input)
        {
            var tiles = Parse(input);

            var (tileByBorder, singleBorderByTile) = TileByBorder(tiles);
            var corners = FindCorners(singleBorderByTile);
            var borderTiles = FindBordersWithoutCorners(singleBorderByTile);

            return corners.Aggregate(1L, (result, tile) => result * tile.Id);
            
            //var size = (int)Math.Sqrt(tiles.Count);

            //foreach(var corner in corners)
            //{
            //    foreach(var or in corner.Orientations.Where(o => o.TopCount == 1 && o.LeftCount == 1))
            //    {
            //        // create top border
            //        var result = new Orientation[size, size];
            //        result[0, 0] = or;
            //        for(var i = 1; i < size - 1; i++)
            //        {
            //            var previous = result[0, i - 1];
            //            var tile = tileByBorder[previous.Right].FirstOrDefault(x => x != previous.Tile);
            //            result[0, i] = tile.Orientations.FirstOrDefault(x => x.Left == previous.Right && x.TopCount == 1);
            //        }
            //    }
            //}
        }

        public List<Tile> FindCorners(Dictionary<Tile, int> singleBordersByTile)
        {
            return singleBordersByTile.Where(x => x.Value == 4).Select(x => x.Key).ToList();
        }

        public List<Tile> FindBordersWithoutCorners(Dictionary<Tile, int> singleBordersByTile)
        {
            return singleBordersByTile.Where(x => x.Value == 2).Select(x => x.Key).ToList();
        }

        public (Dictionary<string, List<Tile>> TileByBorder, Dictionary<Tile, int> SingleBordersByTile) TileByBorder(List<Tile> tiles)
        {
            var dict = new Dictionary<string, List<Tile>>();
            var borderCount = new Dictionary<string, int>();
            var singleBordersCount = new Dictionary<Tile, int>();
            foreach (var tile in tiles)
            {
                foreach(var border in tile.Borders)
                {
                    if (dict.TryGetValue(border, out var list))
                    {
                        list.Add(tile);
                        borderCount[border] += 1;
                    }
                    else
                    {
                        dict[border] = new List<Tile> { tile };
                        borderCount[border] = 1;
                    }
                }
            }

            foreach(var tile in tiles)
            {
                foreach(var or in tile.Orientations)
                {
                    or.TopCount = borderCount[or.Top];
                    or.RightCount = borderCount[or.Right];
                    or.BottomCount = borderCount[or.Bottom];
                    or.LeftCount = borderCount[or.Left];
                }
            }

            foreach(var kv in dict.Where(x => x.Value.Count == 1))
            {
                var tile = kv.Value[0];
                if (singleBordersCount.TryGetValue(tile, out var count))
                    singleBordersCount[tile] = count + 1;
                else
                    singleBordersCount[tile] = 1;
            }

            return (dict, singleBordersCount);
        }

        public List<Tile> Parse(string input)
        {
            var tilesWithIds = input.Split($"{Environment.NewLine}{Environment.NewLine}", 
                StringSplitOptions.RemoveEmptyEntries);

            var tiles = new List<Tile>();
            foreach(var tileData in tilesWithIds)
            {
                var match = Regex.Match(tileData, @"^Tile (?<id>\d+):\r\n", RegexOptions.Singleline);
                var tile = new Tile(int.Parse(match.Groups["id"].Value), tileData.Replace(match.Value, ""));
                //Console.WriteLine(tile);
                //Console.WriteLine("boki:");
                //Console.WriteLine(string.Join(Environment.NewLine, tile.Borders));
                //Console.WriteLine();

                tiles.Add(tile);
            }

            Console.WriteLine($"Liczba kafel: {tiles.Count}");

            return tiles;
        }

        public class Tile
        {
            public Tile(int id, string tileData)
            {
                Id = id;
                var lines = tileData.Split(Environment.NewLine);
                if (lines.Length != Size)
                    throw new ArgumentException(message: "", paramName: nameof(tileData));

                for (int row = 0; row < lines.Length; row++)
                {
                    string line = (string)lines[row];
                    if (line.Length != Size)
                        throw new ArgumentException(message: "", paramName: nameof(tileData));

                    for(var col = 0; col < Size; col++)
                        Pixels[row, col] = line[col];
                }

                Borders = GetBorders().ToArray();
                Orientations = GetOrientations();
            }

            public const int Size = 10;
            public int Id { get; set; }
            public char[,] Pixels { get; } = new char[Size, Size];
            public string[] Borders { get; }

            public Orientation[] Orientations { get; }

            public List<string> GetBorders()
            {
                var sb = Enumerable.Range(0, 8).Select(x => new StringBuilder()).ToList();
                for(var i = 0; i < Size; i++)
                {
                    sb[0].Append(Pixels[0, i]);
                    sb[1].Append(Pixels[i, Size - 1]);
                    sb[2].Append(Pixels[Size - 1, i]);
                    sb[3].Append(Pixels[i, 0]);
                    sb[4].Append(Pixels[0, Size - 1 - i]);
                    sb[5].Append(Pixels[Size - 1 - i, Size - 1]);
                    sb[6].Append(Pixels[Size - 1, Size - 1 - i]);
                    sb[7].Append(Pixels[Size - 1 - i, 0]);
                }

                return sb.Select(x => x.ToString()).ToList();
            }

            public Orientation[] GetOrientations()
            {
                var result = new Orientation[16];
                result[00] = new Orientation(Borders[0], Borders[1], Borders[2], Borders[3], this);
                result[01] = new Orientation(Borders[1], Borders[6], Borders[3], Borders[4], this); // rotate 90 CCw
                result[02] = new Orientation(Borders[6], Borders[7], Borders[4], Borders[5], this); // rotate 180 CCw
                result[03] = new Orientation(Borders[7], Borders[0], Borders[5], Borders[2], this); // rotate 270 CCw

                result[04] = new Orientation(Borders[2], Borders[5], Borders[0], Borders[7], this); // flip horizontally
                result[05] = new Orientation(Borders[5], Borders[4], Borders[7], Borders[6], this); // flip H 90 CCW
                result[06] = new Orientation(Borders[4], Borders[3], Borders[6], Borders[1], this); // flip H 180 CCW
                result[07] = new Orientation(Borders[3], Borders[2], Borders[1], Borders[0], this); // flip H 270 CCW

                result[08] = new Orientation(Borders[4], Borders[3], Borders[6], Borders[1], this); // flip vertically
                result[09] = new Orientation(Borders[3], Borders[2], Borders[1], Borders[0], this); // flip V 90 CCW 
                result[10] = new Orientation(Borders[2], Borders[5], Borders[0], Borders[7], this); // flip V 180 CCW
                result[11] = new Orientation(Borders[5], Borders[4], Borders[7], Borders[6], this); // flip V 270 CCW

                result[12] = new Orientation(Borders[6], Borders[7], Borders[4], Borders[5], this); // flip HV
                result[13] = new Orientation(Borders[7], Borders[0], Borders[5], Borders[2], this); // flip HV 90 CCW
                result[14] = new Orientation(Borders[0], Borders[1], Borders[2], Borders[3], this); // flip HV 180 CCW
                result[15] = new Orientation(Borders[1], Borders[6], Borders[3], Borders[4], this); // flip HV 270 CCW
                return result;
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                sb.AppendLine($"[{Id}]:");
                for(var row = 0; row < Size; row++)
                {
                    for (var col = 0; col < Size; col++)
                        sb.Append(Pixels[row, col]);
                    sb.AppendLine();
                }
                return sb.ToString();
            }

        }

        public class Orientation
        {
            public Orientation(string t, string r, string b, string l, Tile tile = null)
            {
                Top = t;
                Right = r;
                Bottom = b;
                Left = l;
                Tile = tile;
            }

            public Tile Tile { get; set; }

            public string Top { get; set; }
            public string Right { get; set; }
            public string Bottom { get; set; }
            public string Left { get; set; }

            public int TopCount { get; set; }
            public int RightCount { get; set; }
            public int BottomCount { get; set; }
            public int LeftCount { get; set; }

            public override string ToString()
            {
                var sb = new StringBuilder();
                sb.AppendLine(Top);
                sb.AppendLine($"{Left[1]}        {Right[1]}");
                sb.AppendLine($"{Left[2]}        {Right[2]}");
                sb.AppendLine($"{Left[3]}        {Right[3]}");
                sb.AppendLine($"{Left[4]}        {Right[4]}");
                sb.AppendLine($"{Left[5]}        {Right[5]}");
                sb.AppendLine($"{Left[6]}        {Right[6]}");
                sb.AppendLine($"{Left[7]}        {Right[7]}");
                sb.AppendLine($"{Left[8]}        {Right[8]}");
                sb.AppendLine(Bottom);
                return sb.ToString();
            }
        }

    }
}
