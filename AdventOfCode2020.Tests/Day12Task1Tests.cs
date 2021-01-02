using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdventOfCode2020.Day12Task1;

namespace AdventOfCode2020.Tests
{
    [TestFixture]
    public class Day12Task1Tests
    {
        Day12Task1 _solver = new Day12Task1();
        string input => @"F10
N3
F7
R90
F11";

        string input2 = @"N1
S2
E3
W4
L5
R6
F7";

        Position StartingPostion => new Position(1, 1, Direction.E);

        [Test]
        public void parses_commands()
        {
            var commands = _solver.ParseCommands(input);

            commands.Should().SatisfyRespectively(
                f10 => f10.Should().BeOfType<Forward>().And.Subject.As<Forward>().Value.Should().Be(10),
                n3 => n3.Should().BeOfType<North>().And.Subject.As<North>().Value.Should().Be(3),
                f7 => f7.Should().BeOfType<Forward>().And.Subject.As<Forward>().Value.Should().Be(7),
                r90 => r90.Should().BeOfType<Right>().And.Subject.As<Right>().Value.Should().Be(90),
                f11 => f11.Should().BeOfType<Forward>().And.Subject.As<Forward>().Value.Should().Be(11)
                );
        }

        [Test]
        public void parses_commands_from_input2()
        {
            var commands = _solver.ParseCommands(input2);

            commands.Should().SatisfyRespectively(
                n1 => n1.Should().BeOfType<North>().And.Subject.As<Command>().Value.Should().Be(1),
                s2 => s2.Should().BeOfType<South>().And.Subject.As<Command>().Value.Should().Be(2),
                e3 => e3.Should().BeOfType<East>().And.Subject.As<Command>().Value.Should().Be(3),
                w4 => w4.Should().BeOfType<West>().And.Subject.As<Command>().Value.Should().Be(4),
                l5 => l5.Should().BeOfType<Left>().And.Subject.As<Command>().Value.Should().Be(5),
                r6 => r6.Should().BeOfType<Right>().And.Subject.As<Command>().Value.Should().Be(6),
                f7 => f7.Should().BeOfType<Forward>().And.Subject.As<Command>().Value.Should().Be(7)
                );
        }

        [Test]
        public void moves_north()
        {
            var command = new North(11);

            var result = command.Move(StartingPostion);

            result.North.Should().Be(12);
            result.East.Should().Be(1);
            result.Direction.Should().Be(Direction.E);
        }
        
        [Test]
        public void moves_south()
        {
            var command = new South(11);

            var result = command.Move(StartingPostion);

            result.North.Should().Be(-10);
            result.East.Should().Be(1);
            result.Direction.Should().Be(Direction.E);
        }

        [Test]
        public void moves_east()
        {
            var command = new East(11);

            var result = command.Move(StartingPostion with { Direction = Direction.N });

            result.North.Should().Be(1);
            result.East.Should().Be(12);
            result.Direction.Should().Be(Direction.N);
        }

        [Test]
        public void moves_west()
        {
            var command = new West(11);

            var result = command.Move(StartingPostion);

            result.North.Should().Be(1);
            result.East.Should().Be(-10);
            result.Direction.Should().Be(Direction.E);
        }
        [Test]
        public void moves_forward_north()
        {
            var command = new Forward(11);

            var result = command.Move(StartingPostion with { Direction = Direction.N });

            result.North.Should().Be(12);
            result.East.Should().Be(1);
            result.Direction.Should().Be(Direction.N);
        }
        
        [Test]
        public void moves_forward_south()
        {
            var command = new Forward(11);

            var result = command.Move(StartingPostion with { Direction = Direction.S });

            result.North.Should().Be(-10);
            result.East.Should().Be(1);
            result.Direction.Should().Be(Direction.S);
        }

        [Test]
        public void moves_forward_east()
        {
            var command = new Forward(11);

            var result = command.Move(StartingPostion with { Direction = Direction.E });

            result.North.Should().Be(1);
            result.East.Should().Be(12);
            result.Direction.Should().Be(Direction.E);
        }

        [Test]
        public void moves_forward_west()
        {
            var command = new Forward(11);

            var result = command.Move(StartingPostion with { Direction = Direction.W });

            result.North.Should().Be(1);
            result.East.Should().Be(-10);
            result.Direction.Should().Be(Direction.W);
        }

        [TestCase(90, Direction.S)]
        [TestCase(180, Direction.W)]
        [TestCase(270, Direction.N)]
        [TestCase(360, Direction.E)]
        [TestCase(450, Direction.S)]
        public void rotates_right(int angle, Direction expected)
        {
            var command = new Right(angle);

            var result = command.Move(StartingPostion);

            result.North.Should().Be(1);
            result.East.Should().Be(1);
            result.Direction.Should().Be(expected);
        }

        [TestCase(90, Direction.N)]
        [TestCase(180, Direction.W)]
        [TestCase(270, Direction.S)]
        [TestCase(360, Direction.E)]
        [TestCase(450, Direction.N)]
        public void rotates_left(int angle, Direction expected)
        {
            var command = new Left(angle);

            var result = command.Move(StartingPostion);

            result.North.Should().Be(1);
            result.East.Should().Be(1);
            result.Direction.Should().Be(expected);
        }

        [Test]
        public void solves_manhattan_distance_as_25()
        {
            var result = _solver.Solve(input, new Position(0, 0, Direction.E));
            result.Should().Be(25);
        }

        [Test]
        public void solves_manhattan_distance_as_636()
        {
            var result = _solver.Solve(File.ReadAllText("Files\\Day12.txt"), new Position(0, 0, Direction.E));
            result.Should().Be(636);
        }
    }
}
