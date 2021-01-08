using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day17Task2
    {
        public int Solve(string input, int cycles)
        {
            Initialize(input);

            for (var c = 0; c < cycles; c++)
            {
                var changes = new List<Cube>();

                for (var i = _minX - 1; i <= _maxX + 1; i++)
                    for (var j = _minY - 1; j <= _maxY + 1; j++)
                        for (var k = _minZ - 1; k <= _maxZ + 1; k++)
                            for (var w = _minW - 1; w <= _maxW + 1; w++)
                            {
                                var neightbours = CountNeightbours(i, j, k, w);
                                if (this[i, j, k, w] == State.Active
                                    && (neightbours[State.Active] < 2 || neightbours[State.Active] > 3))
                                    changes.Add(new Cube(i, j, k, w, State.Inactive));
                                else if (this[i, j, k, w] == State.Inactive
                                    && neightbours[State.Active] == 3)
                                    changes.Add(new Cube(i, j, k, w, State.Active));
                            }

                foreach(var cube in changes)
                {
                    this[cube.X, cube.Y, cube.Z, cube.W] = cube.State;
                }

                _minX--;
                _maxX++;
                _minY--;
                _maxY++;
                _minZ--;
                _maxZ++;
                _minW--;
                _maxW++;
            }

            return Cubes.Values.Count(x => x.State == State.Active);
        }

        int _minX = 0;
        int _maxX = 0;
        int _minY = 0;
        int _maxY = 0;
        int _minZ = 0;
        int _maxZ = 0;
        int _minW = 0;
        int _maxW = 0;

        public State this[int x, int y, int z, int w]
        {
            get
            {
                if (!Cubes.ContainsKey((x, y, z, w)))
                    return State.Inactive;

                return Cubes[(x, y, z, w)].State;
            }
            set
            {
                if (Cubes.ContainsKey((x, y, z, w)))
                    Cubes[(x, y, z, w)].State = value;
                else
                    Cubes[(x, y, z, w)] = new Cube { X = x, Y = y, Z = z, W = w, State = value };
            }
        }

        public void Initialize(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var x = 0;
            var y = 0;
            var z = 0;
            var w = 0;
            foreach (var line in lines)
            {
                x = 0;
                foreach (var ch in line)
                    this[x++, y, z, w] = ch == '.' ? State.Inactive : State.Active;
                y++;
            }

            _maxX = x - 1;
            _maxY = y - 1;
        }



        Dictionary<(int, int, int, int), Cube> Cubes { get; } = new Dictionary<(int, int, int, int), Cube>();

        public Dictionary<State, int> CountNeightbours(int x, int y, int z, int w)
        {
            var result = new Dictionary<State, int>
            {
                [State.Inactive] = 0,
                [State.Active] = 0
            };

            for (var i = x - 1; i <= x + 1; i++)
            {
                for (var j = y - 1; j <= y + 1; j++)
                {
                    for (var k = z - 1; k <= z + 1; k++)
                    {
                        for (var m = w - 1; m <= w + 1; m++)
                        {
                            if (i == x && j == y && k == z && m == w)
                                continue;

                            result[this[i, j, k, m]]++;
                        }
                    }
                }
            }

            return result;
        }

        public class Cube
        {
            public Cube()
            {

            }

            public Cube(int x, int y, int z, int w, State state)
            {
                X = x;
                Y = y;
                Z = z;
                W = w;
                State = state;
            }

            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
            public int W { get; set; }
            public State State { get; set; }
        }

        public enum State
        {
            Inactive,
            Active
        }
    }
}
