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
    public class Day04Task2Tests
    {
        Day04Task1 PassportChecker => new Day04Task1();
        Day04Task1.IPassportValidator Validator => new Day04Task2.DetailedValidator();

        [Test]
        public void count_4_valid_passports_with_optional_cid()
        {
            var input =
@"pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980
hcl:#623a2f

eyr:2029 ecl:blu cid:129 byr:1989
iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm

hcl:#888785
hgt:164cm byr:2001 iyr:2015 cid:88
pid:545766238 ecl:hzl
eyr:2022

iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719";

            var result = PassportChecker.CheckPassports(input, Validator);

            result.Should().Be(4);
        }

        [Test]
        public void count_0_valid_passports_with_optional_cid()
        {
            var input =
@"eyr:1972 cid:100
hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926

iyr:2019
hcl:#602927 eyr:1967 hgt:170cm
ecl:grn pid:012533040 byr:1946

hcl:dab227 iyr:2012
ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277

hgt:59cm ecl:zzz
eyr:2038 hcl:74454a iyr:2023
pid:3556412378 byr:2007";

            var result = PassportChecker.CheckPassports(input, Validator);

            result.Should().Be(0);
        }

        [Test]
        public void valid_birth_year()
        {
            var input =
@"hcl:#888785
hgt:164cm byr:2002 iyr:2015 cid:88
pid:545766238 ecl:hzl
eyr:2022
";

            var result = PassportChecker.CheckPassports(input, Validator);

            result.Should().Be(1);
        }

        [Test]
        public void invalid_birth_year()
        {
            var input =
@"hcl:#888785
hgt:164cm byr:2003 iyr:2015 cid:88
pid:545766238 ecl:hzl
eyr:2022
";

            var result = PassportChecker.CheckPassports(input, Validator);

            result.Should().Be(0);
        }

        [Test]
        public void valid_height_in_inches()
        {
            var input =
@"hcl:#888785
hgt:60in byr:2002 iyr:2015 cid:88
pid:545766238 ecl:hzl
eyr:2022
";

            var result = PassportChecker.CheckPassports(input, Validator);

            result.Should().Be(1);
        }


        [Test]
        public void valid_height_in_centimeters()
        {
            var input =
@"hcl:#888785
hgt:190cm byr:2002 iyr:2015 cid:88
pid:545766238 ecl:hzl
eyr:2022
";

            var result = PassportChecker.CheckPassports(input, Validator);

            result.Should().Be(1);
        }


        [Test]
        public void invalid_height_in_inches()
        {
            var input =
@"hcl:#888785
hgt:190in byr:2002 iyr:2015 cid:88
pid:545766238 ecl:hzl
eyr:2022
";

            var result = PassportChecker.CheckPassports(input, Validator);

            result.Should().Be(0);
        }

        [Test]
        public void invalid_height()
        {
            var input =
@"hcl:#888785
hgt:190 byr:2002 iyr:2015 cid:88
pid:545766238 ecl:hzl
eyr:2022
";

            var result = PassportChecker.CheckPassports(input, Validator);

            result.Should().Be(0);
        }

        [Test]
        public void valid_hair_color()
        {
            var input =
@"hcl:#123abc
hgt:190cm byr:2002 iyr:2015 cid:88
pid:545766238 ecl:hzl
eyr:2022
";

            var result = PassportChecker.CheckPassports(input, Validator);

            result.Should().Be(1);
        }

        [Test]
        public void invalid_hair_color_in_letters()
        {
            var input =
@"hcl:#123abz
hgt:190cm byr:2002 iyr:2015 cid:88
pid:545766238 ecl:hzl
eyr:2022
";

            var result = PassportChecker.CheckPassports(input, Validator);

            result.Should().Be(0);
        }

        [Test]
        public void invalid_hair_color_no_hash()
        {
            var input =
@"hcl:123abz
hgt:190cm byr:2002 iyr:2015 cid:88
pid:545766238 ecl:hzl
eyr:2022
";

            var result = PassportChecker.CheckPassports(input, Validator);

            result.Should().Be(0);
        }

        [Test]
        public void valid_eye_color()
        {
            var input =
@"hcl:#123abc
hgt:190cm byr:2002 iyr:2015 cid:88
pid:545766238 ecl:brn
eyr:2022
";

            var result = PassportChecker.CheckPassports(input, Validator);

            result.Should().Be(1);
        }

        [Test]
        public void invalid_eye_color()
        {
            var input =
@"hcl:#123abc
hgt:190cm byr:2002 iyr:2015 cid:88
pid:545766238 ecl:wat
eyr:2022
";

            var result = PassportChecker.CheckPassports(input, Validator);

            result.Should().Be(0);
        }

        [Test]
        public void valid_pid()
        {
            var input =
@"hcl:#123abc
hgt:190cm byr:2002 iyr:2015 cid:88
pid:000000001 ecl:brn
eyr:2022
";

            var result = PassportChecker.CheckPassports(input, Validator);

            result.Should().Be(1);
        }

        [Test]
        public void invalid_pid()
        {
            var input =
@"hcl:#123abc
hgt:190cm byr:2002 iyr:2015 cid:88
pid:0123456789 ecl:brn
eyr:2022
";

            var result = PassportChecker.CheckPassports(input, Validator);

            result.Should().Be(0);
        }


        [Test]
        public void count_175_valid_passports_with_optional_cid()
        {
            var input = File.ReadAllText("Files\\Day04.txt");

            var result = PassportChecker.CheckPassports(input, Validator);

            result.Should().Be(175);
        }
    }
}
