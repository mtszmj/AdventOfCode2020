using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day04Task1
    {
        private static string PassportsSeparator = $"{Environment.NewLine}{Environment.NewLine}";
        private static string[] PassportDataSeparator = new string[] { " ", Environment.NewLine };
        private static string PassportDataValueSeparator = ":";

        public int CheckPassports(string data, IPassportValidator validator)
        {
            var passports = SplitPassports(data);
            return passports.Select(x => PassportData.ParsePassport(x).IsValid(validator))
                            .Count(x => x == true);
        }

        public string[] SplitPassports(string data)
        {
            return data.Split(PassportsSeparator);
        }

        public class PassportData
        {
            private Dictionary<string, Action<string>> KeyValueFunctions;

            public PassportData()
            {
                KeyValueFunctions = new Dictionary<string, Action<string>>
                {

                    ["byr"] = byr => BirthYear = byr,
                    ["iyr"] = iyr => IssueYear = iyr,
                    ["eyr"] = eyr => ExpirationYear = eyr,
                    ["hgt"] = hgt => Height = hgt,
                    ["hcl"] = hcl => HairColor = hcl,
                    ["ecl"] = ecl => EyeColor = ecl,
                    ["pid"] = pid => PassportId = pid,
                    ["cid"] = cid => CountryId = cid
                };
            }

            public string BirthYear { get; set; }
            public string IssueYear { get; set; }
            public string ExpirationYear { get; set; }
            public string Height { get; set; }
            public string HairColor { get; set; }
            public string EyeColor { get; set; }
            public string PassportId { get; set; }
            public string CountryId { get; set; }

            public bool IsValid(IPassportValidator validator)
            {
                return validator?.Validate(this) ?? false;
            }

            public static PassportData ParsePassport(string data)
            {
                var fields = data.Split(PassportDataSeparator, StringSplitOptions.RemoveEmptyEntries);

                var passport = new PassportData();
                foreach (var field in fields)
                {
                    var keyValue = field.Split(PassportDataValueSeparator);
                    if (keyValue.Length != 2)
                        continue;

                    if (!passport.KeyValueFunctions.TryGetValue(keyValue[0].Trim(), out var action))
                        continue;

                    action?.Invoke(keyValue[1].Trim());
                }

                return passport;
            }
        }

        public interface IPassportValidator
        {
            bool Validate(PassportData data);
        }

        public class CountryIdOptionalValidator : IPassportValidator
        {
            public bool Validate(PassportData data)
            {
                return !(
                string.IsNullOrWhiteSpace(data.BirthYear)
                || string.IsNullOrWhiteSpace(data.IssueYear)
                || string.IsNullOrWhiteSpace(data.ExpirationYear)
                || string.IsNullOrWhiteSpace(data.Height)
                || string.IsNullOrWhiteSpace(data.HairColor)
                || string.IsNullOrWhiteSpace(data.EyeColor)
                || string.IsNullOrWhiteSpace(data.PassportId)
                );
            }
        }


    }
}
