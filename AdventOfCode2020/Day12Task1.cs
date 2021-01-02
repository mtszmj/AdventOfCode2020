using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day12Task1
    {
        public int Solve(string input, Position position)
        {
            var commands = ParseCommands(input);
            foreach(var cmd in commands)
            {
                position = cmd.Move(position);
            }

            return Math.Abs(position.North) + Math.Abs(position.East);
        }

        public List<Command> ParseCommands(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return ParseCommands(lines).ToList();
        }

        private IEnumerable<Command> ParseCommands(string[] lines)
        {
            foreach (var line in lines)
                yield return line[0] switch
                {
                    'N' => new North(int.Parse(line.Substring(1))),
                    'S' => new South(int.Parse(line.Substring(1))),
                    'E' => new East(int.Parse(line.Substring(1))),
                    'W' => new West(int.Parse(line.Substring(1))),
                    'L' => new Left(int.Parse(line.Substring(1))),
                    'R' => new Right(int.Parse(line.Substring(1))),
                    'F' => new Forward(int.Parse(line.Substring(1))),
                    _ => throw new NotImplementedException()
                };
        }

        public record Position(int North, int East, Direction Direction);

        public enum Direction
        {
            N = 0,
            E = 1,
            S = 2,
            W = 3
        }

        public abstract class Command
        {
            public Command(int value)
            {
                Value = value;
            }

            public int Value { get; }

            public abstract Position Move(Position from);
        }

        public class North : Command
        {
            public North(int value) : base(value) { }
            public override Position Move(Position @from)
            {
                return @from with { North = @from.North + Value };
            }
        }
        public class South : Command
        {
            public South(int value) : base(value) { }
            public override Position Move(Position @from)
            {
                return @from with { North = @from.North - Value };
            }
        }

        public class East : Command
        {
            public East(int value) : base(value) { }
            public override Position Move(Position @from)
            {
                return @from with { East = @from.East + Value };
            }
        }
        public class West : Command
        {
            public West(int value) : base(value) { }
            public override Position Move(Position @from)
            {
                return @from with { East = @from.East - Value };
            }
        }
        public class Left : Command
        {
            public Left(int value) : base(value) { }
            public override Position Move(Position @from)
            {
                var quarters = (Value / 90);
                var directionValue = (int)@from.Direction + (4 - quarters);
                var direction = (Direction)(directionValue % 4);
                return @from with { Direction = direction };
            }
        }
        public class Right : Command
        {
            public Right(int value) : base(value) { }
            public override Position Move(Position @from)
            {
                var quarters = (Value / 90);
                var directionValue = (int)@from.Direction + quarters;
                var direction = (Direction)(directionValue % 4);
                return @from with { Direction = direction };
            }
        }

        public class Forward : Command
        {
            public Forward(int value) : base(value) { }
            public override Position Move(Position @from)
            {
                return @from.Direction switch
                {
                    Direction.N => new North(Value).Move(@from),
                    Direction.E => new East(Value).Move(@from),
                    Direction.S => new South(Value).Move(@from),
                    Direction.W => new West(Value).Move(@from),
                    _ => throw new NotImplementedException(),
                };
            }
        }
    }
}
