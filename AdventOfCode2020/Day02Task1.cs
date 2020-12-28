using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day02Task1
    {
        public const string Separator = " ";
        public const string ValuesSeparator = "-";

        public int ValidatePasswords(string[] passwordWithPolicies)
        {
            return passwordWithPolicies.Select(ParsePasswordWithPolicy)
                                       .Count(x => x.policy.IsValid(x.password) == true);
        }

        public (PasswordPolicy policy, string password) ParsePasswordWithPolicy(string line)
        {
            if (line is null)
                throw new ArgumentNullException();

            var parts = line.Split(Separator);

            if (parts.Length != 3)
                throw new ArgumentException("Incorrect format");

            var policyValues = parts[0].Split(ValuesSeparator);

            if (policyValues.Length != 2
                || !int.TryParse(policyValues[0], out var min)
                || !int.TryParse(policyValues[1], out var max)
                )
            {
                throw new ArgumentException("Incorrect values format");
            }

            var policy = new PasswordPolicy(min, max, parts[1][0]);
            return (policy, parts[2]);
        }

        public class PasswordPolicy 
        {
            public PasswordPolicy(int minimumOccurance, int maximumOccurance, char letterToCheck)
            {
                if (minimumOccurance > maximumOccurance)
                    throw new ArgumentException("Minimum occurance cannot be higher than maximum occurance");

                MinimumOccurance = minimumOccurance;
                MaximumOccurance = maximumOccurance;
                Letter = letterToCheck;
            }

            public int MinimumOccurance { get; set; }
            public int MaximumOccurance { get; set; }
            public char Letter { get; set; }

            public bool IsValid(string password)
            {
                var count = password.Count(x => x == Letter);
                return count >= MinimumOccurance && count <= MaximumOccurance;
            }
        }
    }
}
