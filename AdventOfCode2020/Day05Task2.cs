using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day05Task2
    {
        Day05Task1 solver = new Day05Task1();

        public int FindSeatIdWithMinusOneAndPlusOnePresent(string[] input)
        {
            var seatIds = input.Select(x => solver.FindSeatId(x)).OrderBy(x => x).ToList();

            var previous_minus1 = seatIds[1];
            foreach (var id in seatIds.OrderBy(x => x))
            {
                if (previous_minus1 + 2 == id)
                    return previous_minus1 + 1;
                else
                {
                    previous_minus1 = id;
                }
            }

            return -1;
        }
    }
}
