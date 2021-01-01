using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day09Task1
    {
        public long Solve(string numbersInput, int preambleLength)
        {
            long[] numbers = numbersInput.Split(Environment.NewLine).Select(x => long.Parse(x)).ToArray();
            long[] window = numbers.Take(preambleLength).ToArray();
            int eldestWindowIndex = 0;
            int inputIndex = preambleLength;

            for(int i = inputIndex; i < numbers.Length; i++)
            {
                long value = numbers[i];
                if (!DoesSumExist(value, window))
                    return value;

                window[eldestWindowIndex] = value;
                eldestWindowIndex = (eldestWindowIndex + 1) % preambleLength;
            }

            throw new InvalidOperationException("Solution not found");
        }

        public bool DoesSumExist(long value, long[] window)
        {
            var array = window.OrderBy(x => x).ToArray();

            for(int left = 0; left < array.Length - 1; left++)
            {
                if(array[left] > value)
                    continue;
                
                for(int right = array.Length - 1; right > left; right--)
                {
                    long lValue = array[left];
                    long rValue = array[right];
                    if (lValue != rValue && lValue + rValue == value)
                        return true;

                    if (lValue + rValue < value)
                        break;
                }
            }

            return false;
        }
    }
}
