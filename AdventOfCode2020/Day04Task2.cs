using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day04Task2
    {
        public class DetailedValidator : AbstractValidator<Day04Task1.PassportData>, Day04Task1.IPassportValidator
        {
            private static readonly HashSet<char> _allowedCharsInHairColor
                = new HashSet<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };

            private static readonly HashSet<string> _allowedEyeColor
                = new HashSet<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

            public DetailedValidator()
            {
                this.CascadeMode = CascadeMode.Stop;

                RuleFor(x => x.BirthYear)
                    .NotEmpty()
                    .Must(byr => ValidateYear(byr, 1920, 2002))
                    ;

                RuleFor(x => x.IssueYear)
                    .NotEmpty()
                    .Must(iyr => ValidateYear(iyr, 2010, 2020))
                    ;

                RuleFor(x => x.ExpirationYear)
                    .NotEmpty()
                    .Must(eyr => ValidateYear(eyr, 2020, 2030))
                    ;

                RuleFor(x => x.Height)
                    .NotEmpty()
                    .Must(x => x.EndsWith("cm") || x.EndsWith("in"))
                    .Must(x => int.TryParse(x.Substring(0, x.Length - 2), out var value) && value >= 150 && value <= 193)
                    .When(x => x.Height.EndsWith("cm"), ApplyConditionTo.CurrentValidator)
                    .Must(x => int.TryParse(x.Substring(0, x.Length - 2), out var value) && value >= 59 && value <= 76)
                    .When(x => x.Height.EndsWith("in"), ApplyConditionTo.CurrentValidator)
                    ;

                RuleFor(x => x.HairColor)
                    .NotEmpty()
                    .Must(x => x.StartsWith("#"))
                    .Must(x => x.Length == 7)
                    .Must(x =>
                    {
                        var characters = x.Substring(1, x.Length - 1).ToHashSet();
                        characters.RemoveWhere(c => _allowedCharsInHairColor.Contains(c));
                        return characters.Count == 0;
                    })
                    ;

                RuleFor(x => x.EyeColor)
                    .NotEmpty()
                    .Must(x => _allowedEyeColor.Contains(x))
                    ;

                RuleFor(x => x.PassportId)
                    .NotEmpty()
                    .Must(x => x.Length == 9)
                    .Must(x => long.TryParse(x, out var _))
                    ;
            }

            bool Day04Task1.IPassportValidator.Validate(Day04Task1.PassportData data)
            {
                var result = this.Validate(data);
                return result.IsValid;
            }

            private bool ValidateYear(string yearValue, int minimum, int maximum)
            {
                if (int.TryParse(yearValue, out var year) && year >= minimum && year <= maximum)
                    return true;

                return false;
            }
        }
    }
}
