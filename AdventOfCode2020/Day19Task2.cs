using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day19Task2
    {
        public long Solve(string input)
        {
            var rulesInput = input.Split($"{Environment.NewLine}{Environment.NewLine}");
            var rules = ParseRules(rulesInput[0]);

            var mainRule = rules[0] as CombinedRule;

            var result = 0;
            foreach(var line in rulesInput[1]
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
            {
                var (isCorrect, index) = RecursiveCheck(mainRule, line, 0);
                if (isCorrect)
                    result++;
            }

            return result;
        }

        public (bool isCorrect, int currentIndex) RecursiveCheck(Rule ruleToCheck, string lineToCheck, int indexToCheck)
        {
            if(ruleToCheck is CombinedRule cmbRule)
            {
                foreach(var subrule in cmbRule.Subrules)
                {
                    var newIndex = indexToCheck;
                    var isCorrect = false;
                    foreach(var rule in subrule.Rules)
                    {
                        (isCorrect, newIndex) = RecursiveCheck(rule, lineToCheck, newIndex);
                        if (!isCorrect)
                            break;
                    }
                    if (isCorrect)
                        return (true, newIndex);
                }

                return (false, indexToCheck);
            }
            else
            {
                if (indexToCheck >= lineToCheck.Length)
                    return (false, indexToCheck);

                if (lineToCheck[indexToCheck] == ruleToCheck.Value[0])
                    return (true, indexToCheck + 1);

                return (false, indexToCheck);
            }

        }


        public Dictionary<int, Rule> ParseRules(string rulesInput)
        {
            Dictionary<int, Rule> rules = CreateDictionaryOfRulesObjects(rulesInput);
            CreateTwoWayTreeStructure(rules);
            CreateSubrulesInCombinedRules(rules);

            return rules;
        }

        private static void CreateSubrulesInCombinedRules(Dictionary<int, Rule> rules)
        {
            foreach (CombinedRule cmbRule in rules.Values.Where(x => x is CombinedRule))
            {
                cmbRule.CreateSubrules(rules);
            }
        }

        private static void CreateTwoWayTreeStructure(Dictionary<int, Rule> rules)
        {
            foreach (CombinedRule cmbRules in rules.Values.Where(x => x is CombinedRule))
            {
                cmbRules.AddChildren(cmbRules.Line.Split(" ").Select(x =>
                {
                    if (int.TryParse(x, out var childId))
                        return rules[childId];
                    else return null;
                }).Where(x => x != null));
            }
        }

        private static Dictionary<int, Rule> CreateDictionaryOfRulesObjects(string rulesInput)
        {
            var rules = new Dictionary<int, Rule>();
            foreach (var line in rulesInput
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
            {
                var numberRule = line.Split(": ");
                var match = Regex.Match(numberRule[1], "^\"(?<Value>.*)\"$");
                var number = int.Parse(numberRule[0]);
                if (match.Success)
                {
                    rules.Add(number, new Rule
                    {
                        Number = number,
                        Line = numberRule[1],
                        Value = match.Groups["Value"].Value
                    }
                    );
                }
                else
                {
                    var cmb = new CombinedRule
                    {
                        Number = number,
                        Line = numberRule[1],
                    };
                    rules.Add(number, cmb);
                }
            }

            return rules;
        }

        public class Rule
        {
            public int Number { get; set; }
            public string Line { get; set; }
            public string Value { get; set; }

            public HashSet<CombinedRule> Parents { get; } = new HashSet<CombinedRule>();

            public void AddParent(CombinedRule parent)
            {
                Parents.Add(parent);
            }

            public virtual HashSet<string> GetAllowedMessages()
            {
                return new HashSet<string> { Value };
            }

            public override string ToString()
            {
                return Value;
            }
        }

        public class CombinedRule : Rule
        {
            public List<Subrule> Subrules { get; set; }

            public HashSet<Rule> Children { get; } = new HashSet<Rule>();
            public bool AlreadyReplacedSubrulesInParents { get; private set; }

            public bool IsLoop { get; private set; }

            public void AddChildren(IEnumerable<Rule> children)
            {
                foreach (var ch in children)
                {
                    if (ch == this)
                    {
                        IsLoop = true;
                        continue;
                    }
                    Children.Add(ch);
                    ch.AddParent(this);
                }
            }

            public void CreateSubrules(Dictionary<int, Rule> dict)
            {
                Subrules = Line.Split("|", StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => new Subrule()
                    {
                        Rules = x.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                 .Select(x => dict[int.Parse(x)]).ToList()
                    }).ToList();
            }

            public override HashSet<string> GetAllowedMessages()
            {
                return Subrules.Select(x => x.Value).ToHashSet();
            }

            public override string ToString()
            {
                return $"[{Number}] : {Line}";
            }
        }

        public class Subrule
        {
            private bool _ready;
            public Subrule Copy()
            {
                return new Subrule()
                {
                    Rules = new List<Rule>(Rules),
                    Ready = Ready
                };
            }

            public List<Rule> Rules { get; set; } = new List<Rule>();
            public bool Ready
            {
                get => _ready;
                set
                {
                    _ready = value;
                    if (_ready)
                        Value = string.Join("", Rules.Select(x => x.Value));
                }
            }
            public string Value { get; private set; }

        }
    }
}
