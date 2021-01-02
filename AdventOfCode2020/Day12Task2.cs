using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day12Task2
    {
        public int Solve(string input, Positions positions)
        {
            var commands = ParseCommands(input);
            foreach (var cmd in commands)
            {
                positions = cmd.Move(positions);
            }

            return Math.Abs(positions.Ship.North) + Math.Abs(positions.Ship.East);
        }

        public List<Command2> ParseCommands(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return ParseCommands(lines).ToList();
        }

        private IEnumerable<Command2> ParseCommands(string[] lines)
        {
            foreach (var line in lines)
                yield return line[0] switch
                {
                    'N' => new North2(int.Parse(line.Substring(1))),
                    'S' => new South2(int.Parse(line.Substring(1))),
                    'E' => new East2(int.Parse(line.Substring(1))),
                    'W' => new West2(int.Parse(line.Substring(1))),
                    'L' => new Left2(int.Parse(line.Substring(1))),
                    'R' => new Right2(int.Parse(line.Substring(1))),
                    'F' => new Forward2(int.Parse(line.Substring(1))),
                    _ => throw new NotImplementedException()
                };
        }


        public record Positions(Position2 Ship, Position2 Waypoint);

        public record Position2(int North, int East);

        public abstract class Command2
        {
            public Command2(int value)
            {
                Value = value;
            }

            public int Value { get; }
            public abstract Positions Move(Positions positions);
        }

        public class North2 : Command2
        {
            public North2(int value) : base(value) { }

            public override Positions Move(Positions positions)
            {
                return positions with { Waypoint = positions.Waypoint with { North = positions.Waypoint.North + Value } };
            }
        }
        public class South2 : Command2
        {
            public South2(int value) : base(value) { }

            public override Positions Move(Positions positions)
            {
                return positions with { Waypoint = positions.Waypoint with { North = positions.Waypoint.North - Value } };
            }
        }

        public class East2 : Command2
        {
            public East2(int value) : base(value) { }
            public override Positions Move(Positions positions)
            {
                return positions with { Waypoint = positions.Waypoint with { East = positions.Waypoint.East + Value } };
            }
        }
        public class West2 : Command2
        {
            public West2(int value) : base(value) { }
            public override Positions Move(Positions positions)
            {
                return positions with { Waypoint = positions.Waypoint with { East = positions.Waypoint.East - Value } };
            }
        }
        public class Left2 : Command2
        {
            public Left2(int value) : base(value) { }
            public override Positions Move(Positions positions)
            {
                var quarters = (Value / 90) % 4;
                var directionValue = quarters;
                return directionValue switch
                {
                    0 => positions,
                    1 => positions with
                    {
                        Waypoint = positions.Waypoint with
                        {
                            North = positions.Waypoint.East,
                            East = -positions.Waypoint.North
                        }
                    },
                    2 => positions with
                    {
                        Waypoint = positions.Waypoint with
                        {
                            North = -positions.Waypoint.North,
                            East = -positions.Waypoint.East
                        }
                    },
                    3 => positions with
                    {
                        Waypoint = positions.Waypoint with
                        {
                            North = -positions.Waypoint.East,
                            East = positions.Waypoint.North
                        }
                    },
                    _ => throw new NotImplementedException()
                };
            }
        }
        public class Right2 : Command2
        {
            public Right2(int value) : base(value) { }
            public override Positions Move(Positions positions)
            {
                var quarters = (Value / 90) % 4;
                var directionValue = quarters;
                return directionValue switch
                {
                    0 => positions,
                    1 => positions with
                    {
                        Waypoint = positions.Waypoint with
                        {
                            North = -positions.Waypoint.East,
                            East = positions.Waypoint.North
                        }
                    },
                    2 => positions with
                    {
                        Waypoint = positions.Waypoint with
                        {
                            North = -positions.Waypoint.North,
                            East = -positions.Waypoint.East
                        }
                    },
                    3 => positions with
                    {
                        Waypoint = positions.Waypoint with
                        {
                            North = positions.Waypoint.East,
                            East = -positions.Waypoint.North
                        }
                    },
                    _ => throw new NotImplementedException()
                };
            }
        }

        public class Forward2 : Command2
        {
            public Forward2(int value) : base(value) { }
            public override Positions Move(Positions positions)
            {
                return positions with
                {
                    Ship = positions.Ship with
                    {
                        North = positions.Ship.North + (Value * positions.Waypoint.North),
                        East = positions.Ship.East + (Value * positions.Waypoint.East),
                    }
                };
            }
        }
    }
}
