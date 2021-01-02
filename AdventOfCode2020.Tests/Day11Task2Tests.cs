using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdventOfCode2020.Day11Task1;

namespace AdventOfCode2020.Tests
{
    [TestFixture]
    public class Day11Task2Tests
    {
        Day11Task2 _solver = new Day11Task2();
        
        string input1 => @"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL";

        [Test]
        public void occupy_seats_once()
        {
            var expectedState = @"#.##.##.##
#######.##
#.#.#..#..
####.##.##
#.##.##.##
#.#####.##
..#.#.....
##########
#.######.#
#.#####.##";

            var seats = _solver.ParseInput(input1);

            PrintSeats(seats);
            var result = _solver.OccupySeats(seats);
            PrintSeats(seats);

            var isInExpectedState = _solver.CheckSeats(seats, expectedState);

            result.Should().BeTrue();
            isInExpectedState.Should().BeTrue();
        }

        [Test]
        public void occupy_seats_twice()
        {
            var expectedState = @"#.LL.LL.L#
#LLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLL#
#.LLLLLL.L
#.LLLLL.L#";

            var seats = _solver.ParseInput(input1);

            var result = _solver.OccupySeats(seats);
            result = _solver.OccupySeats(seats);
            PrintSeats(seats);

            var isInExpectedState = _solver.CheckSeats(seats, expectedState);

            result.Should().BeTrue();
            isInExpectedState.Should().BeTrue();
        }

        [Test]
        public void occupy_seats_three_times()
        {
            var expectedState = @"#.L#.##.L#
#L#####.LL
L.#.#..#..
##L#.##.##
#.##.#L.##
#.#####.#L
..#.#.....
LLL####LL#
#.L#####.L
#.L####.L#";

            var seats = _solver.ParseInput(input1);

            var result = _solver.OccupySeats(seats);
            result = _solver.OccupySeats(seats);
            result = _solver.OccupySeats(seats);
            PrintSeats(seats);

            var isInExpectedState = _solver.CheckSeats(seats, expectedState);

            result.Should().BeTrue();
            isInExpectedState.Should().BeTrue();
        }

        [Test]
        public void occupy_seats_four_times()
        {
            var expectedState = @"#.L#.L#.L#
#LLLLLL.LL
L.L.L..#..
##LL.LL.L#
L.LL.LL.L#
#.LLLLL.LL
..L.L.....
LLLLLLLLL#
#.LLLLL#.L
#.L#LL#.L#";

            var seats = _solver.ParseInput(input1);

            var result = _solver.OccupySeats(seats);
            result = _solver.OccupySeats(seats);
            result = _solver.OccupySeats(seats);
            result = _solver.OccupySeats(seats);
            PrintSeats(seats);

            var isInExpectedState = _solver.CheckSeats(seats, expectedState);

            result.Should().BeTrue();
            isInExpectedState.Should().BeTrue();
        }
        [Test]
        public void occupy_seats_five_times()
        {
            var expectedState = @"#.L#.L#.L#
#LLLLLL.LL
L.L.L..#..
##L#.#L.L#
L.L#.#L.L#
#.L####.LL
..#.#.....
LLL###LLL#
#.LLLLL#.L
#.L#LL#.L#";

            var seats = _solver.ParseInput(input1);

            var result = _solver.OccupySeats(seats);
            result = _solver.OccupySeats(seats);
            result = _solver.OccupySeats(seats);
            result = _solver.OccupySeats(seats);
            result = _solver.OccupySeats(seats);
            PrintSeats(seats);

            var isInExpectedState = _solver.CheckSeats(seats, expectedState);

            result.Should().BeTrue();
            isInExpectedState.Should().BeTrue();
        }

        [Test]
        public void occupy_seats_six_times()
        {
            var expectedState = @"#.L#.L#.L#
#LLLLLL.LL
L.L.L..#..
##L#.#L.L#
L.L#.LL.L#
#.LLLL#.LL
..#.L.....
LLL###LLL#
#.LLLLL#.L
#.L#LL#.L#";

            var seats = _solver.ParseInput(input1);

            var result = _solver.OccupySeats(seats);
            result = _solver.OccupySeats(seats);
            result = _solver.OccupySeats(seats);
            result = _solver.OccupySeats(seats);
            result = _solver.OccupySeats(seats);
            result = _solver.OccupySeats(seats);
            PrintSeats(seats);

            var isInExpectedState = _solver.CheckSeats(seats, expectedState);

            result.Should().BeTrue();
            isInExpectedState.Should().BeTrue();
        }
        [Test]
        public void occupy_seats_stabilizes_at_seventh_time()
        {
            var expectedState = @"#.L#.L#.L#
#LLLLLL.LL
L.L.L..#..
##L#.#L.L#
L.L#.LL.L#
#.LLLL#.LL
..#.L.....
LLL###LLL#
#.LLLLL#.L
#.L#LL#.L#";

            var seats = _solver.ParseInput(input1);

            var result = _solver.OccupySeats(seats);
            result = _solver.OccupySeats(seats);
            result = _solver.OccupySeats(seats);
            result = _solver.OccupySeats(seats);
            result = _solver.OccupySeats(seats);
            result = _solver.OccupySeats(seats);
            result = _solver.OccupySeats(seats);
            PrintSeats(seats);

            var isInExpectedState = _solver.CheckSeats(seats, expectedState);

            result.Should().BeFalse();
            isInExpectedState.Should().BeTrue();
        }

        private void PrintSeats(Seat[,] seats)
        {
            var sb = new StringBuilder();
            for (var r = 0; r < seats.GetLength(0); r++)
            {
                for (var c = 0; c < seats.GetLength(1); c++)
                {
                    sb.Append(seats[r, c].ToString());
                }
                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString());
        }


        [Test]
        public void count_26_occupied_seats()
        {
            var occupied = _solver.Solve(input1);

            occupied.Should().Be(26);
        }

        [Test]
        public void count_2208_occupied_seats()
        {
            var occupied = _solver.Solve(File.ReadAllText("Files\\Day11.txt"));

            occupied.Should().Be(2208);
        }
    }
}
