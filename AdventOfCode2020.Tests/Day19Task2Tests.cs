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
    public class Day19Task2Tests
    {

        Day19Task2 Solver() => new Day19Task2();
        
        [Test]
        public void solves_input()
        {
            string input = File.ReadAllText("Files\\Day19_2_ex.txt");

            var result = Solver().Solve(input);

            result.Should().Be(3);
        }

        [Test]
        public void solves_input_2()
        {
            string input = File.ReadAllText("Files\\Day19_2_ex.txt");
            input = input.Replace($"8: 42", "8: 42 | 42 8");
            input = input.Replace("11: 42 31", "11: 42 31 | 42 11 31");

            var result = Solver().Solve(input);

            result.Should().Be(12);
        }
    }
}
