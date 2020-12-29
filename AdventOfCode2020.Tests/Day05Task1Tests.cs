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
    public class Day05Task1Tests
    {
        [TestCase("FFFFFFF", 0)]
        [TestCase("FBFBBFF", 44)]
        [TestCase("BBBBBBB", 127)]
        public void finds_row(string input, int expected)
        {
            var result = new Day05Task1().FindRow(input);

            result.Should().Be(expected);
        }

        [TestCase("LLL",0)]
        [TestCase("LLR",1)]
        [TestCase("LRL",2)]
        [TestCase("LRR",3)]
        [TestCase("RLL",4)]
        [TestCase("RLR",5)]
        [TestCase("RRL",6)]
        [TestCase("RRR",7)]
        public void finds_column(string input, int expected)
        {
            var result = new Day05Task1().FindColumn(input);

            result.Should().Be(expected);
        }

        [TestCase("FBFBBFFRLR", 357)]
        [TestCase("BFFFBBFRRR", 567)]
        [TestCase("FFFBBBFRRR", 119)]
        [TestCase("BBFFBBFRLL", 820)]
        public void returns_seat_id(string input, int expected)
        {
            var result = new Day05Task1().FindSeatId(input);

            result.Should().Be(expected);
        }

        [Test]
        public void returns_max_seat_id()
        {
            var input = new[]
            {
                "FBFBBFFRLR",
                "BFFFBBFRRR",
                "FFFBBBFRRR",
                "BBFFBBFRLL"
            };

            var result = new Day05Task1().FindHighestSeatId(input);

            result.Should().Be(820);
        }

        [Test]
        public void returns_959()
        {
            var input = File.ReadAllLines("Files\\Day05.txt");

            var result = new Day05Task1().FindHighestSeatId(input);

            result.Should().Be(959);
        }
    }
}
