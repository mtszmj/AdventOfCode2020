using System;

namespace AdventOfCode2020
{
    public class Day01Task1
    {
        public long MultiplyTwoEntriesThatSumTo2020(int[] input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if(input.Length < 2)
            {
                throw new ArgumentException("Less than two arguments");
            }

            for(var i = 0; i < input.Length; i++)
            {
                var first = input[i];
                for(var j = i+1; j < input.Length; j++)
                {
                    var second = input[j];

                    if (first + second == 2020)
                        return first * second;
                }
            }

            throw new ArgumentException("No correct result found");
        }

    }
}
