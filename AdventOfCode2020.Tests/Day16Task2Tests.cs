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
    public class Day16Task2Tests
    {
        Day16Task2 _solver = new Day16Task2();

        [Test]
        public void orders_fields_from_example()
        {
            var input = @"class: 0-1 or 4-19
row: 0-5 or 8-19
seat: 0-13 or 16-19

your ticket:
11,12,13

nearby tickets:
3,9,18
15,1,5
5,14,9";

            var result = _solver.OrderFields(input);

            result.Should().Contain(0, "row");
            result.Should().Contain(1, "class");
            result.Should().Contain(2, "seat");
        }

        [Test]
        public void orders_field_from_input()
        {
            var input = File.ReadAllText("Files\\Day16.txt");

            var result = _solver.OrderFields(input);

            result.Should().HaveCount(20);
            result.Where(x => x.Value.StartsWith("departure")).Should().HaveCount(6);
        }

        [Test]
        public void solves_input()
        {
            var input = File.ReadAllText("Files\\Day16.txt");

            var result = _solver.Solve(input);

            result.Should().Be(3765150732757L);
        }
    }
}
