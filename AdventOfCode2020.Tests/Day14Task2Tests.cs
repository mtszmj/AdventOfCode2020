using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdventOfCode2020.Day14Task2;

namespace AdventOfCode2020.Tests
{
    [TestFixture]
    public class Day14Task2Tests
    {
        Day14Task2 _solver = new Day14Task2();

        [Test]
        public void solves_example_as_208()
        {
            var input = @"mask = 000000000000000000000000000000X1001X
mem[42] = 100
mask = 00000000000000000000000000000000X0XX
mem[26] = 1";

            var result = _solver.Solve(input);

            result.Should().Be(208);
        }

        [Test]
        public void solves_example_as_408()
        {
            var input = @"mask = 000000000000000000000000000000X1001X
mem[42] = 100
mem[42] = 200
mask = 00000000000000000000000000000000X0XX
mem[26] = 1";

            var result = _solver.Solve(input);

            result.Should().Be(408);
        }

        [Test]
        public void solves_mask_of_all_zeroes()
        {
            var input = @"mask = 000000000000000000000000000000000000
mem[42] = 100";

            var result = _solver.Solve(input);

            result.Should().Be(100);
        }

        [Test]
        public void solves_mask_of_all_ones()
        {
            var input = @"mask = 111111111111111111111111111111111111
mem[42] = 100";

            var result = _solver.Solve(input);

            result.Should().Be(100);
        }

        [Test]
        public void solves_mask_starting_with_X()
        {
            var input = @"mask = X00000000000000000000000000000000000
mem[42] = 100";

            var result = _solver.Solve(input);

            result.Should().Be(200);
        }

        [Test]
        public void solves_mask_ending_with_X()
        {
            var input = @"mask = 00000000000000000000000000000000000X
mem[42] = 100";

            var result = _solver.Solve(input);

            result.Should().Be(200);
        }

        [Test]
        public void processes_mask_as_4_combinations()
        {
            var mask = new AddressMaskCommand("000000000000000000000000000000X1001X");
            mask.ProcessAddress(42);
            mask.Values.Should().HaveCount(4);
            mask.Values.Should().Contain(new[] { 26L, 27L, 58L, 59L });
        }

        [Test]
        public void initializes_mask_as_8_combinations()
        {
            var mask = new AddressMaskCommand("00000000000000000000000000000000X0XX");
            mask.ProcessAddress(26);
            mask.Values.Should().HaveCount(8);
            mask.Values.Should().Contain(new[] { 16L, 17L, 18L, 19L, 24L, 25L, 26L, 27L });
        }

        [Test]
        public void initializes_mask_as_8_combinations_from_8_to_15()
        {
            var mask = new AddressMaskCommand("000000000000000000000000000000000XXX");
            mask.ProcessAddress(8);
            mask.Values.Should().HaveCount(8);
            mask.Values.Should().Contain(new[] { 8L, 9L, 10L, 11L, 12L, 13L, 14L, 15L });
        }

        [Test]
        public void initializes_mask_as_4_combinations_from_8_to_15()
        {
            var mask = new AddressMaskCommand("XX0000000000000000000000000000000000");
            mask.ProcessAddress(0);
            mask.Values.Should().HaveCount(4);
            mask.Values.Should().Contain(new[] { 0L, 17179869184L, 34359738368L, 51539607552L });
        }

        [Test]
        public void initializes_mask_as_4_combinations()
        {
            var mask = new AddressMaskCommand("X0000000000000000000000000000000000X");
            mask.ProcessAddress(0);
            mask.Values.Should().HaveCount(4);
            mask.Values.Should().Contain(new[] { 0L, 1L, 34359738368L, 34359738369L });
        }

        [Test]
        public void initializes_mask_as_4_combinations_with_plus_2()
        {
            var mask = new AddressMaskCommand("X0000000000000000000000000000000001X");
            mask.ProcessAddress(0);
            mask.Values.Should().HaveCount(4);
            mask.Values.Should().Contain(new[] { 2L, 3L, 34359738370L, 34359738371L });
        }

        [Test]
        public void sums_file_memory_to_4401465949086()
        {
            var input = File.ReadAllText("Files\\Day14.txt");

            var result = _solver.Solve(input);

            result.Should().Be(4401465949086);
        }

        [Test]
        public void sums_to_52()
        {
            var input = @"mask = 000000000000000000000000000000000XXX
mem[8] = 4
mask = XX0000000000000000000000000000000000
mem[0] = 5";

            var result = _solver.Solve(input);

            result.Should().Be(52);
        }

        [Test]
        public void sums_to_6()
        {
            var input = @"mask = 00000000000000000000000000000000000X
mem[1] = 7
mem[0] = 3";

            var result = _solver.Solve(input);

            result.Should().Be(6);
        }

        [Test]
        public void sums_to_508032()
        {
            var input = @"mask = 0XX000X1111001010X10XX1101XX00X00100
mem[50596] = 1000
mask = 0X000001111001010X1011100100001X0X0X
mem[45713] = 1";

            var result = _solver.Solve(input);

            result.Should().Be(508032);
        }

