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
    public class Day06Task1Tests
    {
        Day06Task1 _solver => new Day06Task1();

        [Test]
        public void splits_to_groups()
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
            var result = _solver.SplitToGroups(input);

            result.Should().Contain(new[]
            {
                "abc",
                @"a
b
c",
                @"ab
ac",
                @"a
a
a
a",
                "b"
            });
        }

        [Test]
        public void sums_yeses_to_11()
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

            result.Should().Be(11);
        }
        
        [Test]
        public void sums_yeses_to_6457()
        {
            var input = File.ReadAllText("Files\\Day06.txt");
            var result = _solver.Solve(input);

            result.Should().Be(6457);
        }
    }
}
