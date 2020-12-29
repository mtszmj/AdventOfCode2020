using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day07Task1
    {
        public int Solve(string input, string forColor)
        {
            var rules = ParseRules(input);

            var colorToCheck = new List<string> { forColor };
            var indexToCheck = 0;

            while (indexToCheck < colorToCheck.Count)
            {
                var check = colorToCheck[indexToCheck];
                var newColorsToCheck = rules.Where(x => x.Hold.ContainsKey(check)).Select(x => x.Color);
                var set = colorToCheck.ToHashSet();
                foreach(var c in newColorsToCheck)
                {
                    if(!set.Contains(c))
                        colorToCheck.Add(c);
                }

                indexToCheck++;
            }

            return colorToCheck.Count - 1;
        }

        public List<Rule> ParseRules(string input)
        {
            var rules = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return rules.Select(x => ParseRule(x)).ToList();
        }

        public Rule ParseRule(string rule)
        {
            var (color, holdRule) = SplitToColorAndHoldRule(rule);
            return new Rule(color, ParseHoldRule(holdRule));
        }

        public (string Color, string HoldRule) SplitToColorAndHoldRule(string line)
        {
            var split = line.Split(" bags contain ", StringSplitOptions.RemoveEmptyEntries);
            return (split[0], split[1]);
        }

        public Dictionary<string, int> ParseHoldRule(string holdRule)
        {
            if (holdRule == "no other bags.")
                return new Dictionary<string, int>();

            var rules = holdRule.Split(new[] { ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
            var ruleParts = rules.Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries));
            var entries = ruleParts.Select(x => (Color: $"{x[1]} {x[2]}", Count: int.Parse(x[0])));
            return entries.ToDictionary(x => x.Color, x => x.Count);
        }

        public class Rule
        {
            public Rule(string color, Dictionary<string, int> hold)
            {
                Color = color;
                Hold = new Dictionary<string, int>(hold);
            }

            public string Color { get; }
            public Dictionary<string, int> Hold { get; }
        }
    }
}
