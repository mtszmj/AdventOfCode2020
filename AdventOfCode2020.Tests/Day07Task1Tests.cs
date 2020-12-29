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
    public class Day07Task1Tests
    {
        Day07Task1 solver = new Day07Task1();

        string[] inputAsLines = new[]
        {
            "light red bags contain 1 bright white bag, 2 muted yellow bags.",
            "dark orange bags contain 3 bright white bags, 4 muted yellow bags.",
            "bright white bags contain 1 shiny gold bag.",
            "muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.",
            "shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.",
            "dark olive bags contain 3 faded blue bags, 4 dotted black bags.",
            "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.",
            "faded blue bags contain no other bags.",
            "dotted black bags contain no other bags."
        };

        [Test]
        public void bags_that_can_contain_shiny_gold_count_4()
        {
            var input =
@"light red bags contain 1 bright white bag, 2 muted yellow bags.
dark orange bags contain 3 bright white bags, 4 muted yellow bags.
bright white bags contain 1 shiny gold bag.
muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
dark olive bags contain 3 faded blue bags, 4 dotted black bags.
vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
faded blue bags contain no other bags.
dotted black bags contain no other bags.";

            var result = solver.Solve(input, "shiny gold");

            result.Should().Be(4);
        }

        [Test]
        public void bags_that_can_contain_shiny_gold_count_300()
        {
            var input = File.ReadAllText("Files\\Day07.txt");

            var result = solver.Solve(input, "shiny gold");

            result.Should().Be(300);
        }

        [TestCase("faded blue bags contain no other bags.", "faded blue")]
        [TestCase("dotted black bags contain no other bags.", "dotted black")]
        public void parses_rule_with_no_other_bags(string input, string color)
        {
            var result = solver.ParseRule(input);

            result.Color.Should().Be(color);
            result.Hold.Should().BeEmpty();
        }
        
        [TestCaseSource(nameof(Input))]
        public void parses_rule(string input, string color, Dictionary<string, int> rules)
        {
            var result = solver.ParseRule(input);

            result.Color.Should().Be(color);
            result.Hold.Should().BeEquivalentTo(rules);
        }



        [TestCase("light red bags contain 1 bright white bag, 2 muted yellow bags.", "light red", "1 bright white bag, 2 muted yellow bags.")]
        [TestCase("dark orange bags contain 3 bright white bags, 4 muted yellow bags.", "dark orange", "3 bright white bags, 4 muted yellow bags.")]
        [TestCase("bright white bags contain 1 shiny gold bag.", "bright white", "1 shiny gold bag.")]
        [TestCase("muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.", "muted yellow", "2 shiny gold bags, 9 faded blue bags.")]
        [TestCase("shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.", "shiny gold", "1 dark olive bag, 2 vibrant plum bags.")]
        [TestCase("dark olive bags contain 3 faded blue bags, 4 dotted black bags.", "dark olive", "3 faded blue bags, 4 dotted black bags.")]
        [TestCase("vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.", "vibrant plum", "5 faded blue bags, 6 dotted black bags.")]
        [TestCase("faded blue bags contain no other bags.", "faded blue", "no other bags.")]
        [TestCase("dotted black bags contain no other bags.", "dotted black", "no other bags.")]
        public void splits_to_color_and_rule(string line, string color, string holdRule)
        {
            var result = solver.SplitToColorAndHoldRule(line);

            result.Color.Should().Be(color);
            result.HoldRule.Should().Be(holdRule);
        }

        [TestCaseSource(nameof(HoldRulesTestCases))]
        public void parses_hold_rule(string holdRule, Dictionary<string, int> rules)
        {
            var result = solver.ParseHoldRule(holdRule);

            result.Should().BeEquivalentTo(rules);
        }

        public static object[] Input()
        {
            return new object[]
            {
                new object[] 
                { 
                    "light red bags contain 1 bright white bag, 2 muted yellow bags.",
                    "light red",
                    new Dictionary<string, int> 
                    {
                        ["bright white"] = 1,
                        ["muted yellow"] = 2
                    } 
                },
                new object[]
                {
                    "dark orange bags contain 3 bright white bags, 4 muted yellow bags.",
                    "dark orange",
                    new Dictionary<string, int>
                    {
                        ["bright white"] = 3,
                        ["muted yellow"] = 4
                    }
                },
                new object[]
                {
                    "bright white bags contain 1 shiny gold bag.",
                    "bright white",
                    new Dictionary<string, int>
                    {
                        ["shiny gold"] = 1,
                    }
                },
                new object[]
                {
                    "muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.",
                    "muted yellow",
                    new Dictionary<string, int>
                    {
                        ["shiny gold"] = 2,
                        ["faded blue"] = 9
                    }
                },
                new object[]
                {
                    "shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.",
                    "shiny gold",
                    new Dictionary<string, int>
                    {
                        ["dark olive"] = 1,
                        ["vibrant plum"] = 2
                    }
                },
                new object[]
                {
                    "dark olive bags contain 3 faded blue bags, 4 dotted black bags.",
                    "dark olive",
                    new Dictionary<string, int>
                    {
                        ["faded blue"] = 3,
                        ["dotted black"] = 4
                    }
                },
                new object[]
                {
                    "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.",
                    "vibrant plum",
                    new Dictionary<string, int>
                    {
                        ["faded blue"] = 5,
                        ["dotted black"] = 6
                    }
                },
                new object[]
                {
                    "faded blue bags contain no other bags.",
                    "faded blue",
                    new Dictionary<string, int>()
                },
                new object[]
                {
                    "dotted black bags contain no other bags.",
                    "dotted black",
                    new Dictionary<string, int>()
                },
            };
        }

        public static object[] HoldRulesTestCases()
        {
            return new object[]
            {
                new object[]
                {
                    "1 bright white bag, 2 muted yellow bags.",
                    new Dictionary<string, int>
                    {
                        ["bright white"] = 1,
                        ["muted yellow"] = 2
                    }
                },
                new object[]
                {
                    "3 bright white bags, 4 muted yellow bags.",
                    new Dictionary<string, int>
                    {
                        ["bright white"] = 3,
                        ["muted yellow"] = 4
                    }
                },
                new object[]
                {
                    "1 shiny gold bag.",
                    new Dictionary<string, int>
                    {
                        ["shiny gold"] = 1,
                    }
                },
                new object[]
                {
                    "2 shiny gold bags, 9 faded blue bags.",
                    new Dictionary<string, int>
                    {
                        ["shiny gold"] = 2,
                        ["faded blue"] = 9
                    }
                },
                new object[]
                {
                    "1 dark olive bag, 2 vibrant plum bags.",
                    new Dictionary<string, int>
                    {
                        ["dark olive"] = 1,
                        ["vibrant plum"] = 2
                    }
                },
                new object[]
                {
                    "3 faded blue bags, 4 dotted black bags.",
                    new Dictionary<string, int>
                    {
                        ["faded blue"] = 3,
                        ["dotted black"] = 4
                    }
                },
                new object[]
                {
                    "5 faded blue bags, 6 dotted black bags.",
                    new Dictionary<string, int>
                    {
                        ["faded blue"] = 5,
                        ["dotted black"] = 6
                    }
                },
                new object[]
                {
                    "no other bags.",
                    new Dictionary<string, int>()
                },
            };
        }

    }
}
