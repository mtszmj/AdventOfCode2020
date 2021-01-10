using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day19Task1
    {
        public static List<Rule> listOfReady = new List<Rule>();

        public int Solve(string input)
        {
            var rulesInput = input.Split($"{Environment.NewLine}{Environment.NewLine}");

            var rules = ParseRules(rulesInput[0]);

            var messages = rules[0].GetAllowedMessages();
            var allParsed = (rules[0] as CombinedRule).Subrules.All(x => x.Rules.All(y => y.GetType() == typeof(Rule)));
            var inputMessages = rulesInput[1]
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var isInputOk = inputMessages.Where(x => {
                var hashset = x.ToHashSet();
                if (hashset.Count != 2 || !hashset.Contains('a') || !hashset.Contains('b'))
                    return true;

                return false;
            }).ToList();

            return inputMessages.Count(x => messages.Contains(x));
        }

        public Dictionary<int, Rule> ParseRules(string rulesInput)
        {
            Dictionary<int, Rule> rules = CreateDictionaryOfRulesObjects(rulesInput);
            CreateTwoWayTreeStructure(rules);
            CreateSubrulesInCombinedRules(rules);

            foreach (var rule in rules.Values.Where(x => x.GetType() == typeof(Rule)))
            {
                Console.WriteLine($"[{rule.Number}] (Rule) ParseRules");
                foreach (var cmbRule in rule.Parents)
                {
                    cmbRule.ReplaceSubrulesInParents();
                }
            }


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

            public void AddChildren(IEnumerable<Rule> children)
            {
                foreach (var ch in children)
                {
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

            public void ReplaceSubrulesInParents()
            {
                Console.WriteLine($"[{Number}] Enter ReplaceSubrulesInParents");
                for (int i = 0; i < Subrules.Count; i++)
                {
                    Subrule sub = Subrules[i];

                    if (sub.Rules.All(x => x.GetType() == typeof(Rule)))
                        sub.Ready = true;
                }

                if (!AlreadyReplacedSubrulesInParents && Subrules.All(x => x.Ready))
                {
                    this.AlreadyReplacedSubrulesInParents = true;
                    Console.WriteLine($"{Number} ready");
                    Console.WriteLine($"[{Number}]: " + string.Join(" | ", Subrules.Select(x => x.Value)));
                    listOfReady.Add(this);
                    listOfReady.Sort((a,b) => a.Number.CompareTo(b.Number));
                    foreach (var parent in Parents)
                    {
                        var subrulesCount = parent.Subrules.Count;
                        for (var p = 0; p < subrulesCount; p++)
                        {
                            if (Subrules.Count == 1)
                            {
                                var end = parent.Subrules[p].Rules.Count;
                                for (var i = 0; i < end; i++)
                                {
                                    if (parent.Subrules[p].Rules[i] == this)
                                    {
                                        var removeIndex = i;
                                        for (var j = 0; j < Subrules[0].Rules.Count; j++, i++, end++)
                                        {
                                            parent.Subrules[p].Rules.Insert(i+1, Subrules[0].Rules[j]);
                                        }
                                        parent.Subrules[p].Rules.RemoveAt(removeIndex);
                                        i--;
                                        end--;
                                    }
                                }
                            }
                            else if (Subrules.Count > 1)
                            {
                                var remove = false;
                                foreach (var sub in Subrules)
                                {
                                    var insert = false;
                                    var copy = parent.Subrules[p].Copy();
                                    var end = copy.Rules.Count;
                                    for (var i = 0; i < end; i++)
                                    {
                                        if (copy.Rules[i] == this)
                                        {
                                            insert = true;
                                            remove = true;
                                            var removeIndex = i;
                                            for (var j = 0; j < sub.Rules.Count; j++, i++, end++)
                                            {
                                                copy.Rules.Insert(i + 1, sub.Rules[j]);
                                            }
                                            copy.Rules.RemoveAt(removeIndex);
                                            i--;
                                            end--;
                                        }
                                    }
                                    if (insert)
                                        parent.Subrules.Add(copy);
                                }
                                if (remove)
                                {
                                    parent.Subrules.RemoveAt(p);
                                    p--;
                                    subrulesCount--;
                                }
                            }

                        }

                        parent.ReplaceSubrulesInParents();
                    }
                }
            }

            public override HashSet<string> GetAllowedMessages()
            {
                return Subrules.Select(x => x.Value).ToHashSet();
            }

            public override string ToString()
            {
                return $"[{Number}] : {Line}";
                return base.ToString();

                var sb = new StringBuilder();
                foreach (var subrule in Subrules)
                {
                    sb.Append("(");
                    foreach (var rule in subrule.Rules)
                        sb.Append(rule.ToString());
                    sb.Append(")");

                }

                return $"> {string.Join(" | ", Subrules)} <";
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

            //public void ReplaceRule(Rule rule)
            //{
            //    if (rule.GetType() != typeof(Rule) && Rules.Contains(rule))
            //    {
            //        var cmb = rule as CombinedRule;
            //        if (cmb is null)
            //            return;

            //        if (cmb.Subrules.Count == 1)
            //        {
            //            var end = Rules.Count;
            //            for (var i = 0; i < end; i++) 
            //            {
            //                if(Rules[i] == rule)
            //                {
            //                    for(var j = 0; j < cmb.Subrules[0].Rules.Count; j++, i++, end++)
            //                    {
            //                        Rules[i] = cmb.Subrules[0].Rules[j];
            //                    }
            //                }
            //            }
            //        }
            //        else if(cmb.Subrules.Count > 1)
            //        {

            //        }
            //    }

            //    if (Rules.All(x => x.GetType() == typeof(Rule)))
            //        Ready = true;

            //}

            public override string ToString()
            {
                return base.ToString();
                return $"{string.Join(" ", Rules)} ";
            }
        }


    }
}
