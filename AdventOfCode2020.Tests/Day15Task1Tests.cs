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
    public class Day15Task1Tests
    {
        Day15Task1 _solver = new Day15Task1();

        public static IEnumerable Data()
        {
            yield return new TestCaseData(new int[] { 0, 3, 6 }, 0, 1);
            yield return new TestCaseData(new int[] { 0, 3, 6 }, 3, 2);
            yield return new TestCaseData(new int[] { 0, 3, 6 }, 6, 3);
            yield return new TestCaseData(new int[] { 0, 3, 6 }, 0, 4);
            yield return new TestCaseData(new int[] { 0, 3, 6 }, 3, 5);
            yield return new TestCaseData(new int[] { 0, 3, 6 }, 3, 6);
            yield return new TestCaseData(new int[] { 0, 3, 6 }, 1, 7);
            yield return new TestCaseData(new int[] { 0, 3, 6 }, 0, 8);
            yield return new TestCaseData(new int[] { 0, 3, 6 }, 4, 9);
            yield return new TestCaseData(new int[] { 0, 3, 6 }, 0, 10);
            yield return new TestCaseData(new int[] { 0, 3, 6 }, 436, 2020);
            yield return new TestCaseData(new int[] { 1, 3, 2 }, 1, 2020);
            yield return new TestCaseData(new int[] { 2, 1, 3 }, 10, 2020);
            yield return new TestCaseData(new int[] { 1, 2, 3 }, 27, 2020);
            yield return new TestCaseData(new int[] { 2, 3, 1 }, 78, 2020);
            yield return new TestCaseData(new int[] { 3, 2, 1 }, 438, 2020);
            yield return new TestCaseData(new int[] { 3, 1, 2 }, 1836, 2020);
            yield return new TestCaseData(new int[] { 1, 20, 11, 6, 12, 0 }, 1085, 2020);
        }

        [TestCaseSource(nameof(Data))]
        public void solves_input(int[] input, int expected, int turn)
        {
            var result = _solver.Solve(input, turn);
            result.Should().Be(expected);
        }
    }
}
