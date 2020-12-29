using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day06Task2 : Day06Task1
    {
        public override int CountYesesInGroup(string group)
        {
            var users = group.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var sets = users.Select(x => x.ToHashSet());
            var result = sets.Aggregate((x, y) => x.Intersect(y).ToHashSet());
            return result.Count();
        }
    }
}
