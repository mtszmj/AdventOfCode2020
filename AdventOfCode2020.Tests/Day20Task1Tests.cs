using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdventOfCode2020.Day20Task1;

namespace AdventOfCode2020.Tests
{
    [TestFixture]
    public class Day20Task1Tests
    {
        Day20Task1 Solver() => new Day20Task1();

        [Test]
        public void solves_example()
        {
            var input = File.ReadAllText("Files\\Day20_1_ex.txt");
            var solver = Solver();

            var result = solver.Solve(input);
            result.Should().Be(20899048083289);
        }

        [Test]
        public void solves_input()
        {
            var input = File.ReadAllText("Files\\Day20.txt");
            var solver = Solver();

            var result = solver.Solve(input);
            result.Should().Be(21599955909991);
        }

        [Test]
        public void parses_example()
        {
            var input = File.ReadAllText("Files\\Day20_1_ex.txt");
            var solver = Solver();

            var tiles = solver.Parse(input);

            tiles.Count.Should().Be(9);

            tiles.Select(x => x.Id).ToHashSet()
                .Should().Contain(new int[] { 2311, 1951, 1171, 1427, 1489, 2473, 2971, 2729, 3079 });

            var (tileByBorder, singleBorderCount) = solver.TileByBorder(tiles);

            Console.WriteLine($"Rogi: {string.Join(",", singleBorderCount.Where(x => x.Value == 4).Select(x => x.Key.Id))}");
            Console.WriteLine($"Boki: {string.Join(",", singleBorderCount.Where(x => x.Value == 2).Select(x => x.Key.Id))}");

            Console.WriteLine(string.Join(Environment.NewLine, 
                tileByBorder.GroupBy(x => x.Value.Count).Select(x => $"{x.Key}: {x.Count()}")));
        }

        [Test]
        public void check_rotations_and_flips_of_tile()
        {
            var tileData =
@"abcdefghij
!xxxxxxxx1
@xxxxxxxx2
#xxxxxxxx3
$xxxxxxxx4
%xxxxxxxx5
^xxxxxxxx6
&xxxxxxxx7
*xxxxxxxx8
klmnopqrst";

            var tile = new Tile(1, tileData);

            var top = "abcdefghij";
            var topReversed = string.Join("", top.Reverse());

            var right = "j12345678t";
            var rightReversed = string.Join("", top.Reverse());

            var bottom = "klmnopqrst";
            var bottomReversed = string.Join("", top.Reverse());

            var left = "a!@#$%^&*k";
            var leftReversed = string.Join("", top.Reverse());

            Console.WriteLine(string.Join<Orientation>(Environment.NewLine, tile.Orientations));

            //// normal
            //tile.Orientations[0].Top.Should().Be(top);
            //tile.Orientations[0].Right.Should().Be(right);
            //tile.Orientations[0].Bottom.Should().Be(bottom);
            //tile.Orientations[0].Left.Should().Be(left);

            //// 90CCW
            //tile.Orientations[1].Top.Should().Be(right);
            //tile.Orientations[1].Right.Should().Be(bottom);
            //tile.Orientations[1].Bottom.Should().Be(left);
            //tile.Orientations[1].Left.Should().Be(top);
            
            //// 180CCW
            //tile.Orientations[2].Top.Should().Be(bottom);
            //tile.Orientations[2].Right.Should().Be(left);
            //tile.Orientations[2].Bottom.Should().Be(top);
            //tile.Orientations[2].Left.Should().Be(right);

            //// 270CCW
            //tile.Orientations[3].Top.Should().Be(left);
            //tile.Orientations[3].Right.Should().Be(top);
            //tile.Orientations[3].Bottom.Should().Be(right);
            //tile.Orientations[3].Left.Should().Be(bottom);

            ////// flip h
            ////tile.Orientations[4].Top.Should().Be(bottom);
            ////tile.Orientations[4].Right.Should().Be(rightReversed);
            ////tile.Orientations[4].Bottom.Should().Be(top);
            ////tile.Orientations[4].Left.Should().Be(leftReversed);

        }
    }
}
