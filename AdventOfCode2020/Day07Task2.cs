using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day07Task2
    {
        Day07Task1 solver = new Day07Task1();

        public int Solve(string input, string forColor)
        {
            var rules = solver.ParseRules(input).ToDictionary(x => x.Color, x => x);

            List<(Day07Task1.Rule Rule, int Multiplier)> Nodes = new List<(Day07Task1.Rule Rule, int Multiplier)>
            {
                (rules[forColor], 1)
            };

            var index = 0;

            var sum = 0;
            while (index < Nodes.Count)
            {
                var node = Nodes[index];
                foreach (var rule in node.Rule.Hold)
                {
                    Nodes.Add((rules[rule.Key], node.Multiplier * rule.Value));
                    sum += node.Multiplier * rule.Value;
                }
                index++;
            }

            return sum;
        }

    }
}
