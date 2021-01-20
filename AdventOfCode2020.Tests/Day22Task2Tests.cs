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
    public class Day22Task2Tests
    {
        Day22Task2 Solver() => new Day22Task2();
        string Example() => @"Player 1:
9
2
6
3
1

Player 2:
5
8
4
7
10";

        [Test]
        public void solves_example()
        {
            var input = Example();
            var solver = Solver();

            var result = solver.Solve(input);

            result.Should().Be(291);
        }

        [Test]
        public void solves_input()
        {
            var input = File.ReadAllText("Files\\Day22.txt");
            var solver = Solver();

            var result = solver.Solve(input);

            result.Should().Be(31151);
        }
    }
}
