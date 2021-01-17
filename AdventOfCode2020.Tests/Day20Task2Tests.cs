using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdventOfCode2020.Day20Task2;

namespace AdventOfCode2020.Tests
{
    [TestFixture]
    public class Day20Task2Tests
    {
        Day20Task2 Solver() => new Day20Task2();

        [Test]
        public void solves_example()
        {
            var input = File.ReadAllText("Files\\Day20_1_ex.txt");
            var solver = Solver();

            var result = solver.Solve(input);
            result.Should().Be(273);
        }

        [Test]
        public void solves_input()
        {
            var input = File.ReadAllText("Files\\Day20.txt");
            var solver = Solver();

            var result = solver.Solve(input);
            result.Should().Be(2495);
        }

        [Test]
        public void check_rotations_and_flips_of_tile()
        {
            var tileData =
@"abcdefghij
!ABCDEFGH1
@aBcDeFgH2
#AbCdEfGh3
${:<,.>|}4
%[;(~`)\]5
^LmNoPqRs6
&lMnOpQrS7
*LMNOPQRS8
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

            Console.WriteLine(string.Join(Environment.NewLine, tile.Orientations.Select(o => $"{o.Tile.Id}:\n{o}\n\n{o.PrintBorderWithMiddle()}\n\n{o.PrintMiddle()}")));

        }
    }
    
}
