using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day18Task1
    {
        private static readonly char[] Digits = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        const char Space = ' ';
        const char Multiplication = '*';
        const char Addition = '+';
        const char OpenBracket = '(';
        const char CloseBracket = ')';

        public long Solve(string input)
        {
            return input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => SolveOne(x)).Sum();
        }

        public long SolveOne(string input)
        {
            if (input.Last() != '\n' && input.Last() != Space)
                input += Space;

            var data = new Queue<Object>();
            var currentValue = "";
            var index = 0;
            var count = input.Length;
            var brackets = 0;
            var insideBrackets = "";
            foreach(var c in input)
            {
                index++;

                if(c == OpenBracket)
                {
                    brackets++;
                    insideBrackets += c;
                }
                else if (c == CloseBracket)
                {
                    insideBrackets += c;
                    if(--brackets == 0)
                    {
                        var inner = SolveOne(insideBrackets.Substring(1, insideBrackets.Length - 2));
                        data.Enqueue(new Value { Val = inner});
                        insideBrackets = "";
                    }
                }
                else if (brackets > 0)
                {
                    insideBrackets += c;
                }
                else if (Digits.Contains(c))
                {
                    currentValue += c;
                }
                else if ((c == Space) && currentValue != "")
                {
                    data.Enqueue(new Value { Val = Convert.ToInt64(currentValue) });
                    currentValue = "";
                }
                else if (c == Addition)
                {
                    data.Enqueue(Add.Instance);
                }
                else if (c == Multiplication)
                {
                    data.Enqueue(Multiply.Instance);
                }
            }

            var result = ((Value)data.Dequeue()).Val;
            IOperation operation = null;
            foreach(var current in data)
            {
                if (current is IOperation oper)
                    operation = oper;
                else if (current is Value val)
                    result = operation.Perform(result, val.Val);
            }

            return result;
        }


        public interface IOperation
        {
            long Perform(long left, long right);
        }

        public class Multiply : IOperation
        {
            public static readonly Multiply Instance = new Multiply();
            public long Perform(long left, long right) => left * right;
        }

        public class Add : IOperation
        {
            public static readonly Add Instance = new Add();
            public long Perform(long left, long right) => left + right;
        }

        public class Value
        {
            public long Val { get; set; }
        }
    }
}
