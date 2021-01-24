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
    public class Day24Task1Tests
    {
        Day24Task1 Solver() => new Day24Task1();

        [Test]
        public void solves_input()
        {
            var input = File.ReadAllText("Files\\Day24.txt");

            var solver = Solver();

            var result = solver.Solve(input);

            result.Should().Be(320);
        }

        [Test]
        public void solves_example()
        {
            var input = @"sesenwnenenewseeswwswswwnenewsewsw
neeenesenwnwwswnenewnwwsewnenwseswesw
seswneswswsenwwnwse
nwnwneseeswswnenewneswwnewseswneseene
swweswneswnenwsewnwneneseenw
eesenwseswswnenwswnwnwsewwnwsene
sewnenenenesenwsewnenwwwse
wenwwweseeeweswwwnwwe
wsweesenenewnwwnwsenewsenwwsesesenwne
neeswseenwwswnwswswnw
nenwswwsewswnenenewsenwsenwnesesenew
enewnwewneswsewnwswenweswnenwsenwsw
sweneswneswneneenwnewenewwneswswnese
swwesenesewenwneswnwwneseswwne
enesenwswwswneneswsenwnewswseenwsese
wnwnesenesenenwwnenwsewesewsesesew
nenewswnwewswnenesenwnesewesw
eneswnwswnwsenenwnwnwwseeswneewsenese
neswnwewnwnwseenwseesewsenwsweewe
wseweeenwnesenwwwswnew";

            var solver = Solver();

            var result = solver.Solve(input);

            result.Should().Be(10);
        }

        [Test]
        public void finds_coordinate()
        {
            var input = "sesenwnenenewseeswwswswwnenewsewsw";
            var solver = Solver();

            //se   se    nw  ne   ne   ne   w   se   e   sw   w   sw   sw    w    ne   ne   w   se    w     sw
            //-1,1 -2,2 -1,1 0,2  1,3  2,4  2,2 1,3 1,5  0,4  0,2 -1,1 -2,0 -2-2  -1,-1 0,0 0-2 -1,-1 -1,-3 -2,-4
            var result = solver.FindCoordinates(input);

            result.X.Should().Be(-4);
            result.Y.Should().Be(-2);
        }

        [TestCase("nenwswse")]
        [TestCase("nenwsesw")]
        [TestCase("seswnenw")]
        [TestCase("swsenenw")]
        [TestCase("ew")]
        [TestCase("we")]
        [TestCase("wnese")]
        [TestCase("eswnw")]
        public void moves_to_zero(string input)
        {
            var solver = Solver();

            var result = solver.FindCoordinates(input);

            result.X.Should().Be(0);
            result.Y.Should().Be(0);
        }

        [Test]
        public void parses_single_input()
        {
            var input = "sesenwnenenewseeswwswswwnenewsewsw";
            var solver = Solver();

            var result = solver.ParseInstruction(input);

            //se se nw ne ne ne w se e sw w sw sw w ne ne w se w sw
            result.Should().HaveCount(20);
            result.Should().SatisfyRespectively(
                r => r.Should().BeOfType<SouthEast>(),
                r => r.Should().BeOfType<SouthEast>(),
                r => r.Should().BeOfType<NorthWest>(),
                r => r.Should().BeOfType<NorthEast>(),
                r => r.Should().BeOfType<NorthEast>(),
                r => r.Should().BeOfType<NorthEast>(),
                r => r.Should().BeOfType<West>(),
                r => r.Should().BeOfType<SouthEast>(),
                r => r.Should().BeOfType<East>(),
                r => r.Should().BeOfType<SouthWest>(),
                r => r.Should().BeOfType<West>(),
                r => r.Should().BeOfType<SouthWest>(),
                r => r.Should().BeOfType<SouthWest>(),
                r => r.Should().BeOfType<West>(),
                r => r.Should().BeOfType<NorthEast>(),
                r => r.Should().BeOfType<NorthEast>(),
                r => r.Should().BeOfType<West>(),
                r => r.Should().BeOfType<SouthEast>(),
                r => r.Should().BeOfType<West>(),
                r => r.Should().BeOfType<SouthWest>()
                ) ;
        }

    }
}
