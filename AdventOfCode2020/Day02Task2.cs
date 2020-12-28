using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day02Task2 : Day02Task1
    {
        protected override IPasswordPolicy CreatePolicy(int value1, int value2, char letter)
        {
            return new PasswordPositionsPolicy(value1, value2, letter);
        }

        public class PasswordPositionsPolicy : IPasswordPolicy
        {
            public PasswordPositionsPolicy(int firstPosition, int secondPosition, char letterToCheck)
            {
                FirstPosition = firstPosition;
                SecondPosition = secondPosition;
                Letter = letterToCheck;
            }

            public int FirstPosition { get; set; }
            public int SecondPosition { get; set; }
            public char Letter { get; set; }

            public bool IsValid(string password)
            {
                return IsFirstGivenLetterAndSecondNot(password)
                    || IsSecondGivenLetterAndFirstNot(password);
            }
            
            private bool IsFirstGivenLetterAndSecondNot(string password)
            {
                return password[FirstPosition - 1] == Letter && password[SecondPosition - 1] != Letter;
            }

            private bool IsSecondGivenLetterAndFirstNot(string password)
            {
                return password[FirstPosition - 1] != Letter && password[SecondPosition - 1] == Letter;
            }

        }
    }
}
