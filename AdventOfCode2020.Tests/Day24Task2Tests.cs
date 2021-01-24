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
    public class Day24Task2Tests
    {
        Day24Task2 Solver() => new Day24Task2();

        [Test]
        public void solves_input()
        {
            var input = File.ReadAllText("Files\\Day24.txt");

            var solver = Solver();

            var result = solver.Solve(input);

            result.Should().Be(3777);
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

            result.Should().Be(2208);
        }
    }
}
