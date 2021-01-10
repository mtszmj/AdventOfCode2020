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
    public class Day19Task1Tests
    {

        Day19Task1 Solver() => new Day19Task1();
        
        [Test]
        public void solves_example()
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
        public void parse_rules()
        {
            string input = @"0: 4 1 5
1: 2 3 | 3 2
2: 4 4 | 5 5
3: 4 5 | 5 4
4: ""a""
5: ""b""";

            var result = Solver().ParseRules(input);

            var hs = result[0].GetAllowedMessages();
            hs.Should().Contain("ababbb");
            hs.Should().Contain("abbbab");
            hs.Should().NotContain("bababa");
            hs.Should().NotContain("aaabbb");
            hs.Should().NotContain("aaaabbb");

            hs.Should().HaveCount(8);
            hs.Should().Contain("aaaabb");
            hs.Should().Contain("aaabab");
            hs.Should().Contain("abbabb");
            hs.Should().Contain("abbbab");
            hs.Should().Contain("aabaab");
            hs.Should().Contain("aabbbb");
            hs.Should().Contain("abaaab");
            hs.Should().Contain("ababbb");
            //"0: a aa ab b | a aa ba b | a bb ab b | a bb ba b | a ab aa b | a ab bb b | a ba aa b | a ba bb b"
            //"1: aa ab | aa ba | bb ab | bb ba  |  ab aa | ab bb | ba aa | ba bb"
            //"2: a a | b b"
            //"3: a b | b a"
            //"4: a"
            //"5: b"
        }


        [Test]
        public void parse_rules_extended()
        {
            string input = @"0: 6 | 7
7: 1
6: 4 1 5
1: 2 3 | 3 2
2: 4 4 | 5 5
3: 4 5 | 5 4
4: ""a""
5: ""b""";

            var result = Solver().ParseRules(input);

            var hs = result[0].GetAllowedMessages();
            hs.Should().Contain("ababbb");
            hs.Should().Contain("abbbab");
            hs.Should().NotContain("bababa");
            hs.Should().NotContain("aaabbb");
            hs.Should().NotContain("aaaabbb");

            hs.Should().HaveCount(16);
            hs.Should().Contain("aaaabb");
            hs.Should().Contain("aaabab");
            hs.Should().Contain("abbabb");
            hs.Should().Contain("abbbab");
            hs.Should().Contain("aabaab");
            hs.Should().Contain("aabbbb");
            hs.Should().Contain("abaaab");
            hs.Should().Contain("ababbb");

            hs.Should().Contain("aaab");
            hs.Should().Contain("aaba");
            hs.Should().Contain("bbab");
            hs.Should().Contain("bbba");
            hs.Should().Contain("abaa");
            hs.Should().Contain("abbb");
            hs.Should().Contain("baaa");
            hs.Should().Contain("babb");

            //"6: a aa ab b | a aa ba b | a bb ab b | a bb ba b | a ab aa b | a ab bb b | a ba aa b | a ba bb b"
            //"1: aa ab | aa ba | bb ab | bb ba  |  ab aa | ab bb | ba aa | ba bb"
            //"2: a a | b b"
            //"3: a b | b a"
            //"4: a"
            //"5: b"
        }



        [Test]
        public void parse_example_2()
        {
            string input = @"0: 1 | 11
1: ""a""
11: ""b""";

            var result = Solver().ParseRules(input);
            var hs = result[0].GetAllowedMessages();
            hs.Should().Contain("a");
            hs.Should().Contain("b");

        }

        [Test]
        public void solves_input()
        {
            string input = File.ReadAllText("Files\\Day19.txt");

            var result = Solver().Solve(input);

            result.Should().Be(2);
        }

        [Test]
        public void solves_input_2()
        {
            string input = File.ReadAllText("Files\\Day19_1.txt");

            var result = Solver().Solve(input);

            result.Should().Be(230);
        }

        [Test]
        public void check_matches_for_input_2()
        {
            string input = File.ReadAllText("Files\\Day19_1.txt");
            string matches = File.ReadAllText("Files\\Day19_1_matches.txt");

            var rules = Solver().ParseRules(input.Split($"{Environment.NewLine}{Environment.NewLine}")[0]);
            var messages = rules[0].GetAllowedMessages();
            var shouldBeZero = matches.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Where(x => !messages.Contains(x));

            shouldBeZero.Should().HaveCount(0);
        }

        [Test]
        public void check_example_of_two_the_same()
        {
            string input = @"0: 28 28
28: 77 | 91
77: ""a""
91: ""b""";

            var result = Solver().ParseRules(input);

            var hs = result[0].GetAllowedMessages();
            hs.Should().Contain("aa");
            hs.Should().Contain("bb");
            hs.Should().Contain("ab");
            hs.Should().Contain("ba");
        }
    }
}
