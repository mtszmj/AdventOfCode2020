using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdventOfCode2020.Day17Task2;

namespace AdventOfCode2020.Tests
{
    [TestFixture]
    public class Day17Task2Tests
    {
        Day17Task2 Solver() => new Day17Task2();

        [Test]
        public void solves_input()
        {
            var input = @"...#...#
#######.
....###.
.#..#...
#.#.....
.##.....
#.####..
#....##.";

            var solver = Solver();

            var result = solver.Solve(input, 6);
            result.Should().Be(1816);
        }

        [Test]
        public void solves_example()
        {
            var input = @".#.
..#
###";

            var solver = Solver();
            var result = solver.Solve(input, 6);

            result.Should().Be(848);
        }
    }
}
