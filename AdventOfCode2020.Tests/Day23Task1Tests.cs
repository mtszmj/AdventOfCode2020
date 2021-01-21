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
    public class Day23Task1Tests
    {
        Day23Task1 Solver() => new Day23Task1();

        [Test]
        public void solves_example()
        {
            var input = "389125467";
            var solver = Solver();

            var result = solver.Solve(input);

            result.Should().Be("67384529");
        }

        [Test]
        public void solves_input()
        {
            var input = "389547612";
            var solver = Solver();

            var result = solver.Solve(input);

            result.Should().Be("45286397");
        }

    }
}
