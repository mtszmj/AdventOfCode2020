using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day24Task1
    {

        public int Solve(string input)
        {
            var dict = new Dictionary<(int X, int Y), bool>();
            foreach(var line in input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
            {
                var (x, y) = FindCoordinates(line);
                if(dict.TryGetValue((x,y), out bool value))
                {
                    dict[(x, y)] = !value;
                }
                else
                {
                    dict[(x, y)] = true;
                }
            }

            return dict.Values.Count(x => x == true);
        }

        public (int X, int Y) FindCoordinates(string line)
        {
            var instructions = ParseInstruction(line);

            var x = 0;
            var y = 0;
            foreach(var i in instructions)
            {
                (x, y) = i.Move(x, y);
            }

            return (x, y);
        }

        public IEnumerable<IInstruction> ParseInstruction(string line)
        {
            string memory = null;
            var index = 0;
            while(index < line.Length)
            {
                if (line[index] == 's' || line[index] == 'n')
                {
                    memory = line.Substring(index, 2);
                    index += 2;
                }
                else memory = line.Substring(index++, 1);

                yield return memory switch
                {
                    "e" => new East(),
                    "se" => new SouthEast(),
                    "sw" => new SouthWest(),
                    "w" => new West(),
                    "nw" => new NorthWest(),
                    "ne" => new NorthEast(),
                    _ => throw new ArgumentException("incorrect value in memory")
                };
            }
        }

        public interface IInstruction
        {
            (int X, int Y) Move(int X, int Y);
        }

        public class East : IInstruction
        {
            public (int X, int Y) Move(int X, int Y)
            {
                return (X + 2, Y);
            }
        }

        public class SouthEast : IInstruction
        {
            public (int X, int Y) Move(int X, int Y)
            {
                return (X + 1, Y - 1);
            }
        }

        public class SouthWest : IInstruction
        {
            public (int X, int Y) Move(int X, int Y)
            {
                return (X - 1, Y - 1);
            }
        }

        public class West : IInstruction
        {
            public (int X, int Y) Move(int X, int Y)
            {
                return (X - 2, Y);
            }
        }

        public class NorthWest : IInstruction
        {
            public (int X, int Y) Move(int X, int Y)
            {
                return (X - 1, Y + 1);
            }
        }

        public class NorthEast : IInstruction
        {
            public (int X, int Y) Move(int X, int Y)
            {
                return (X + 1, Y + 1);
            }
        }
    }
}
