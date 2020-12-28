using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day01Task2
    {
        public long MultiplyThreeEntriesThatSumTo2020(int[] input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input.Length < 3)
            {
                throw new ArgumentException("Less than two arguments");
            }

            for (var i = 0; i < input.Length; i++)
            {
                var first = input[i];
                for (var j = i + 1; j < input.Length; j++)
                {
                    var second = input[j];
                    var sumOfTwo = first + second;
                    if (sumOfTwo > 2020)
                        continue;
                    
                    for(var k = j + 1; k < input.Length; k++)
                    {
                        var third = input[k];
                        if (sumOfTwo + third == 2020)
                            return first * second * third;
                    }
                }
            }

            throw new ArgumentException("No correct result found");
        }
    }
}
