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
    public class Day16Task1Tests
    {
        Day16Task1 _solver = new Day16Task1();

        [Test]
        public void solves_input()
        {
            var result = _solver.Solve(File.ReadAllText("Files\\Day16.txt"));

            result.Should().Be(23044);
        }

        [Test]
        public void solves_example()
        {
            var input = @"class: 1-3 or 5-7
row: 6-11 or 33-44
seat: 13-40 or 45-50

your ticket:
7,1,14

nearby tickets:
7,3,47
40,4,50
55,2,20
38,6,12";

            var result = _solver.Solve(input);

            result.Should().Be(71);
        }

        [Test]
        public void parse_all()
        {
            var input = @"class: 1-3 or 5-7
row: 6-11 or 33-44
seat: 13-40 or 45-50

your ticket:
7,1,14

nearby tickets:
7,3,47
40,4,50
55,2,20
38,6,12";

            var result = _solver.Parse(input);

            result.Rules.Should().HaveCount(3);
            result.YourTicket.Should().NotBeNull();
            result.OtherTickets.Should().HaveCount(4);
        }


        [Test]
        public void parse_rules()
        {
            var input = @"class: 1-3 or 5-7
row: 6-11 or 33-44
seat: 13-40 or 45-50
";

            var result = _solver.ParseRules(input);

            result.Should().SatisfyRespectively(
                f =>
                {
                    f.Name.Should().Be("class");
                    f.ValidNumbers.Should().Contain(new int[] { 1, 2, 3, 5, 6, 7 });
                },
                s =>
                {
                    s.Name.Should().Be("row");
                    s.ValidNumbers.Should().Contain(new int[] { 6, 7, 8, 9, 10, 11,
                        33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44 });
                },
                t =>
                {
                    t.Name.Should().Be("seat");
                    t.ValidNumbers.Should().Contain(new int[] { 13,14,15,16,17,18,19,
                        20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,
                        45,46,47,48,49,50
                    });
                }
                );
        }

        [Test]
        public void parse_your_ticket()
        {
            var input = @"your ticket:
7,1,14";

            var result = _solver.ParseYourTicket(input);

            result.Numbers.Should().Contain(new[] { 7, 1, 14 });
        }

        [Test]
        public void parse_other_tickets()
        {
            var input = @"nearby tickets:
7,3,47
40,4,50
55,2,20
38,6,12";

            var result = _solver.ParseOtherTickets(input);

            result.Should().SatisfyRespectively(
                f => f.Numbers.Should().Contain(new int[] { 7, 3, 47 }),
                s => s.Numbers.Should().Contain(new int[] { 40, 4, 50 }),
                t => t.Numbers.Should().Contain(new int[] { 55, 2, 20 }),
                fo => fo.Numbers.Should().Contain(new int[] { 38, 6, 12 })
                );
        }
    }
}
