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
    public class Day10Task1Tests
    {
        Day10Task1 _solver = new Day10Task1();
        string input1 = @"16
10
15
5
1
11
7
19
6
12
4";

        long[] input1Values => Parse(input1);

        string input2 = @"28
33
18
42
31
14
46
20
48
47
24
23
49
45
19
38
39
11
1
32
25
35
8
17
7
9
4
2
34
10
3";

        long[] input2Values => Parse(input2);

        private long[] Parse(string input) => input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                                    .Select(x => long.Parse(x))
                                    .ToArray();

        [Test]
        public void finds_device_voltage()
        {
            var result = _solver.FindDeviceVoltage(input1Values);

            result.Should().Be(22);
        }

        [Test]
        public void order_adapters()
        {
            var result = _solver.OrderAdaptersWithDevice(input1Values);

            result.Should().ContainInOrder(1, 4, 5, 6, 7, 10, 11, 12, 15, 16, 19, 22);
        }

        [Test]
        public void count_differences()
        {
            var result = _solver.CountDifferences(input1Values);

            result.Should().ContainKeys(1, 3);
            result[1].Should().Be(7);
            result[3].Should().Be(5);
        }

        [Test]
        public void multiplies_to_35()
        {
            var result = _solver.MultiplyDifferencesOf1And3JoltsOccurances(input1Values);

            result.Should().Be(35);
        }

        [Test]
        public void multiplies_to_220()
        {
            var result = _solver.MultiplyDifferencesOf1And3JoltsOccurances(input2Values);

            result.Should().Be(220);
        }



        [Test]
        public void multiplies_to_2475()
        {
            var input = Parse(File.ReadAllText("Files\\Day10.txt"));
            var result = _solver.MultiplyDifferencesOf1And3JoltsOccurances(input);

            result.Should().Be(2475);
        }
    }
}
