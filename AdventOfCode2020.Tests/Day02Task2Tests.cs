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
    public class Day02Task2Tests
    {
        Day02Task2 Solution => new Day02Task2();


        [TestCase(1, 3, 'a', "abcde")]
        public void password_validation_returns_true(int first, int second, char letter, string password)
        {
            var policy = new Day02Task2.PasswordPositionsPolicy(first, second, letter);

            var result = policy.IsValid(password);

            result.Should().BeTrue();
        }

        [TestCase(1, 3, 'b', "cdefg")]
        [TestCase(2, 9, 'c', "ccccccccc")]
        public void password_validation_returns_false(int first, int second, char letter, string password)
        {
            var policy = new Day02Task2.PasswordPositionsPolicy(first, second, letter);

            var result = policy.IsValid(password);

            result.Should().BeFalse();
        }

        [Test]
        public void returns_1()
        {
            var input = new string[]
            {
                "1-3 a: abcde",
                "1-3 b: cdefg",
                "2-9 c: ccccccccc"
            };

            var result = Solution.ValidatePasswords(input);

            result.Should().Be(1);
        }

        [Test]
        public void returns_558()
        {
            var input = File.ReadAllLines("Files\\Day02.txt");

            var result = Solution.ValidatePasswords(input);
            Console.WriteLine(result);


            result.Should().Be(558);
        }
    }
}
