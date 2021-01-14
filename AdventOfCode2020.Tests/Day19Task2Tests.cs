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
        public void solves_example_from_part_1()
        {
            string input = @"0: 4 1 5
1: 2 3 | 3 2
2: 4 4 | 5 5
3: 4 5 | 5 4
4: ""a""
5: ""b""

ababbb
bababa
abbbab
aaabbb
aaaabbb";
            var result = Solver().Solve(input);

            result.Should().Be(2);
        }

        [Test]
        public void solves_input_from_part_1()
        {
            string input = File.ReadAllText("Files\\Day19.txt");

            var result = Solver().Solve(input);

            result.Should().Be(195);
        }

        [Test]
        public void solves_input_2_from_part_1()
        {
            string input = File.ReadAllText("Files\\Day19_1.txt");

            var result = Solver().Solve(input);

            result.Should().Be(230);
        }

        [Test]
        public void solves_input_2()
        {
            string input = File.ReadAllText("Files\\Day19_2_ex.txt");
            input = input.Replace("8: 42", "8: 42 | 42 8");
            input = input.Replace("11: 42 31", "11: 42 31 | 42 11 31");

            var result = Solver().Solve(input);

            result.Should().Be(12);
        }


        [TestCase("bbabbbbaabaabba", 1)]
        [TestCase("babbbbaabbbbbabbbbbbaabaaabaaa", 1)]
        [TestCase("aaabbbbbbaaaabaababaabababbabaaabbababababaaa", 1)]
        [TestCase("bbbbbbbaaaabbbbaaabbabaaa", 1)]
        [TestCase("bbbababbbbaaaaaaaabbababaaababaabab", 1)]
        [TestCase("ababaaaaaabaaab", 1)]
        [TestCase("ababaaaaabbbaba", 1)]
        [TestCase("baabbaaaabbaaaababbaababb", 1)]
        [TestCase("abbbbabbbbaaaababbbbbbaaaababb", 1)]
        [TestCase("aaaaabbaabaaaaababaa", 1)]
        [TestCase("aaaabbaabbaaaaaaabbbabbbaaabbaabaaa", 1)]
        [TestCase("aabbbbbaabbbaaaaaabbbbbababaaaaabbaaabba", 1)]
        [TestCase("abbbbbabbbaaaababbaabbbbabababbbabbbbbbabaaaa", 0)]
        [TestCase("aaaabbaaaabbaaa", 0)]
        [TestCase("babaaabbbaaabaababbaabababaaab", 0)]
        public void solves_input_2_one_by_one(string msg, int expected)
        {
            string input = File.ReadAllText("Files\\Day19_2_ex.txt");
            input = input.Replace("8: 42", "8: 42 | 42 8");
            input = input.Replace("11: 42 31", "11: 42 31 | 42 11 31");
            input = input.Split($"{Environment.NewLine}{Environment.NewLine}")[0] + Environment.NewLine + Environment.NewLine + msg;
            
            var result = Solver().Solve(input);

            result.Should().Be(expected);
        }
    }
}
