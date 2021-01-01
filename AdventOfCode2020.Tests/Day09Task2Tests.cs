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
    public class Day09Task2Tests
    {
        Day09Task2 _solver = new Day09Task2();

        [Test]
        public void finds_set_summing_to_127()
        {
            var input = new long[]
            { 35, 20, 15, 25, 47, 40, 62, 55, 65, 95, 102, 117, 150, 182, 127, 219, 299, 277, 309, 576 };

            var result = _solver.FindSetWithSumOfGivenValue(127, input);
            result.left.Should().Be(2);
            result.right.Should().Be(5);
        }


        [Test]
        public void sums_min_and_max_of_set()
        {
            var input = new long[]
            { 35, 20, 15, 25, 47, 40, 62, 55, 65, 95, 102, 117, 150, 182, 127, 219, 299, 277, 309, 576 };

            var result = _solver.SumMinAndMax(2, 5, input);

            result.Should().Be(62);
        }

        [Test]
        public void solves_with_62()
        {
            var input =
@"35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576";

            var result = _solver.Solve(input, 5);

            result.Should().Be(62);
        }
        [Test]
        public void solves_with_N()
        {
            var input = File.ReadAllText("Files\\Day09.txt");

            var result = _solver.Solve(input, 25);

            result.Should().Be(35602097);
        }
    }
}
