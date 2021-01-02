using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdventOfCode2020.Day12Task2;

namespace AdventOfCode2020.Tests
{
    [TestFixture]
    public class Day12Task2Tests
    {
        Day12Task2 _solver = new Day12Task2();
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

        Positions StartingPositions => new Positions
        (
            Ship: new Position2(0, 0),
            Waypoint: new Position2(1, 10)
        );

        [Test]
        public void moves_waypoint_north()
        {
            var command = new North2(11);

            var result = command.Move(StartingPositions);

            result.Ship.North.Should().Be(0);
            result.Ship.East.Should().Be(0);
            result.Waypoint.North.Should().Be(12);
            result.Waypoint.East.Should().Be(10);
        }


        [Test]
        public void moves_waypoint_south()
        {
            var command = new South2(11);

            var result = command.Move(StartingPositions);

            result.Ship.North.Should().Be(0);
            result.Ship.East.Should().Be(0);
            result.Waypoint.North.Should().Be(-10);
            result.Waypoint.East.Should().Be(10);
        }
        [Test]
        public void moves_waypoint_east()
        {
            var command = new East2(11);

            var result = command.Move(StartingPositions);

            result.Ship.North.Should().Be(0);
            result.Ship.East.Should().Be(0);
            result.Waypoint.North.Should().Be(1);
            result.Waypoint.East.Should().Be(21);
        }
        [Test]
        public void moves_waypoint_west()
        {
            var command = new West2(11);

            var result = command.Move(StartingPositions);

            result.Ship.North.Should().Be(0);
            result.Ship.East.Should().Be(0);
            result.Waypoint.North.Should().Be(1);
            result.Waypoint.East.Should().Be(-1);
        }

        // waypoint N:1, E:10
        [TestCase(90, 10, -1)]
        [TestCase(180, -1, -10)]
        [TestCase(270, -10, 1)]
        [TestCase(360, 1, 10)]
        [TestCase(450, 10, -1)]
        public void rotates_waypoint_left(int angle, int expectedNorth, int expectedEast)
        {
            var command = new Left2(angle);

            var result = command.Move(StartingPositions);

            result.Ship.North.Should().Be(0);
            result.Ship.East.Should().Be(0);
            result.Waypoint.North.Should().Be(expectedNorth);
            result.Waypoint.East.Should().Be(expectedEast);
        }

        // waypoint N:1, E:10
        [TestCase(90, -10, 1)]
        [TestCase(180, -1, -10)]
        [TestCase(270, 10, -1)]
        [TestCase(360, 1, 10)]
        [TestCase(450, -10, 1)]
        public void rotates_waypoint_right(int angle, int expectedNorth, int expectedEast)
        {
            var command = new Right2(angle);

            var result = command.Move(StartingPositions);

            result.Ship.North.Should().Be(0);
            result.Ship.East.Should().Be(0);
            result.Waypoint.North.Should().Be(expectedNorth);
            result.Waypoint.East.Should().Be(expectedEast);
        }

        // waypoint N:1, E:10
        [TestCase(1, 1, 10)]
        [TestCase(10, 10, 100)]
        public void moves_ship_forward(int forward, int expectedNorth, int expectedEast)
        {
            var command = new Forward2(forward);

            var result = command.Move(StartingPositions);

            result.Ship.North.Should().Be(expectedNorth);
            result.Ship.East.Should().Be(expectedEast);
            result.Waypoint.North.Should().Be(1);
            result.Waypoint.East.Should().Be(10);
        }


        [Test]
        public void solves_manhattan_distance_as_286()
        {
            var result = _solver.Solve(input, StartingPositions);
            result.Should().Be(286);
        }

        [Test]
        public void solves_manhattan_distance_as_26841()
        {
            var result = _solver.Solve(File.ReadAllText("Files\\Day12.txt"), StartingPositions);
            result.Should().Be(26841);
        }
    }
}
