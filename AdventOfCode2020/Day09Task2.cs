using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day09Task2
    {
        Day09Task1 task1 = new Day09Task1();

        public long Solve(string numbersInput, int preambleLength)
        {
            var nonExistingSumOfTwoElements = task1.Solve(numbersInput, preambleLength);

            long[] numbers = numbersInput.Split(Environment.NewLine).Select(x => long.Parse(x)).ToArray();
            var (left, right) = FindSetWithSumOfGivenValue(nonExistingSumOfTwoElements, numbers);
            var sum = SumMinAndMax(left, right, numbers);
            return sum;
        }

        public (int left, int right) FindSetWithSumOfGivenValue(long nonExistingSumOfTwoElements, long[] numbers)
        {
            var setLeftIndex = 0;
            var setRightIndex = 1;
            var sum = numbers[setLeftIndex] + numbers[setRightIndex];
            while (true)
            {
                if (sum == nonExistingSumOfTwoElements)
                    return (setLeftIndex, setRightIndex);

                if (setRightIndex == setLeftIndex + 1)
                    sum += numbers[++setRightIndex];
                else if (sum < nonExistingSumOfTwoElements)
                    sum += numbers[++setRightIndex];
                else sum -= numbers[setLeftIndex++];
            }

            throw new InvalidOperationException("Solution not found");
        }

        public long SumMinAndMax(int leftIndex, int rightIndex, long[] numbers)
        {
            var min = numbers.Skip(leftIndex).Take(rightIndex - leftIndex + 1).Min();
            var max = numbers.Skip(leftIndex).Take(rightIndex - leftIndex + 1).Max();
            return min + max;
        }
    }
}
