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
    public class Day18Task2Tests
    {
        Day18Task2 Solver() => new Day18Task2();
        
        [TestCase("1 + 2 * 3 + 4 * 5 + 6", 231)]
        [TestCase("1 + (2 * 3) + (4 * (5 + 6))", 51)]
        [TestCase("2 * 3 + (4 * 5)", 46)]
        [TestCase("5 + (8 * 3 + 9 + 3 * 4 * 3)", 1445)]
        [TestCase("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 669060)]
        [TestCase("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 23340)]
        public void solves_example(string input, long expected)
        {
            var solver = Solver();

            var result = solver.SolveOne(input);

            result.Should().Be(expected);
        }

        [Test]
        public void solves_sum_of_lines()
        {
            var input = @"1 + 2 * 3 + 4 * 5 + 6
1 + (2 * 3) + (4 * (5 + 6))
2 * 3 + (4 * 5)
5 + (8 * 3 + 9 + 3 * 4 * 3)
5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))
((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2";
            var solver = Solver();

            var result = solver.Solve(input);

            result.Should().Be(231 + 51 + 46 + 1445 + 669060 + 23340);
        }

        [Test]
        public void solves_input_from_file()
        {
            var input = File.ReadAllText("Files\\Day18.txt");
            var solver = Solver();

            var result = solver.Solve(input);

            result.Should().Be(1);
        }

    }
}
