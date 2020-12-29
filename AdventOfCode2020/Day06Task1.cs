using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day06Task1
    {
        private static string GroupSeparator = $"{Environment.NewLine}{Environment.NewLine}";

        public int Solve(string input)
        {
            var groups = SplitToGroups(input);
            return groups.Sum(x => CountYesesInGroup(x));
        }

        public string[] SplitToGroups(string input)
        {
            return input.Split(GroupSeparator, StringSplitOptions.RemoveEmptyEntries);
        }

        public virtual int CountYesesInGroup(string group)
        {
            var yeses = group.Where(x => char.IsLetter(x)).ToHashSet();
            return yeses.Count();
        }
    }
}
