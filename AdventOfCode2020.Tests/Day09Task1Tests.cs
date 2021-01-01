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
    public class Day09Task1Tests
    {
        Day09Task1 solver = new Day09Task1();
        long[] _window = new[] { 1L, 3, 5, 7, 10, 21, 13, 21 };

        [TestCase(4)]
        [TestCase(6)]
        [TestCase(8)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(14)]
        [TestCase(22)]
        [TestCase(13)]
        [TestCase(16)]
        [TestCase(24)]
        [TestCase(12)]
        [TestCase(15)]
        [TestCase(18)]
        [TestCase(26)]
        [TestCase(17)]
        [TestCase(20)]
        [TestCase(28)]
        [TestCase(23)]
        [TestCase(31)]
        [TestCase(34)]
        public void sum_of_two_different_element_exist(int value)
        {
            var result = solver.DoesSumExist(value, _window);

            result.Should().BeTrue();
        }

        [TestCase(5)]
        [TestCase(7)]
        [TestCase(9)]
        [TestCase(19)]
        [TestCase(21)]
        [TestCase(25)]
        [TestCase(27)]
        [TestCase(29)]
        [TestCase(30)]
        [TestCase(32)]
        [TestCase(33)]
        [TestCase(42)]
        public void sum_of_two_different_element_does_not_exist(int value)
        {
            var result = solver.DoesSumExist(value, _window);

            result.Should().BeFalse();
        }

        [Test]
        public void solves_127()
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
            var result = solver.Solve(input, 5);

            result.Should().Be(127);
        }

        [Test]
        public void solves_257342611()
        {
            var input = File.ReadAllText("Files\\Day09.txt");
            var result = solver.Solve(input, 25);

            result.Should().Be(257342611L);
        }
    }
}
