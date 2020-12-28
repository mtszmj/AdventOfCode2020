using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Tests
{
    [TestFixture]
    class Day03Task2Tests
    {
        [Test]
        public void count_multiply_as_336()
        {
            var input =
@"..##.......
#...#...#..
.#....#..#.
..#.#...#.#
.#...##..#.
..#.##.....
.#.#.#....#
.#........#
#.##...#...
#...##....#
.#..#...#.#";

            var result = new Day03Task2().CompareRoutes(input, new List<(int, int)>
            {
                (1, 1),
                (3, 1),
                (5, 1),
                (7, 1),
                (1, 2)
            });

            result.Should().Be(336);
        }

        [Test]
        public void count_3737923200_trees()
        {
            var input = File.ReadAllText("Files\\Day03.txt");

            var result = new Day03Task2().CompareRoutes(input, new List<(int, int)>
            {
                (1, 1),
                (3, 1),
                (5, 1),
                (7, 1),
                (1, 2)
            });

            result.Should().Be(3737923200);
        }
    }
}
