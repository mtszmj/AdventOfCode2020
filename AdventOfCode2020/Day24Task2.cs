using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day24Task2
    {
        Day24Task1 _task1 = new Day24Task1();

        public int Solve(string input)
        {
            var dict = new Dictionary<(int X, int Y), Tile>();
            PrepareTileFromInput(input, dict);

            for (var day = 1; day <= 100; day++)
            {
                Dictionary<(int X, int Y), Tile> extendedTiles = AddAdjacementTilesToMemory(dict);
                HashSet<(int X, int Y)> toFlip = GetCoordinatesOfTilesToFlip(extendedTiles);
                Flip(extendedTiles, toFlip);

                dict = extendedTiles;
            }

            return dict.Values.Count(x => x.IsBlack);
        }

        private static void Flip(Dictionary<(int X, int Y), Tile> extendedTiles, HashSet<(int X, int Y)> toFlip)
        {
            foreach (var tilePos in toFlip)
                extendedTiles[(tilePos.X, tilePos.Y)].Flip();
        }

        private static HashSet<(int X, int Y)> GetCoordinatesOfTilesToFlip(Dictionary<(int X, int Y), Tile> extendedTiles)
        {
            var toFlip = new HashSet<(int X, int Y)>();
            foreach (var tile in extendedTiles.Values)
            {
                var adjBlacks = tile.CountAdjacementBlackTiles(extendedTiles);

                if (tile.IsBlack && adjBlacks is 0 or > 2)
                    toFlip.Add((tile.X, tile.Y));
                else if (!tile.IsBlack && adjBlacks == 2)
                    toFlip.Add((tile.X, tile.Y));
            }

            return toFlip;
        }

        private static Dictionary<(int X, int Y), Tile> AddAdjacementTilesToMemory(Dictionary<(int X, int Y), Tile> dict)
        {
            var extendTilesInMemory = new Dictionary<(int X, int Y), Tile>(dict);
            foreach (var tile in dict.Values.SelectMany(x => x.AdjacementTiles))
                if (!extendTilesInMemory.ContainsKey((tile.X, tile.Y)))
                    extendTilesInMemory[(tile.X, tile.Y)] = new Tile(tile.X, tile.Y, false);
            return extendTilesInMemory;
        }

        private void PrepareTileFromInput(string input, Dictionary<(int X, int Y), Tile> dict)
        {
            foreach (var line in input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
            {
                var (x, y) = _task1.FindCoordinates(line);
                if (dict.TryGetValue((x, y), out var tile))
                {
                    tile.Flip();
                }
                else
                {
                    dict[(x, y)] = new Tile(x, y, true);
                }
            }
        }

        public class Tile
        {
            public Tile(int x, int y, bool isBlack)
            {
                X = x;
                Y = y;
                IsBlack = isBlack;
                AdjacementTiles = new[]
                {
                    (x - 2, y),
                    (x + 2, y),
                    (x - 1, y - 1),
                    (x + 1, y - 1),
                    (x - 1, y + 1),
                    (x + 1, y + 1),
                };
            }

            public int X { get; }
            public int Y { get; }

            public (int X, int Y)[] AdjacementTiles { get; }

            public bool IsBlack { get; private set; }

            public void Flip()
            {
                IsBlack = !IsBlack;
            }

            public int CountAdjacementBlackTiles(IDictionary<(int X, int Y), Tile> tiles)
            {
                return AdjacementTiles.Count(pos => tiles.TryGetValue(pos, out var tile) && tile.IsBlack);
            }
        }
    }
}
