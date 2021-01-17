using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day20Task2
    {
        public long Solve(string input)
        {
            var tiles = Parse(input);

            var (tileByBorder, singleBorderByTile) = TileByBorder(tiles);
            var corners = FindCorners(singleBorderByTile);
            var borderTiles = FindBordersWithoutCorners(singleBorderByTile);

            var size = (int)Math.Sqrt(tiles.Count);

            var usedTiles = new HashSet<Tile>();
            var corner = corners.First();
            var or = corner.Orientations.First(o => o.TopCount == 1 && o.LeftCount == 1);
            var result = new Orientation[size, size];
            result[0, 0] = or;
            usedTiles.Add(or.Tile);
            AssignTopBorder(tileByBorder, size, usedTiles, result);
            AssignLeftBorder(tileByBorder, size, usedTiles, result);
            AssignRightBorder(tileByBorder, size, usedTiles, result);
            AssignBottomBorder(tileByBorder, size, usedTiles, result);
            AssignMiddle(tileByBorder, size, usedTiles, result);

            var picture = PrepareWholePicture(result, size);
            int allHashesCount = CountAllHashes(picture);
            PrintPicture(picture);
            var smo = PrepareSeaMonsterPattern();

            foreach (var orientationId in Enumerable.Range(0, 15))
            {
                var p = Transform(picture, (OrientationType)orientationId);
                var seaMonsterCount = 0;
                for (var row = 0; row < p.GetLength(0) - smo.Height; row++)
                {
                    for (var col = 0; col < p.GetLength(1) - smo.Width; col++)
                    {
                        var incorrect = false;
                        foreach (var bits in smo.Pixels)
                        {
                            if (p[row + bits.row, col + bits.col] != '#')
                            {
                                incorrect = true;
                                break;
                            }
                        }
                        if (incorrect)
                            continue;
                        seaMonsterCount++;
                    }
                }
                if (seaMonsterCount > 0)
                {
                    return allHashesCount - (smo.Pixels.Count * seaMonsterCount);
                }
            }
            //foreach (var orr in result)
            //{
            //    Console.WriteLine(orr.Tile.ToString());
            //    Console.WriteLine();
            //    Console.WriteLine(orr.PrintBorderWithMiddle());
            //    Console.WriteLine();
            //    Console.WriteLine();
            //}

            //Console.WriteLine(PrintBorders(result));



            return 0;

            static void AssignTopBorder(Dictionary<string, List<Tile>> tileByBorder, int size, HashSet<Tile> usedTiles, Orientation[,] result)
            {
                Orientation previous = null;
                for (var i = 1; i < size - 1; i++)
                {
                    previous = result[0, i - 1];
                    var tile = tileByBorder[previous.Right].FirstOrDefault(x => x != previous.Tile && !usedTiles.Contains(x));
                    result[0, i] = tile.Orientations.FirstOrDefault(x => x.Left == previous.Right && x.TopCount == 1);
                    usedTiles.Add(result[0, i].Tile);
                }
                previous = result[0, size - 2];
                var topRightCorner = tileByBorder[previous.Right].FirstOrDefault(x => x != previous.Tile && !usedTiles.Contains(x));
                result[0, size - 1] = topRightCorner.Orientations.FirstOrDefault(x => x.Left == previous.Right && x.TopCount == 1 && x.RightCount == 1);
            }

            static void AssignLeftBorder(Dictionary<string, List<Tile>> tileByBorder, int size, HashSet<Tile> usedTiles, Orientation[,] result)
            {
                Orientation previous = null;
                for (var i = 1; i < size - 1; i++)
                {
                    previous = result[i - 1, 0];
                    var tile = tileByBorder[previous.Bottom].FirstOrDefault(x => x != previous.Tile && !usedTiles.Contains(x));
                    result[i, 0] = tile.Orientations.FirstOrDefault(x => x.Top == previous.Bottom && x.LeftCount == 1);
                    usedTiles.Add(result[i, 0].Tile);
                }
                previous = result[size - 2, 0];
                var leftBottomCorner = tileByBorder[previous.Bottom].FirstOrDefault(x => x != previous.Tile && !usedTiles.Contains(x));
                result[size - 1, 0] = leftBottomCorner.Orientations.FirstOrDefault(x => x.Top == previous.Bottom && x.LeftCount == 1 && x.BottomCount == 1);
            }

            static void AssignRightBorder(Dictionary<string, List<Tile>> tileByBorder, int size, HashSet<Tile> usedTiles, Orientation[,] result)
            {
                Orientation previous = null;
                for (var i = 1; i < size - 1; i++)
                {
                    previous = result[i - 1, size - 1];
                    var tile = tileByBorder[previous.Bottom].FirstOrDefault(x => x != previous.Tile && !usedTiles.Contains(x));
                    result[i, size - 1] = tile.Orientations.FirstOrDefault(x => x.Top == previous.Bottom && x.RightCount == 1);
                    usedTiles.Add(result[i, size - 1].Tile);
                }
                previous = result[size - 2, size - 1];
                var leftRightCorner = tileByBorder[previous.Bottom].FirstOrDefault(x => x != previous.Tile && !usedTiles.Contains(x));
                result[size - 1, size - 1] = leftRightCorner.Orientations.FirstOrDefault(x => x.Top == previous.Bottom && x.RightCount == 1 && x.BottomCount == 1);
            }

            static void AssignBottomBorder(Dictionary<string, List<Tile>> tileByBorder, int size, HashSet<Tile> usedTiles, Orientation[,] result)
            {
                Orientation previous = null;
                for (var i = 1; i < size - 2; i++)
                {
                    previous = result[size - 1, i - 1];
                    var tile = tileByBorder[previous.Right].FirstOrDefault(x => x != previous.Tile && !usedTiles.Contains(x));
                    result[size - 1, i] = tile.Orientations.FirstOrDefault(x => x.Left == previous.Right && x.BottomCount == 1);
                    usedTiles.Add(result[size - 1, i].Tile);
                }
                previous = result[size - 1, size - 3];
                var bottomRightCorner = result[size - 1, size - 1];
                var beforeBottomRightCorner = tileByBorder[previous.Right].FirstOrDefault(x => x != previous.Tile && !usedTiles.Contains(x));
                result[size - 1, size - 2] = beforeBottomRightCorner.Orientations
                    .FirstOrDefault(x => x.Left == previous.Right && x.Right == bottomRightCorner.Left && x.BottomCount == 1);
            }

            static void AssignMiddle(Dictionary<string, List<Tile>> tileByBorder, int size, HashSet<Tile> usedTiles, Orientation[,] result)
            {
                Orientation previous = null;
                for (var row = 1; row < size - 1; row++)
                {
                    for (var col = 1; col < size - 1; col++)
                    {
                        previous = result[row, col - 1];
                        var tile = tileByBorder[previous.Right].FirstOrDefault(x => x != previous.Tile && !usedTiles.Contains(x));

                        if (row == 1 && col == size - 2)
                        {
                            result[row, col] = tile.Orientations
                                .FirstOrDefault(x => x.Left == previous.Right && x.Top == result[row - 1, col].Bottom && x.Right == result[row, col + 1].Left);
                        }
                        else if (row == size - 2 && col < size - 2)
                        {
                            result[row, col] = tile.Orientations
                                .FirstOrDefault(x => x.Left == previous.Right && x.Top == result[row - 1, col].Bottom && x.Bottom == result[row + 1, col].Top);
                        }
                        else if (row == size - 2 && col == size - 2)
                        {
                            result[row, col] = tile.Orientations
                                .FirstOrDefault(x => x.Left == previous.Right && x.Top == result[row - 1, col].Bottom && x.Bottom == result[row + 1, col].Top
                                && x.Right == result[row, col + 1].Left);
                        }
                        else //if (row == 1 && col < size - 2)
                        {
                            result[row, col] = tile.Orientations
                                .FirstOrDefault(x => x.Left == previous.Right && x.Top == result[row - 1, col].Bottom);
                        }
                        usedTiles.Add(result[row, col].Tile);
                    }
                }
            }

            static int CountAllHashes(char[,] picture)
            {
                var countHashes = 0;
                for (var row = 0; row < picture.GetLength(0); row++)
                {
                    for (var col = 0; col < picture.GetLength(0); col++)
                    {
                        if (picture[row, col] == '#')
                            countHashes++;
                    }
                }

                return countHashes;
            }
        }

        private static void PrintPicture(char[,] picture)
        {
            for (var row = 0; row < picture.GetLength(0); row++)
            {
                for (var col = 0; col < picture.GetLength(1); col++)
                {
                    Console.Write(picture[row, col]);
                }
                Console.WriteLine();
            }
        }

        private char[,] PrepareWholePicture(Orientation[,] result, int size)
        {
            var sizeWithoutBorders = Tile.Size - 2;
            var pictureSize = size * sizeWithoutBorders;
            var picture = new char[pictureSize, pictureSize];

            for (var tileRow = 0; tileRow < size; tileRow++)
                for (var tileCol = 0; tileCol < size; tileCol++)
                {
                    var middle = result[tileRow, tileCol].PrintMiddle().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                    for (var pixelRow = 0; pixelRow < middle.Length; pixelRow++)
                        for (var pixelCol = 0; pixelCol < middle[0].Length; pixelCol++)
                        {
                            picture[tileRow * sizeWithoutBorders + pixelRow, tileCol * sizeWithoutBorders + pixelCol] = middle[pixelRow][pixelCol];
                        }
                }

            return picture;
        }

        public SeaMonsterOrientation PrepareSeaMonsterPattern()
        {
            //01234567890123456789
            //                  # 
            //#    ##    ##    ###
            // #  #  #  #  #  #   
            //012345678901234567#9
            //#1234##7890##3456###
            //0#23#56#89#12#45#789
            return new SeaMonsterOrientation(new[]
            {
                (1,0),
                (2,1),
                (2,4),
                (1,5),
                (1,6),
                (2,7),
                (2,10),
                (1,11),
                (1,12),
                (2,13),
                (2,16),
                (1,17),
                (1,18),
                (1,19),
                (0,18),
            });
        }

        public class SeaMonsterOrientation
        {
            public SeaMonsterOrientation(IEnumerable<(int row, int col)> pixels)
            {
                Pixels = pixels.ToList();
                Height = pixels.Max(x => x.row);
                Width = pixels.Max(x => x.col);
            }
            public List<(int row, int col)> Pixels { get; set; }
            public int Height { get; set; }
            public int Width { get; set; }
        }

        public string PrintBorders(Orientation[,] orientations)
        {
            var sizeRow = orientations.GetLength(0);
            var sizeColumn = orientations.GetLength(1);

            var sb = new StringBuilder();
            for (var row = 0; row < sizeRow; row++)
            {
                for (var line = 0; line < orientations[0, 0].Left.Length; line++)
                {
                    for (var col = 0; col < sizeColumn; col++)
                    {
                        sb.Append(orientations[row, col].GetLine(line));
                        sb.Append(" ");
                    }
                    sb.AppendLine();
                }
                sb.AppendLine();
            }
            return sb.ToString();
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
                foreach (var border in tile.Borders)
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

            foreach (var tile in tiles)
            {
                foreach (var or in tile.Orientations)
                {
                    or.TopCount = borderCount[or.Top];
                    or.RightCount = borderCount[or.Right];
                    or.BottomCount = borderCount[or.Bottom];
                    or.LeftCount = borderCount[or.Left];
                }
            }

            foreach (var kv in dict.Where(x => x.Value.Count == 1))
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
            foreach (var tileData in tilesWithIds)
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

                    for (var col = 0; col < Size; col++)
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
                for (var i = 0; i < Size; i++)
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
                result[00] = new Orientation(Borders[0], Borders[1], Borders[2], Borders[3], OrientationType.Normal, this);
                result[01] = new Orientation(Borders[1], Borders[6], Borders[3], Borders[4], OrientationType.Rot90CCW, this); // rotate 90 CCw
                result[02] = new Orientation(Borders[6], Borders[7], Borders[4], Borders[5], OrientationType.Rot180CCW, this); // rotate 180 CCw
                result[03] = new Orientation(Borders[7], Borders[0], Borders[5], Borders[2], OrientationType.Rot270CCW, this); // rotate 270 CCw

                result[04] = new Orientation(Borders[2], Borders[5], Borders[0], Borders[7], OrientationType.FlipHorNormal, this); // flip horizontally
                result[05] = new Orientation(Borders[5], Borders[4], Borders[7], Borders[6], OrientationType.FlipHorRot90CCW, this); // flip H 90 CCW
                result[06] = new Orientation(Borders[4], Borders[3], Borders[6], Borders[1], OrientationType.FlipHorRot180CCW, this); // flip H 180 CCW
                result[07] = new Orientation(Borders[3], Borders[2], Borders[1], Borders[0], OrientationType.FlipHorRot270CCW, this); // flip H 270 CCW

                result[08] = new Orientation(Borders[4], Borders[3], Borders[6], Borders[1], OrientationType.FlipVerNormal, this); // flip vertically
                result[09] = new Orientation(Borders[3], Borders[2], Borders[1], Borders[0], OrientationType.FlipVerRot90CCW, this); // flip V 90 CCW 
                result[10] = new Orientation(Borders[2], Borders[5], Borders[0], Borders[7], OrientationType.FlipVerRot180CCW, this); // flip V 180 CCW
                result[11] = new Orientation(Borders[5], Borders[4], Borders[7], Borders[6], OrientationType.FlipVerRot270CCW, this); // flip V 270 CCW

                result[12] = new Orientation(Borders[6], Borders[7], Borders[4], Borders[5], OrientationType.FlipHorVerNormal, this); // flip HV
                result[13] = new Orientation(Borders[7], Borders[0], Borders[5], Borders[2], OrientationType.FlipHorVerRot90CCW, this); // flip HV 90 CCW
                result[14] = new Orientation(Borders[0], Borders[1], Borders[2], Borders[3], OrientationType.FlipHorVerRot180CCW, this); // flip HV 180 CCW
                result[15] = new Orientation(Borders[1], Borders[6], Borders[3], Borders[4], OrientationType.FlipHorVerRot270CCW, this); // flip HV 270 CCW
                return result;
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                sb.AppendLine($"[{Id}]:");
                for (var row = 0; row < Size; row++)
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
            public Orientation(string t, string r, string b, string l, OrientationType otype, Tile tile = null)
            {
                Top = t;
                Right = r;
                Bottom = b;
                Left = l;
                Tile = tile;
                OrientationType = otype;
            }

            public Tile Tile { get; set; }

            public OrientationType OrientationType { get; set; }

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

            public string GetLine(int index)
            {
                if (index == 0)
                    return Top;
                else if (index == 9)
                    return Bottom;
                else return $"{Left[index]}        {Right[index]}";
            }

            public string PrintBorderWithMiddle()
            {
                var sb = new StringBuilder();
                var size_1 = Tile.Size - 1;
                for (var row = 0; row < Tile.Size; row++)
                {
                    for (var col = 0; col < Tile.Size; col++)
                    {
                        char c = GetChar(size_1, row, col);
                        sb.Append(c);
                    }
                    sb.AppendLine();
                }
                return sb.ToString();
            }

            public string PrintMiddle()
            {
                var sb = new StringBuilder();
                var size_1 = Tile.Size - 1;
                for (var row = 1; row < Tile.Size - 1; row++)
                {
                    for (var col = 1; col < Tile.Size - 1; col++)
                    {
                        char c = GetChar(size_1, row, col);
                        sb.Append(c);
                    }
                    sb.AppendLine();
                }
                return sb.ToString();
            }

            private char GetChar(int size_1, int row, int col)
            {
                return OrientationType switch
                {
                    OrientationType.Normal => Tile.Pixels[row, col],
                    OrientationType.Rot90CCW => Tile.Pixels[col, size_1 - row],
                    OrientationType.Rot180CCW => Tile.Pixels[size_1 - row, size_1 - col],
                    OrientationType.Rot270CCW => Tile.Pixels[size_1 - col, row],
                    OrientationType.FlipHorNormal => Tile.Pixels[size_1 - row, col],
                    OrientationType.FlipHorRot90CCW => Tile.Pixels[size_1 - col, size_1 - row],
                    OrientationType.FlipHorRot180CCW => Tile.Pixels[row, size_1 - col],
                    OrientationType.FlipHorRot270CCW => Tile.Pixels[col, row],
                    OrientationType.FlipVerNormal => Tile.Pixels[row, size_1 - col],
                    OrientationType.FlipVerRot90CCW => Tile.Pixels[col, row],
                    OrientationType.FlipVerRot180CCW => Tile.Pixels[size_1 - row, col],
                    OrientationType.FlipVerRot270CCW => Tile.Pixels[size_1 - col, size_1 - row],
                    OrientationType.FlipHorVerNormal => Tile.Pixels[size_1 - row, size_1 - col],
                    OrientationType.FlipHorVerRot90CCW => Tile.Pixels[size_1 - col, row],
                    OrientationType.FlipHorVerRot180CCW => Tile.Pixels[row, col],
                    OrientationType.FlipHorVerRot270CCW => Tile.Pixels[col, size_1 - row]
                };
            }
        }

        public enum OrientationType
        {
            Normal,
            Rot90CCW,
            Rot180CCW,
            Rot270CCW,
            FlipHorNormal,
            FlipHorRot90CCW,
            FlipHorRot180CCW,
            FlipHorRot270CCW,
            FlipVerNormal,
            FlipVerRot90CCW,
            FlipVerRot180CCW,
            FlipVerRot270CCW,
            FlipHorVerNormal,
            FlipHorVerRot90CCW,
            FlipHorVerRot180CCW,
            FlipHorVerRot270CCW,
        }

        private char[,] Transform(char[,] picture, OrientationType otype)
        {
            var size = picture.GetLength(0);
            if (size != picture.GetLength(1))
                return null;
            var size_1 = size - 1;
            var p = new char[size, size];

            for (var row = 0; row < size; row++)
            {
                for (var col = 0; col < size; col++)
                {
                    p[row, col] = GetChar(row, col);
                }
            }
            return p;

            char GetChar(int row, int col)
            {
                return otype switch
                {
                    OrientationType.Normal => picture[row, col],
                    OrientationType.Rot90CCW => picture[col, size_1 - row],
                    OrientationType.Rot180CCW => picture[size_1 - row, size_1 - col],
                    OrientationType.Rot270CCW => picture[size_1 - col, row],
                    OrientationType.FlipHorNormal => picture[size_1 - row, col],
                    OrientationType.FlipHorRot90CCW => picture[size_1 - col, size_1 - row],
                    OrientationType.FlipHorRot180CCW => picture[row, size_1 - col],
                    OrientationType.FlipHorRot270CCW => picture[col, row],
                    OrientationType.FlipVerNormal => picture[row, size_1 - col],
                    OrientationType.FlipVerRot90CCW => picture[col, row],
                    OrientationType.FlipVerRot180CCW => picture[size_1 - row, col],
                    OrientationType.FlipVerRot270CCW => picture[size_1 - col, size_1 - row],
                    OrientationType.FlipHorVerNormal => picture[size_1 - row, size_1 - col],
                    OrientationType.FlipHorVerRot90CCW => picture[size_1 - col, row],
                    OrientationType.FlipHorVerRot180CCW => picture[row, col],
                    OrientationType.FlipHorVerRot270CCW => picture[col, size_1 - row],
                    _ => throw new NotImplementedException()
                };
            }
        }

    }
}
