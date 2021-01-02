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
    public class Day11Task1Tests
    {
        Day11Task1 _solver = new Day11Task1();
        public string NL = Environment.NewLine;

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
        public void parses_input_correctly()
        {
            var input = $"L.L{NL}#L.{NL}LLL";

            var result = _solver.ParseInput(input);

            result.Length.Should().Be(9);
            result.GetLength(0).Should().Be(3);
            result.GetLength(1).Should().Be(3);

            result[0, 0].Row.Should().Be(0);
            result[0, 0].Column.Should().Be(0);
            result[0, 0].Occupation.Should().Be(Occupation.Empty);

            result[0, 1].Row.Should().Be(0);
            result[0, 1].Column.Should().Be(1);
            result[0, 1].Occupation.Should().Be(Occupation.Floor);

            result[1, 0].Row.Should().Be(1);
            result[1, 0].Column.Should().Be(0);
            result[1, 0].Occupation.Should().Be(Occupation.Occupied);
        }

        [Test]
        public void checks_seats_as_correct()
        {
            var input = $"L.L{NL}#L.{NL}LLL";
            var seats = _solver.ParseInput(input);
            var result = _solver.CheckSeats(seats, input);

            result.Should().BeTrue();
        }

        [Test]
        public void checks_seats_as_incorrect()
        {
            var input = $"L.L{NL}#L.{NL}LLL";
            var seats = _solver.ParseInput(input);
            var result = _solver.CheckSeats(seats, $"L.L{NL}#L.{NL}LL.");

            result.Should().BeFalse();
        }


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
            var expectedState = @"#.LL.L#.##
#LLLLLL.L#
L.L.L..L..
#LLL.LL.L#
#.LL.LL.LL
#.LLLL#.##
..L.L.....
#LLLLLLLL#
#.LLLLLL.L
#.#LLLL.##";

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
            var expectedState = @"#.##.L#.##
#L###LL.L#
L.#.#..#..
#L##.##.L#
#.##.LL.LL
#.###L#.##
..#.#.....
#L######L#
#.LL###L.L
#.#L###.##";

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
            var expectedState = @"#.#L.L#.##
#LLL#LL.L#
L.L.L..#..
#LLL.##.L#
#.LL.LL.LL
#.LL#L#.##
..L.L.....
#L#LLLL#L#
#.LLLLLL.L
#.#L#L#.##";

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
            var expectedState = @"#.#L.L#.##
#LLL#LL.L#
L.#.L..#..
#L##.##.L#
#.#L.LL.LL
#.#L#L#.##
..L.L.....
#L#L##L#L#
#.LLLLLL.L
#.#L#L#.##";

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
        public void occupy_seats_stabilizes_at_sixth_time()
        {
            var expectedState = @"#.#L.L#.##
#LLL#LL.L#
L.#.L..#..
#L##.##.L#
#.#L.LL.LL
#.#L#L#.##
..L.L.....
#L#L##L#L#
#.LLLLLL.L
#.#L#L#.##";

            var seats = _solver.ParseInput(input1);

            var result = _solver.OccupySeats(seats);
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
                for(var c = 0; c < seats.GetLength(1); c++)
                {
                    sb.Append(seats[r, c].ToString());
                }
                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString());
        }

        [Test]
        public void count_37_occupied_seats()
        {
            var occupied = _solver.Solve(input1);

            occupied.Should().Be(37);
        }
        [Test]
        public void count_2424_occupied_seats()
        {
            var occupied = _solver.Solve(File.ReadAllText("Files\\Day11.txt"));

            occupied.Should().Be(2424);
        }
    }
}
