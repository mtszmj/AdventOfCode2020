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
    public class Day04Task1Tests
    {
        [Test]
        public void count_2_valid_passports_with_optional_cid()
        {
            var input =
@"ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
byr:1937 iyr:2017 cid:147 hgt:183cm

iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
hcl:#cfa07d byr:1929

hcl:#ae17e1 iyr:2013
eyr:2024
ecl:brn pid:760753108 byr:1931
hgt:179cm

hcl:#cfa07d eyr:2025 pid:166559648
iyr:2011 ecl:brn hgt:59in";

            var result = new Day04Task1().CheckPassports(input, new Day04Task1.CountryIdOptionalValidator());

            result.Should().Be(2);
        }

        [Test]
        public void count_228_valid_passports_with_optional_cid()
        {
            var input = File.ReadAllText("Files\\Day04.txt");

            var result = new Day04Task1().CheckPassports(input, new Day04Task1.CountryIdOptionalValidator());

            result.Should().Be(228);
        }
    }
}
