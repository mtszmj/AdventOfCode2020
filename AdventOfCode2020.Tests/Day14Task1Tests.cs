using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static AdventOfCode2020.Day14Task1;

namespace AdventOfCode2020.Tests
{
    [TestFixture]
    public class Day14Task1Tests
    {
        Day14Task1 _solver = new Day14Task1();

        [Test]
        public void check_regex_for_mask()
        {
            var mask = "mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X";

            var result = Day14Task1.MaskRegex.Match(mask);

            result.Success.Should().BeTrue();
            result.Groups[1].Value.Should().Be("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X");
        }

        [Test]
        public void check_regex_for_memory()
        {
            var memory = "mem[8] = 11";

            var result = Day14Task1.MemoryRegex.Match(memory);

            result.Success.Should().BeTrue();
            result.Groups[1].Value.Should().Be("8");
            result.Groups[2].Value.Should().Be("11");
        }

        [Test]
        public void mask_value_11_to_73()
        {
            var mask = new MaskCommand("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X");
            var result = mask.ProcessValue(11);
            result.Should().Be(73);
        }

        [Test]
        public void parses_input_to_commands()
        {
            var input = @"mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
mem[8] = 11
mem[7] = 101
mem[8] = 0";

            var result = _solver.ParseInput(input).ToList();

            result.Should().HaveCount(4);
            
            result[0].Should().BeOfType<MaskCommand>()
                .And.Subject.As<MaskCommand>().Value.Should().Be("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X");

            result[1].Should().BeOfType<MemoryCommand>();
            result[1].As<MemoryCommand>().Address.Should().Be(8);
            result[1].As<MemoryCommand>().Value.Should().Be(11);

            result[2].Should().BeOfType<MemoryCommand>();
            result[2].As<MemoryCommand>().Address.Should().Be(7);
            result[2].As<MemoryCommand>().Value.Should().Be(101);

            result[3].Should().BeOfType<MemoryCommand>();
            result[3].As<MemoryCommand>().Address.Should().Be(8);
            result[3].As<MemoryCommand>().Value.Should().Be(0);
        }

        [Test]
        public void sums_memory_to_165()
        {
            var input = @"mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
mem[8] = 11
mem[7] = 101
mem[8] = 0";

            var result = _solver.Solve(input);

            result.Should().Be(165);
        }

        [Test]
        public void sums_file_memory_to_17765746710228()
        {
            var input = File.ReadAllText("Files\\Day14.txt");

            var result = _solver.Solve(input);

            result.Should().Be(17765746710228);
        }
    }
}
