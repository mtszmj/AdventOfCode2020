using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day17Task1
    {
        public void Solve(string input, int cycles)
        {
            Initialize(input);

            for (var c = 0; c < cycles; c++)
            {
                var changes = new List<Cube>();

                for (var i = _minX - 1; i <= _maxX + 1; i++)
                    for (var j = _minY - 1; j <= _maxY + 1; j++)
                        for (var k = _minZ - 1; k <= _maxZ + 1; k++)
                        {
                            var neightbours = CountNeightbours(i, j, k);
                            if (this[i, j, k] == State.Active
                                && (neightbours[State.Active] < 2 || neightbours[State.Active] > 3))
                                changes.Add(new Cube(i, j, k, State.Inactive));
                            else if (this[i, j, k] == State.Inactive
                                && neightbours[State.Active] == 3)
                                changes.Add(new Cube(i, j, k, State.Active));
                        }

                foreach(var cube in changes)
                {
                    this[cube.X, cube.Y, cube.Z] = cube.State;
                }

                _minX--;
                _maxX++;
                _minY--;
                _maxY++;
                _minZ--;
                _maxZ++;
            }
        }

        int _minX = 0;
        int _maxX = 0;
        int _minY = 0;
        int _maxY = 0;
        int _minZ = 0;
        int _maxZ = 0;

        public State this[int x, int y, int z]
        {
            get
            {
                if (!Cubes.ContainsKey((x, y, z)))
                    return State.Inactive;

                return Cubes[(x, y, z)].State;
            }
            set
            {
                if (Cubes.ContainsKey((x, y, z)))
                    Cubes[(x, y, z)].State = value;
                else
                    Cubes[(x, y, z)] = new Cube { X = x, Y = y, Z = z, State = value };
            }
        }

        public void Initialize(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var x = 0;
            var y = 0;
            var z = 0;
            foreach (var line in lines)
            {
                x = 0;
                foreach (var ch in line)
                    this[x++, y, z] = ch == '.' ? State.Inactive : State.Active;
                y++;
            }

            _maxX = x - 1;
            _maxY = y - 1;
        }



        Dictionary<(int, int, int), Cube> Cubes { get; } = new Dictionary<(int, int, int), Cube>();

        public Dictionary<State, int> CountNeightbours(int x, int y, int z)
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
                        if (i == x && j == y && k == z)
                            continue;

                        result[this[i, j, k]]++;
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

            public Cube(int x, int y, int z, State state)
            {
                X = x;
                Y = y;
                Z = z;
                State = state;
            }

            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
            public State State { get; set; }
        }

        public enum State
        {
            Inactive,
            Active
        }
    }
}
