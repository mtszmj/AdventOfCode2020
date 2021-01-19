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
    public class Day21Task2Tests
    {
        Day21Task2 Solver() => new Day21Task2();

        [Test]
        public void solves_example()
        {
            var input = @"mxmxvkd kfcds sqjhc nhms (contains dairy, fish)
trh fvjkl sbzzf mxmxvkd (contains dairy)
sqjhc fvjkl (contains soy)
sqjhc mxmxvkd sbzzf (contains fish)";

            var solver = Solver();
            var result = solver.Solve(input);

            result.Should().Be("mxmxvkd,sqjhc,fvjkl");
        }

        [Test]
        public void solves_input()
        {
            var input = File.ReadAllText("Files\\Day21.txt");
            var solver = Solver();
            var result = solver.Solve(input);

            result.Should().Be("vrzkz,zjsh,hphcb,mbdksj,vzzxl,ctmzsr,rkzqs,zmhnj");
        }
    }
}
