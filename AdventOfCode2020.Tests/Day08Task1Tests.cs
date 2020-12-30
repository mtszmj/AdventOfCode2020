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
    public class Day08Task1Tests
    {
        [Test]
        public void handle_nop_operation_returns_the_same_accumulator()
        {
            var state = new Day08Task1.State(123, 10);
            var nop = new Day08Task1.Nop(11);

            var result = nop.Handle(state);

            result.Accumulator.Should().Be(123);
        }
        
        [Test]
        public void handle_nop_operation_returns_next_line_of_1_more()
        {
            var state = new Day08Task1.State(123, 10);
            var nop = new Day08Task1.Nop(12);

            var result = nop.Handle(state);

            result.NextLine.Should().Be(11);
        }

        [Test]
        public void handle_nop_operation_sets_was_executed_flag()
        {
            var state = new Day08Task1.State(123, 10);
            var nop = new Day08Task1.Nop(13);

            nop.WasExecuted.Should().BeFalse();
            var result = nop.Handle(state);

            nop.WasExecuted.Should().BeTrue();
        }

        [TestCase(10)]
        [TestCase(-10)]
        [TestCase(0)]
        public void handle_acc_operation_changes_accumulator_value(int value)
        {
            var startingAccumulatorValue = 123;
            var state = new Day08Task1.State(startingAccumulatorValue, 10);
            var acc = new Day08Task1.Acc(value);

            var result = acc.Handle(state);

            result.Accumulator.Should().Be(startingAccumulatorValue + value);
        }

        [Test]
        public void handle_acc_operation_returns_next_line_of_1_more()
        {
            var state = new Day08Task1.State(123, 10);
            var acc = new Day08Task1.Acc(124);

            var result = acc.Handle(state);

            result.NextLine.Should().Be(11);
        }

        [Test]
        public void handle_acc_operation_sets_was_executed_flag()
        {
            var state = new Day08Task1.State(123, 10);
            var acc = new Day08Task1.Acc(124);

            acc.WasExecuted.Should().BeFalse();
            var result = acc.Handle(state);

            acc.WasExecuted.Should().BeTrue();
        }

        [Test]
        public void handle_jmp_operation_returns_the_same_accumulator()
        {
            var startingAccumulatorValue = 123;
            var state = new Day08Task1.State(startingAccumulatorValue, 10);
            var jmp = new Day08Task1.Jmp(11);

            var result = jmp.Handle(state);

            result.Accumulator.Should().Be(startingAccumulatorValue);
        }


        [TestCase(10)]
        [TestCase(-10)]
        [TestCase(0)]
        public void handle_jmp_operation_returns_next_line_with_offset(int offset)
        {
            var startingLineValue = 10;
            var state = new Day08Task1.State(123, startingLineValue);
            var jmp = new Day08Task1.Jmp(offset);

            var result = jmp.Handle(state);

            result.NextLine.Should().Be(startingLineValue + offset);
        }

        [Test]
        public void handle_jmp_operation_sets_was_executed_flag()
        {
            var state = new Day08Task1.State(123, 10);
            var jmp = new Day08Task1.Jmp(124);

            jmp.WasExecuted.Should().BeFalse();
            var result = jmp.Handle(state);

            jmp.WasExecuted.Should().BeTrue();
        }

        [Test]
        public void parses_input_to_list_of_operations()
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

            var solver = new Day08Task1();
            var result = solver.ParseInput(input.Split(Environment.NewLine)).ToList();

            result.Should().HaveCount(9);
            result[0].Should().BeOfType<Day08Task1.Nop>().And.Subject.As<Day08Task1.Nop>().Value.Should().Be(0);
            result[1].Should().BeOfType<Day08Task1.Acc>().And.Subject.As<Day08Task1.Acc>().Value.Should().Be(1);
            result[2].Should().BeOfType<Day08Task1.Jmp>().And.Subject.As<Day08Task1.Jmp>().Offset.Should().Be(4);
            result[3].Should().BeOfType<Day08Task1.Acc>().And.Subject.As<Day08Task1.Acc>().Value.Should().Be(3);
            result[4].Should().BeOfType<Day08Task1.Jmp>().And.Subject.As<Day08Task1.Jmp>().Offset.Should().Be(-3);
            result[5].Should().BeOfType<Day08Task1.Acc>().And.Subject.As<Day08Task1.Acc>().Value.Should().Be(-99);
            result[6].Should().BeOfType<Day08Task1.Acc>().And.Subject.As<Day08Task1.Acc>().Value.Should().Be(1);
            result[7].Should().BeOfType<Day08Task1.Jmp>().And.Subject.As<Day08Task1.Jmp>().Offset.Should().Be(-4);
            result[8].Should().BeOfType<Day08Task1.Acc>().And.Subject.As<Day08Task1.Acc>().Value.Should().Be(6);
        }

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

            var solver = new Day08Task1();
            var result = solver.Solve(input.Split(Environment.NewLine));

            result.Should().Be(5);
        }

        [Test]
        public void returns_1867_acc_value_when_infite_loop_is_found()
        {
            var input = File.ReadAllText("Files\\Day08.txt");

            var solver = new Day08Task1();
            var result = solver.Solve(input.Split(Environment.NewLine));

            result.Should().Be(1867);
        }
    }
}
