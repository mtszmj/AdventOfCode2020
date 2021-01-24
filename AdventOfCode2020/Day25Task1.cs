using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day25Task1
    {

        public long Solve(long cardPk, long doorPk)
        {
            var cardLoopSize = FindCardLoopSize(cardPk);
            var doorLoopSize = FindDoorLoopSize(doorPk);

            var result = 1L;
            for (var i = 0; i < doorLoopSize; i++)
                result = Transform(result, cardPk);

            return result;
        }

        public long FindCardLoopSize(long cardPk)
        {
            var cardLoopSize = 0L;
            var cardResult = 1L;
            while (cardResult != cardPk)
            {
                cardResult = Transform(cardResult, 7);
                cardLoopSize++;
            }

            return cardLoopSize;
        }

        public long FindDoorLoopSize(long doorPk)
        {
            var cardLoopSize = 0L;
            var cardResult = 1L;
            while (cardResult != doorPk)
            {
                cardResult = Transform(cardResult, 7);
                cardLoopSize++;
            }

            return cardLoopSize;
        }

        public long Transform(long current, long subject)
        {
            return (current * subject) % 20201227;
        }
    }
}
