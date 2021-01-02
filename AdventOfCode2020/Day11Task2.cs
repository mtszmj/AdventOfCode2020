using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day11Task2 : Day11Task1
    {

        public override Dictionary<Occupation, int> CheckAdjacementSeats(Seat[,] seats, int row, int column)
        {
            var counter = new Dictionary<Occupation, int>();

            counter[Occupation.Floor] = 0;
            counter[Occupation.Empty] = 0;
            counter[Occupation.Occupied] = 0;

            counter[CheckAdjacementOnOneDirection(seats, row, column, rowColumn => (--rowColumn.r, rowColumn.c))]++; // left
            counter[CheckAdjacementOnOneDirection(seats, row, column, rowColumn => (++rowColumn.r, rowColumn.c))]++; // right
            counter[CheckAdjacementOnOneDirection(seats, row, column, rowColumn => (rowColumn.r, --rowColumn.c))]++; // up
            counter[CheckAdjacementOnOneDirection(seats, row, column, rowColumn => (rowColumn.r, ++rowColumn.c))]++; // down
            counter[CheckAdjacementOnOneDirection(seats, row, column, rowColumn => (--rowColumn.r, --rowColumn.c))]++; // left-up
            counter[CheckAdjacementOnOneDirection(seats, row, column, rowColumn => (--rowColumn.r, ++rowColumn.c))]++; // left-down
            counter[CheckAdjacementOnOneDirection(seats, row, column, rowColumn => (++rowColumn.r, --rowColumn.c))]++; // right-up
            counter[CheckAdjacementOnOneDirection(seats, row, column, rowColumn => (++rowColumn.r, ++rowColumn.c))]++; // right-down

            return counter;
        }

        public Occupation CheckAdjacementOnOneDirection(Seat[,] seats, int row, int column, 
            Func<(int r, int c), (int r, int c)> move)
        {
            var occupation = Occupation.Floor;
            var (r,c) = move((row, column));
            while (r >= 0 && c >= 0 && r < seats.GetLength(0) && c < seats.GetLength(1))
            {
                if (seats[r, c].IsEmpty)
                    return Occupation.Empty;
                if (seats[r, c].IsOccupied)
                    return Occupation.Occupied;
                (r,c) = move((r, c));
            }

            return occupation;
        }

        public override int NumberOfOccupiedAdjacementSeatsToLeaveSeat() => 5;
    }
}
