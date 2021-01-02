using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day11Task1
    {
        public virtual int NumberOfOccupiedAdjacementSeatsToLeaveSeat() => 4;

        public long Solve(string input)
        {
            var seats = ParseInput(input);
            while (OccupySeats(seats))
                ;
            return CountOccupiedSeats(seats);
        }

        private static long CountOccupiedSeats(Seat[,] seats)
        {
            var occupied = 0L;
            foreach (var seat in seats)
            {
                if (seat.Occupation == Occupation.Occupied)
                    occupied++;
            }

            return occupied;
        }

        public Seat[,] ParseInput(string input)
        {
            var rows = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var numberOfRows = rows.Length;
            var numberOfColumns = rows[0].Length;

            var seats = new Seat[numberOfRows, numberOfColumns];
            for (var r = 0; r < numberOfRows; r++)
            {
                for (var c = 0; c < numberOfColumns; c++)
                {
                    seats[r, c] = new Seat
                    {
                        Row = r,
                        Column = c,
                        Occupation = rows[r][c] switch
                        {
                            'L' => Occupation.Empty,
                            '#' => Occupation.Occupied,
                            '.' => Occupation.Floor,
                            _ => throw new ArgumentException("Invalid character in input")
                        }
                    };
                }
            }

            return seats;
        }

        public bool OccupySeats(Seat[,] seats)
        {
            var occupationHasChanged = false;

            var snapshot = new Seat[seats.GetLength(0), seats.GetLength(1)];
            for (var r = 0; r < seats.GetLength(0); r++)
            {
                for (var c = 0; c < seats.GetLength(1); c++)
                {
                    snapshot[r, c] = seats[r, c].Copy();
                }
            }

            for (var r = 0; r < seats.GetLength(0); r++)
            {
                for (var c = 0; c < seats.GetLength(1); c++)
                {

                    var adjacementSeatsStates = CheckAdjacementSeats(snapshot, r, c);
                    if (snapshot[r, c].IsEmpty
                        && !adjacementSeatsStates.TryGetValue(Occupation.Occupied, out int _)
                        )
                    {
                        seats[r, c].Occupation = Occupation.Occupied;
                        occupationHasChanged = true;
                    }
                    else if (snapshot[r, c].IsOccupied
                        && adjacementSeatsStates.TryGetValue(Occupation.Occupied, out int occupied)
                        && occupied >= NumberOfOccupiedAdjacementSeatsToLeaveSeat()
                        )
                    {
                        seats[r, c].Occupation = Occupation.Empty;
                        occupationHasChanged = true;
                    }
                }
            }

            return occupationHasChanged;
        }

        public virtual Dictionary<Occupation, int> CheckAdjacementSeats(Seat[,] seats, int row, int column)
        {
            var counter = new Dictionary<Occupation, int>();
            for (var r = row - 1; r <= row + 1; r++)
                for (var c = column - 1; c <= column + 1; c++)
                {
                    if (r < 0 || r >= seats.GetLength(0)
                        || c < 0 || c >= seats.GetLength(1)
                        || (r == row && c == column))
                        continue;

                    var occupation = seats[r, c].Occupation;
                    if (counter.ContainsKey(occupation))
                        counter[occupation]++;
                    else counter[occupation] = 1;
                }

            return counter;
        }

        public bool CheckSeats(Seat[,] seats, string expected)
        {
            var rows = expected.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            for (var r = 0; r < seats.GetLength(0); r++)
            {
                for (var c = 0; c < seats.GetLength(1); c++)
                {
                    switch (seats[r, c].Occupation)
                    {
                        case Occupation.Floor:
                            if (rows[r][c] != '.') return false;
                            break;
                        case Occupation.Empty:
                            if (rows[r][c] != 'L') return false;
                            break;
                        case Occupation.Occupied:
                            if (rows[r][c] != '#') return false;
                            break;
                    }
                }
            }

            return true;
        }

        public class Seat
        {
            public int Row { get; set; }
            public int Column { get; set; }
            public Occupation Occupation { get; set; }

            public bool IsEmpty => Occupation == Occupation.Empty;
            public bool IsFloor => Occupation == Occupation.Floor;
            public bool IsOccupied => Occupation == Occupation.Occupied;

            public override string ToString()
            {
                return Occupation switch
                {
                    Occupation.Floor => ".",
                    Occupation.Empty => "L",
                    Occupation.Occupied => "#",
                };
            }

            public Seat Copy()
            {
                return new Seat
                {
                    Row = Row,
                    Column = Column,
                    Occupation = Occupation
                };
            }
        }

        public enum Occupation
        {
            Floor,
            Empty,
            Occupied
        }
    }
}
