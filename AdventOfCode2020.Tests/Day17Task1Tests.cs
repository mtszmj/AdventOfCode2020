using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdventOfCode2020.Day17Task1;

namespace AdventOfCode2020.Tests
{
    [TestFixture]
    public class Day17Task1Tests
    {
        Day17Task1 Solver() => new Day17Task1();

        [Test]
        public void solves_input()
        {
            var input = @"...#...#
#######.
....###.
.#..#...
#.#.....
.##.....
#.####..
#....##.";

            var solver = Solver();

            var result = solver.Solve(input, 6);
            result.Should().Be(293);
        }

        [Test]
        public void solves_example()
        {
            var input = @".#.
..#
###";

            var solver = Solver();
            var result = solver.Solve(input, 6);

            result.Should().Be(112);
        }

        [Test]
        public void solves_example_one_cycle()
        {
            var input = @".#.
..#
###";
            var solver = Solver();
            var result = solver.Solve(input, 1);

            result.Should().Be(11);

            //solver[0, 0, -1].Should().Be(State.Active);
            //solver[1, 0, -1].Should().Be(State.Inactive);
            //solver[2, 0, -1].Should().Be(State.Inactive);

            //solver[0, 1, -1].Should().Be(State.Inactive);
            //solver[1, 1, -1].Should().Be(State.Inactive);
            //solver[2, 1, -1].Should().Be(State.Active);

            //solver[0, 2, -1].Should().Be(State.Inactive);
            //solver[1, 2, -1].Should().Be(State.Active);
            //solver[2, 2, -1].Should().Be(State.Inactive);


            //solver[0, 0, 0].Should().Be(State.Active);
            //solver[1, 0, 0].Should().Be(State.Inactive);
            //solver[2, 0, 0].Should().Be(State.Active);
                         
            //solver[0, 1, 0].Should().Be(State.Inactive);
            //solver[1, 1, 0].Should().Be(State.Active);
            //solver[2, 1, 0].Should().Be(State.Active);
                         
            //solver[0, 2, 0].Should().Be(State.Inactive);
            //solver[1, 2, 0].Should().Be(State.Active);
            //solver[2, 2, 0].Should().Be(State.Inactive);


            //solver[0, 0, 1].Should().Be(State.Active);
            //solver[1, 0, 1].Should().Be(State.Inactive);
            //solver[2, 0, 1].Should().Be(State.Inactive);
                         
            //solver[0, 1, 1].Should().Be(State.Inactive);
            //solver[1, 1, 1].Should().Be(State.Inactive);
            //solver[2, 1, 1].Should().Be(State.Active);
                         
            //solver[0, 2, 1].Should().Be(State.Inactive);
            //solver[1, 2, 1].Should().Be(State.Active);
            //solver[2, 2, 1].Should().Be(State.Inactive);

        }

        [Test]
        public void initializes_cubes()
        {
            var input = @".#.
..#
###";
            var solver = Solver();
            solver.Initialize(input);

            solver[0, 0, 0].Should().Be(State.Inactive);
            solver[1, 0, 0].Should().Be(State.Active);
            solver[2, 0, 0].Should().Be(State.Inactive);

            solver[0, 1, 0].Should().Be(State.Inactive);
            solver[1, 1, 0].Should().Be(State.Inactive);
            solver[2, 1, 0].Should().Be(State.Active);

            solver[0, 2, 0].Should().Be(State.Active);
            solver[1, 2, 0].Should().Be(State.Active);
            solver[2, 2, 0].Should().Be(State.Active);
        }

        [Test]
        public void non_set_cube_returns_inactive()
        {
            var solver = Solver();
            solver[0, 0, 0].Should().Be(Day17Task1.State.Inactive);
        }

        [Test]
        public void sets_non_existing_cube_to_active()
        {
            var solver = Solver();
            solver[0, 0, 0].Should().Be(State.Inactive);
            solver[0, 0, 0] = State.Active;
            solver[0, 0, 0].Should().Be(State.Active);
        }

        [Test]
        public void sets_active_to_inactive()
        {
            var solver = Solver();
            solver[0, 0, 0].Should().Be(State.Inactive);
            solver[0, 0, 0] = State.Active;
            solver[0, 0, 0].Should().Be(State.Active);
            solver[0, 0, 0] = State.Inactive;
            solver[0, 0, 0].Should().Be(State.Inactive);
        }

        [Test]
        public void count_neightbours_on_uninitialized_should_return_26_inactive()
        {
            var solver = Solver();
            var result = solver.CountNeightbours(0, 0, 0);

            result[State.Inactive].Should().Be(26);
            result[State.Active].Should().Be(0);
        }
    }
}
