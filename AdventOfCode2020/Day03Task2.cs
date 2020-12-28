using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day03Task2
    {
        private Day03Task1 Task1 { get; } = new Day03Task1();

        public long CompareRoutes(string input, List<(int right, int down)> routeMoves)
        {
            var results = routeMoves.Select(x => Task1.Count(input, x.right, x.down));
            return results.Aggregate(1L, (acc, x) => acc * x);
        }
    }
}
