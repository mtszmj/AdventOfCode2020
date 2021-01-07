using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Tests
{
    [TestFixture]
    public class Day15Task2Tests
    {
        Day15Task2 _solver = new Day15Task2();

        public static IEnumerable Data()
        {
            yield return new TestCaseData(new int[] { 0, 3, 6 }, 175594, 30000000);
            yield return new TestCaseData(new int[] { 1, 3, 2 }, 2578, 30000000);
            yield return new TestCaseData(new int[] { 2, 1, 3 }, 3544142, 30000000);
            yield return new TestCaseData(new int[] { 1, 2, 3 }, 261214, 30000000);
            yield return new TestCaseData(new int[] { 2, 3, 1 }, 6895259, 30000000);
            yield return new TestCaseData(new int[] { 3, 2, 1 }, 18, 30000000);
            yield return new TestCaseData(new int[] { 3, 1, 2 }, 362, 30000000);
            yield return new TestCaseData(new int[] { 1, 20, 11, 6, 12, 0 }, 10652, 30000000);
        }

        [TestCaseSource(nameof(Data))]
        public void solves_input(int[] input, int expected, int turn)
        {
            var result = _solver.Solve(input, turn);
            result.Should().Be(expected);
        }
    }
}