        [Test]
        public void sums_to_()
        {
            var inputLine1 = "mask = 0XX000X1111001010X10XX1101XX00X00100";
            var inputLine2 = "mem[0] = 1000";
            var inputLine3 = "mask = 0X000001111001010X1011100100001X0X0X";
            var inputLine4 = "mem[45713] = 1";

            var result = _solver.Solve(inputLine1 + Environment.NewLine + inputLine2);

            // 0XX000X1111001010X10XX1101XX00X00100
            //                     1100010110100100
            // 0XX000X1111001010X10XX1101XX10X00100
            // 512 combinations * 1000
            result.Should().Be(512000);
        }

        [Test]
        public void checks_mask_with_8_X_first_octad()
        {
            var inputLine1 = "mask = 0000000000000000000000000000XXXXXXXX";
            var inputLine2 = "mem[0] = 1000";

            var result = _solver.Solve(inputLine1 + Environment.NewLine + inputLine2);
            
            result.Should().Be(256000);
            _solver.System.Mask.Values.Count.Should().Be(256);
        }

        [Test]
        public void checks_mask_with_8_X_second_octad()
        {
            var inputLine1 = "mask = 00000000000000000000XXXXXXXX00000000";
            var inputLine2 = "mem[0] = 1000";

            var result = _solver.Solve(inputLine1 + Environment.NewLine + inputLine2);

            result.Should().Be(256000);
            _solver.System.Mask.Values.Count.Should().Be(256);
        }

        [Test]
        public void checks_mask_with_8_X_third_octad()
        {
            var inputLine1 = "mask = 000000000000XXXXXXXX0000000000000000";
            var inputLine2 = "mem[0] = 1000";

            var result = _solver.Solve(inputLine1 + Environment.NewLine + inputLine2);

            result.Should().Be(256000);
            _solver.System.Mask.Values.Count.Should().Be(256);
        }

        [Test]
        public void checks_mask_with_8_X_fourth_octad()
        {
            var inputLine1 = "mask = 0000XXXXXXXX000000000000000000000000";
            var inputLine2 = "mem[0] = 1000";

            var result = _solver.Solve(inputLine1 + Environment.NewLine + inputLine2);

            result.Should().Be(256000);
            _solver.System.Mask.Values.Count.Should().Be(256);
        }

        [Test]
        public void checks_mask_with_2_X_at_the_beginning_and_multiple_ones()
        {
            var inputLine1 = "mask = XX1111111111111111111111111111111000";
            var inputLine2 = "mem[0] = 10";

            var result = _solver.Solve(inputLine1 + Environment.NewLine + inputLine2);

            _solver.System.Mask.Values.Should().Contain(new long[] {
                0b001111111111111111111111111111111000,
                0b011111111111111111111111111111111000,
                0b101111111111111111111111111111111000,
                0b111111111111111111111111111111111000
                });

            Console.WriteLine(string.Join(Environment.NewLine, _solver.System.Mask.Values.Select(x => Convert.ToString(x, 2))));

            result.Should().Be(40);
            _solver.System.Mask.Values.Count.Should().Be(4);
        }

        [Test]
        public void test_step_by_step()
        {
            var inputLine1 = "mask = 11100XX0000X1101X1010100X1010001XX0X";
            var inputLine2 = "mem[24196] = 465592";
            var inputLine3 = "mem[17683] = 909049";
            var inputLine4 = "mem[28999] = 20912603";
            var inputLine5 = "mem[22864] = 7675";
            var inputLine6 = "mem[55357] = 6401";
            var inputLine7 = "mem[47006] = 1087112";

            // step 1
            var result = _solver.Solve(inputLine1);

            result.Should().Be(0);
            _solver.System.Mask.MaskValue.Should().Be("11100XX0000X1101X1010100X1010001XX0X");

            // step 2
            result = _solver.Solve(inputLine1+Environment.NewLine+inputLine2);
            // mask 11100XX0000X1101X1010100X1010001XX0X
            // add  000000000000000000000101111010000100 (24196)
            // res  11000XX0000X1101X1010101X1101000XX0X
            // 256 combinations * 465592
            result.Should().Be(256 * 465592);
            _solver.System.Memory.Count.Should().Be(256);

            // step 3
            result = _solver.Solve(inputLine1 + Environment.NewLine + inputLine2 + Environment.NewLine + inputLine3);
            // mask 11100XX0000X1101X1010100X1010001XX0X
            // add  000000000000000000000101111010000100 (24196)
            // res  11000XX0000X1101X1010101X1101000XX0X
            // 256 combinations * 465592
            // add  000000000000000000000101111010000100 (17683)
            // res  11100XX0000X1101X1010101X1111001XX0X
            // 256 combinations * 909049
            result.Should().Be(256 * 465592 + 256 * 909049);
            _solver.System.Memory.Count.Should().Be(512);

        }
    }
}
