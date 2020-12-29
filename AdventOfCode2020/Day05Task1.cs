using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day05Task1
    {
        public int NumberOfRows { get; set; } = 128;
        public int NumberOfColumns { get; set; } = 8;
        public int NumberOfRowsLetters = 7;
        public int NumberOfColumnsLetters = 3;

        public int FindHighestSeatId(string[] input)
        {
            return input.Max(x => FindSeatId(x));
        }

        public int FindSeatId(string input)
        {
            var row = FindRow(input.Substring(0, NumberOfRowsLetters));
            var column = FindColumn(input.Substring(NumberOfRowsLetters));

            return row * 8 + column;
        }

        public int FindRow(string input)
        {
            var low = 0;
            var high = NumberOfRows - 1;
            for(int i = 0; i < NumberOfRowsLetters; i++)
            {
                var next = (high - low) / 2 + (high - low) % 2 + low;
                if (input[i] == 'B')
                    low = next;
                else
                    high = next;
            }
            return low;
        }

        public int FindColumn(string input)
        {
            var low = 0;
            var high = NumberOfColumns - 1;
            for (int i = 0; i < NumberOfColumnsLetters; i++)
            {
                var next = (high - low) / 2 + (high - low) % 2 + low;
                if (input[i] == 'R')
                    low = next;
                else
                    high = next;
            }
            return low;
        }
    }
}
