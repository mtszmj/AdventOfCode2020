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
    public class Day23Task2Tests
    {
        Day23Task2 Solver() => new Day23Task2();

        [Test]
        public void solves_example_from_task_1()
        {
            var input = "389125467";
            var solver = Solver();

            var result = solver.Solve(input, input.Length, 100);

            result.Should().Be(6*7);
        }

        [Test]
        public void solves_example()
        {
            var input = "389125467";
            var solver = Solver();

            var result = solver.Solve(input, 1000000, 10000000);

            result.Should().Be(149245887792);
        }

        [Test]
        public void solves_input()
        {
            var input = "389547612";
            var solver = Solver();

            var result = solver.Solve(input, 1000000, 10000000);

            result.Should().Be(836763710);
        }

    }
}
