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
    public class Day05Task2Tests
    {
        [Test]
        public void finds_seat_527()
        {
            var solver = new Day05Task2();
            var input = File.ReadAllLines("Files\\Day05.txt");

            var result = solver.FindSeatIdWithMinusOneAndPlusOnePresent(input);

            result.Should().Be(527);
        }

    }

}
