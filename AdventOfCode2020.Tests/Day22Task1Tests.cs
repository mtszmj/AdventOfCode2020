using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Tests
{
    [TestFixture]
    public class Day22Task1Tests
    {
        Day22Task1 Solver() => new Day22Task1();
        string Example() => @"Player 1:
9
2
6
3
1

Player 2:
5
8
4
7
10";
        [Test]
        public void solves_example()
        {
            var input = Example();
            var solver = Solver();
            
            var result = solver.Solve(input);

            result.Should().Be(306);
        }

        [Test]
        public void parses_example()
        {
            var input = Example();
            var solver = Solver();

            var (p1, p2) = solver.Parse(input);

            p1.Should().ContainInOrder(new[] { 9, 2, 6, 3, 1 });
            p2.Should().ContainInOrder(new[] { 5, 8, 4, 7, 10 });
        }


        [Test]
        public void player1_wins_first_round()
        {
            var input = Example();
            var solver = Solver();

            var (p1, p2) = solver.Parse(input);

            int round = 0;
            var winners = new List<int>();
            solver.PlayOneRound(p1, p2, ref round, winners);

            p1.Should().ContainInOrder(new[] { 2, 6, 3, 1, 9, 5 });
            p2.Should().ContainInOrder(new[] { 8, 4, 7, 10 });
            round.Should().Be(1);
            winners.Should().ContainInOrder(new[] { 1 });
        }

        [Test]
        public void player2_wins_second_round()
        {
            var input = Example();
            var solver = Solver();

            var (p1, p2) = solver.Parse(input);

            int round = 0;
            var winners = new List<int>();
            solver.PlayOneRound(p1, p2, ref round, winners);
            solver.PlayOneRound(p1, p2, ref round, winners);

            p1.Should().ContainInOrder(new[] { 6, 3, 1, 9, 5 });
            p2.Should().ContainInOrder(new[] { 4, 7, 10, 8, 2 });
            round.Should().Be(2);
            winners.Should().ContainInOrder(new[] { 1, 2 });
        }

        [Test]
        public void player2_wins()
        {
            var input = Example();
            var solver = Solver();

            var (p1, p2) = solver.Parse(input);

            solver.Play(p1, p2);

            p1.Should().BeEmpty();
            p2.Should().ContainInOrder(new[] { 3, 2, 10, 6, 8, 5, 9, 4, 7, 1 });
        }

        [Test]
        public void calculates_result()
        {
            var player = new[] { 3, 2, 10, 6, 8, 5, 9, 4, 7, 1 };
            var solver = Solver();

            var result = solver.CalculateResult(player);

            result.Should().Be(306);
        }

        [Test]
        public void solves_input()
        {
            var input = File.ReadAllText("Files\\Day22.txt");
            var solver = Solver();

            var result = solver.Solve(input);

            result.Should().Be(31269);
        }
    }
}
