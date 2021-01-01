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
    public class Day10Task2Tests
    {

        Day10Task2 _solver = new Day10Task2();
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
        public void count_combinations()
        {
            var result = _solver.Test(input1Values);

            result.Should().Be(8);
        }


        [Test]
        public void count_combinations_2()
        {
            var result = _solver.Test(input2Values);

            result.Should().Be(19208);
        }


        [Test]
        public void count_combinations_442136281481216()
        {
            var input = Parse(File.ReadAllText("Files\\Day10.txt"));
            var result = _solver.Test(input);

            result.Should().Be(442136281481216);
        }

    }
    
}
