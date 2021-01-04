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
    public class Day13Task2Tests
    {
        Day13Task2 _solver = new Day13Task2();

        [Test]
        public void solve_example_1()
        {

            //7,13,x,x,59,x,31,19
            var bus0 = 7;
            var offsets = new Dictionary<long, long>
            {
                [1] = 13,
                [4] = 59,
                [6] = 31,
                [7] = 19,
            };

            var result = _solver.SolveV1(bus0, offsets);

            result.Should().Be(1068781);
        }

        [Test]
        public void solve_example_2()
        {
            var bus0 = 17;
            var offsets = new Dictionary<long, long>
            {
                [2] = 13,
                [3] = 19
            };

            var result = _solver.SolveV1(bus0, offsets);

            result.Should().Be(3417);
        }

        [Test]
        public void solve_example_3()
        {
            var bus0 = 67;
            var offsets = new Dictionary<long, long>
            {
                [1] = 7,
                [2] = 59,
                [3] = 61
            };

            var result = _solver.SolveV1(bus0, offsets);

            result.Should().Be(754018);
        }

        [Test]
        public void solve_example_4()
        {
            var bus0 = 67;
            var offsets = new Dictionary<long, long>
            {
                [2] = 7,
                [3] = 59,
                [4] = 61
            };

            var result = _solver.SolveV1(bus0, offsets);

            result.Should().Be(779210);
        }

        [Test]
        public void solve_example_5()
        {
            var bus0 = 67;
            var offsets = new Dictionary<long, long>
            {
                [1] = 7,
                [3] = 59,
                [4] = 61
            };

            var result = _solver.SolveV1(bus0, offsets);

            result.Should().Be(1261476);
        }

        [Test]
        public void solve_example_6()
        {
            var bus0 = 1789;
            var offsets = new Dictionary<long, long>
            {
                [1] = 37,
                [2] = 47,
                [3] = 1889
            };

            var result = _solver.SolveV1(bus0, offsets);

            result.Should().Be(1202161486);
        }

        [Test]
        public void solve_example_1_from_input()
        {

            var input = @"_
7,13,x,x,59,x,31,19";

            var result = _solver.Solve(input, 1000000);

            result.Should().Be(1068781);
        }

        [Test]
        public void solve_example_from_input()
        {

            var input = File.ReadAllText("Files\\Day13.txt");

            var result = _solver.Solve(input);

            result.Should().Be(404517869995362L);
        }

        [Test]
        public void solve_example_2_full()
        {
            var input = @"_
17,x,13,19";
            
            var result = _solver.Solve(input, 1234);

            result.Should().Be(3417);
        }
    }
}
