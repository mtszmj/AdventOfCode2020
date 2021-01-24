using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdventOfCode2020.Day24Task1;

namespace AdventOfCode2020.Tests
{
    [TestFixture]
    public class Day25Task1Tests
    {
        Day25Task1 Solver() => new Day25Task1();

        [Test]
        public void solves_input()
        {
            var cardPk = 8987316;
            var doorPk = 14681524;

            var solver = Solver();

            var result = solver.Solve(cardPk, doorPk);

            result.Should().Be(15217943);
        }

        [Test]
        public void solves_example()
        {
            var cardPk = 5764801;
            var doorPk = 17807724;

            var solver = Solver();

            var result = solver.Solve(cardPk, doorPk);

            result.Should().Be(14897079);
        }

        [Test]
        public void finds_card_loop_size()
        {
            var cardPk = 5764801;
            var solver = Solver();

            var result = solver.FindCardLoopSize(cardPk);

            result.Should().Be(8);
        }

        [Test]
        public void finds_door_loop_size()
        {
            var doorPk = 17807724;
            var solver = Solver();

            var result = solver.FindDoorLoopSize(doorPk);

            result.Should().Be(11);
        }
    }
}
