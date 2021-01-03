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
    public class Day13Task1Tests
    {
        Day13Task1 _solver = new Day13Task1();
        string input = @"939
7,13,x,x,59,x,31,19";

        [Test]
        public void parses_input()
        {
            var (earliest, buses) = _solver.Parse(input);

            earliest.Should().Be(939);
            buses.Should().SatisfyRespectively(
                a => a.Should().Be(7),
                b => b.Should().Be(13),
                b => b.Should().Be(59),
                b => b.Should().Be(31),
                b => b.Should().Be(19)
                );
        }

        [Test]
        public void finds_buses_departures_after_or_at_given_time()
        {
            var departures = _solver.FindDepartures(939, new List<int>() { 7, 13, 59, 31, 19 });

            departures.Should().ContainKeys(7, 13, 59, 31, 19);
            departures[7].Should().Be(945);
            departures[13].Should().Be(949);
            departures[59].Should().Be(944);
            departures[31].Should().Be(961);
            departures[19].Should().Be(950);
        }

        [Test]
        public void solves_as_295()
        {
            var result = _solver.Solve(input);

            result.Should().Be(295);
        }
        [Test]
        public void solves_as_156()
        {
            var result = _solver.Solve(File.ReadAllText("Files\\Day13.txt"));

            result.Should().Be(156);
        }
    }
}
