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
    public class Day02Task1Tests
    {
        Day02Task1 Solution => new Day02Task1();

        [Test]
        public void parse_password_with_policy_throws_argument_null_exception_when_null()
        {
            Action action = () => Solution.ParsePasswordWithPolicy(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void parse_password_with_policy_throws_argument_exception_when_incorrect_format()
        {
            Action action = () => Solution.ParsePasswordWithPolicy("1-3 asdadsa");

            action.Should().Throw<ArgumentException>().WithMessage("Incorrect format");
        }

        [Test]
        public void parse_password_with_policy_throws_argument_exception_when_only_one_value_in_policy()
        {
            Action action = () => Solution.ParsePasswordWithPolicy("1 a: asdadsa");

            action.Should().Throw<ArgumentException>().WithMessage("Incorrect values format");
        }

        [Test]
        public void parse_password_with_policy_throws_argument_exception_when_more_than_two_values_in_policy()
        {
            Action action = () => Solution.ParsePasswordWithPolicy("1-3-5 a: asdadsa");

            action.Should().Throw<ArgumentException>().WithMessage("Incorrect values format");
        }

        [Test]
        public void parse_password_with_policy_throws_argument_exception_when_cannot_parse_first_policy_value()
        {
            Action action = () => Solution.ParsePasswordWithPolicy("a-5 a: asdadsa");

            action.Should().Throw<ArgumentException>().WithMessage("Incorrect values format");
        }

        [Test]
        public void parse_password_with_policy_throws_argument_exception_when_cannot_parse_second_policy_value()
        {
            Action action = () => Solution.ParsePasswordWithPolicy("1-b a: asdadsa");

            action.Should().Throw<ArgumentException>().WithMessage("Incorrect values format");
        }

        [Test]
        public void parse_password_with_policy_returns_correct_policy()
        {
            var (policy, _) = Solution.ParsePasswordWithPolicy("1-3 a: babcde");

            policy.MinimumOccurance.Should().Be(1);
            policy.MaximumOccurance.Should().Be(3);
            policy.Letter.Should().Be('a');
        }

        [Test]
        public void parse_password_with_policy_returns_correct_password()
        {
            var (_, password) = Solution.ParsePasswordWithPolicy("1-3 a: babcde");

            password.Should().Be("babcde");
        }

        [TestCase(1, 3, 'a', "abcde")]
        [TestCase(2, 9, 'c', "ccccccccc")]
        public void password_validation_returns_true(int min, int max, char letter, string password)
        {
            var policy = new Day02Task1.PasswordPolicy(min, max, letter);

            var result = policy.IsValid(password);

            result.Should().BeTrue();
        }

        [TestCase(1, 3, 'b', "cdefg")]
        public void password_validation_returns_false(int min, int max, char letter, string password)
        {
            var policy = new Day02Task1.PasswordPolicy(min, max, letter);

            var result = policy.IsValid(password);

            result.Should().BeFalse();
        }

        [Test]
        public void returns_2()
        {
            var input = new string[]
            {
                "1-3 a: abcde",
                "1-3 b: cdefg",
                "2-9 c: ccccccccc"
            };

            var result = Solution.ValidatePasswords(input);

            result.Should().Be(2);
        }

        [Test]
        public void returns_536()
        {
            var input = File.ReadAllLines("Files\\Day02.txt");

            var result = Solution.ValidatePasswords(input);
            Console.WriteLine(result);


            result.Should().Be(536);
        }
    }
}
