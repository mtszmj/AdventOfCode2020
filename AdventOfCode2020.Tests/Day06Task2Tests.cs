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
    public class Day06Task2Tests
    {
        Day06Task2 _solver => new Day06Task2();

        [Test]
        public void sums_yeses_to_6()
        {
            var input =
@"abc

a
b
c

ab
ac

a
a
a
a

b";
            var result = _solver.Solve(input);

            result.Should().Be(6);
        }
        
        [Test]
        public void sums_yeses_to_3260()
        {
            var input = File.ReadAllText("Files\\Day06.txt");
            var result = _solver.Solve(input);

            result.Should().Be(3260);
        }
    }
}
