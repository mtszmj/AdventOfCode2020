using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day23Task2
    {
        public long Solve(string input, int cupsCount, int rounds)
        {
            var cups = CreateCircle2(input, cupsCount, out int current);
            for (var i = 0; i < rounds; i++)
            {
                current = PlayRound(cups, current);
            }

            return (long)cups[1] * cups[cups[1]];
        }

        private int[] CreateCircle2(string input, int cupsNumber, out int first)
        {
            var cups = new int[cupsNumber + 1];
            first = int.Parse($"{input[0]}");
            int next = 0;
            for (var i = 0; i < input.Length - 1; i++)
            {
                var id = int.Parse($"{input[i]}");
                next = int.Parse($"{input[i+1]}");
                cups[id] = next;
            }
            for(var i = input.Length + 1; i <= cupsNumber; i++)
            {
                cups[next] = i;
                next = i;
            }
            cups[next] = first;

            return cups;
        }

        public int PlayRound(int[] cups, int current)
        {
            // current => r1 => r2 => r3 => next
            var removed1 = cups[current];
            var removed2 = cups[removed1];
            var removed3 = cups[removed2];
            cups[current] = cups[removed3];

            var destination = current - 1;
            while (destination == removed1 || destination == removed2
                || destination == removed3 || destination <= 0)
            {
                destination--;
                if (destination <= 0)
                    destination = cups.Length - 1;
            }

            cups[removed3] = cups[destination];
            cups[destination] = removed1;

            return cups[current];
        }
    }
}
