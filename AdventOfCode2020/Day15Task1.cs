using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day15Task1
    {
        public int Solve(int[] input, int turn)
        {
            var data = new Dictionary<int, (int prev_2, int prev_1)>();

            var last = 0;
            for(var i = 1; i <= input.Length && i <= turn; i++)
            {
                last = input[i-1];
                data[last] = (0, i);
            }

            var next = 0;
            for(var i = input.Length + 1; i <= turn; i++)
            {
                var wasUsed = data.TryGetValue(last, out var prevs);
                if (wasUsed && prevs.prev_2 == 0 || !wasUsed)
                    next = 0;
                else
                    next = prevs.prev_1 - prevs.prev_2;

                if(!data.TryGetValue(next, out var nextPrevs))
                    nextPrevs = (0, 0);
                data[next] = (nextPrevs.prev_1, i);

                last = next;    
            }

            return last;
        }
    }
}
