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
    public class Day08Task2Tests
    {
        [Test]
        public void returns_5_acc_value_when_infite_loop_is_found()
        {
            var input =
@"nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6";

            var solver = new Day08Task2();
            var result = solver.Solve(input.Split(Environment.NewLine));

            result.Should().Be(8);
        }

        [Test]
        public void returns_1303_acc_value_when_infite_loop_is_found()
        {
            var input = File.ReadAllText("Files\\Day08.txt");

            var solver = new Day08Task2();
            var result = solver.Solve(input.Split(Environment.NewLine));

            result.Should().Be(1303);
        }
    }
}
