using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day16Task1
    {
        private static string NL = Environment.NewLine;
        
        public virtual long Solve(string input)
        {
            var data = Parse(input);

            var allValid = data.Rules.SelectMany(x => x.Value.ValidNumbers).ToHashSet();
            var sum = 0;
            foreach(var ticket in data.OtherTickets)
            {
                sum += ticket.Numbers.Where(x => !allValid.Contains(x)).Sum();
            }
            return sum;
        }


        public Data Parse(string input)
        {
            var parts = input.Split($"{NL}{NL}", StringSplitOptions.RemoveEmptyEntries);

            return new Data(
                ParseRules(parts[0]).ToDictionary(x => x.Name, x => x), 
                ParseYourTicket(parts[1]), 
                ParseOtherTickets(parts[2]).ToList()
                );

        }

        public IEnumerable<Rule> ParseRules(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var match = Regex.Match(line, @"^(?<name>.+): (?<val11>\d+)\-(?<val12>\d+) or (?<val21>\d+)-(?<val22>\d+)$");

                var name = match.Groups["name"].Value;
                var val11 = int.Parse(match.Groups["val11"].Value);
                var val12 = int.Parse(match.Groups["val12"].Value);
                var val21 = int.Parse(match.Groups["val21"].Value);
                var val22 = int.Parse(match.Groups["val22"].Value);

                var rule = new Rule()
                {
                    Name = name
                };

                foreach (var value in Enumerable.Range(val11, val12 - val11 + 1)
                                                .Concat(Enumerable.Range(val21, val22 - val21 + 1)))
                {
                    rule.ValidNumbers.Add(value);
                }

                yield return rule;
            }
        }

        public Ticket ParseYourTicket(string input)
        {
            var ticket = new Ticket();
            foreach(var value in input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)[1]
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x)))
            {
                ticket.Numbers.Add(value);
            }
            return ticket;
        }

        public IEnumerable<Ticket> ParseOtherTickets(string input)
        {
            foreach (var line in input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Skip(1))
            {
                var ticket = new Ticket();
                foreach (var value in line.Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => int.Parse(x)))
                {
                    ticket.Numbers.Add(value);
                }

                yield return ticket;
            }
        }


        public class Rule
        {
            public string Name { get; set; }
            public HashSet<int> ValidNumbers { get; } = new HashSet<int>();
        }

        public class Ticket
        {
            public List<int> Numbers { get; } = new List<int>();
        }

        public class Data
        {
            public Data(Dictionary<string, Rule> rules,
                Ticket yourTicket,
                List<Ticket> otherTickets
                )
            {
                Rules = rules;
                YourTicket = yourTicket;
                OtherTickets = otherTickets;
            }

            public Dictionary<string, Rule> Rules { get; } 
            public Ticket YourTicket { get; }
            public List<Ticket> OtherTickets { get; } 
        }
    }
}
