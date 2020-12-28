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
    public class Day03Task1Tests
    {
        [Test]
        public void count_7_trees()
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

            var result = new Day03Task1().Count(input, 3, 1);

            result.Should().Be(7);
        }

        [Test]
        public void count_200_trees()
        {
            var input = File.ReadAllText("Files\\Day03.txt");

            var result = new Day03Task1().Count(input, 3, 1);

            result.Should().Be(200);
        }
    }
}
