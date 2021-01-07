using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day16Task2 : Day16Task1
    {
        public override long Solve(string input)
        {
            Data data = Parse(input);
            var fields = OrderFields(data);

            var result = 1L;
            var six = 0;
            for(var i = 0; i < data.YourTicket.Numbers.Count && six < 6; i++)
            {
                if(fields[i].StartsWith("departure"))
                {
                    six++;
                    result *= data.YourTicket.Numbers[i];
                }
            }

            return result;
        }
        public Dictionary<int, string> OrderFields(string input)
        {
            Data data = Parse(input);
            return OrderFields(data);
        }

        public Dictionary<int, string> OrderFields(Data data)
        {
            var allValid = data.Rules.SelectMany(x => x.Value.ValidNumbers).ToHashSet();

            var tickets = data.OtherTickets.Where(x => x.Numbers.All(x => allValid.Contains(x)))
                .Concat(new[] { data.YourTicket });

            var assignments = new List<Assignment>();

            foreach(var ticket in tickets)
            {
                if(assignments.Count == 0)
                    for (var i = 0; i < ticket.Numbers.Count; i++)
                        assignments.Add(new Assignment());

                for(var i = 0; i < ticket.Numbers.Count; i++)
                {
                    assignments[i].Numbers.Add(ticket.Numbers[i]);
                }
            }

            foreach(var assignment in assignments)
            {
                foreach (var rule in data.Rules.Where(r => assignment.Numbers.All(a => r.Value.ValidNumbers.Contains(a))))
                    assignment.Rules.Add(rule.Value);
            }

            var retry = true;
            while (retry)
            {
                retry = false;
                var rulesAssigned = assignments.Where(x => x.Rules.Count == 1).Select(x => x.Rules.ElementAt(0)).ToHashSet();
                foreach(var assignment in assignments)
                {
                    if(assignment.Rules.Count > 1)
                    {
                        retry = true;
                        assignment.Rules.RemoveWhere(x => rulesAssigned.Contains(x));
                    }
                }
            }

            var result = new Dictionary<int, string>();
            for (var i = 0; i < assignments.Count; i++)
            {
                result.Add(i, assignments[i].Rules.ElementAt(0).Name);
            }
            return result;
        }

        public class Assignment
        {
            public HashSet<int> Numbers { get; set; } = new HashSet<int>();

            public HashSet<Rule> Rules { get; set; } = new HashSet<Rule>();
        }
    }
}
