using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day08Task1
    {
        public int Solve(string[] input)
        {
            var state = new State(0, 0);
            var operations = ParseInput(input).ToList();

            var nextOp = operations[state.NextLine];
            while(!nextOp.WasExecuted)
            {
                state = nextOp.Handle(state);
                nextOp = operations[state.NextLine];
            }

            return state.Accumulator;
        }

        public IEnumerable<Operation> ParseInput(string[] input)
        {
            foreach(var line in input.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                var lineParts = line.Trim().Split(" ");
                var value = int.Parse(lineParts[1]);
                yield return lineParts[0] switch
                {
                    "nop" => new Nop(),
                    "acc" => new Acc(value),
                    "jmp" => new Jmp(value),
                    _ => throw new ArgumentOutOfRangeException(paramName: null, message: $"Out of range: {lineParts[0]}")
                };
            }
        }

        public class State
        {
            public State(int acc, int nextLine)
            {
                Accumulator = acc;
                NextLine = nextLine;
            }

            public int Accumulator { get; }
            public int NextLine { get; }
        }

        public abstract class Operation
        {
            public bool WasExecuted { get; private set; }
            public State Handle(State state)
            {
                WasExecuted = true;
                return HandleStateChange(state);
            }

            protected abstract State HandleStateChange(State state);
        }

        public class Nop : Operation
        {
            protected override State HandleStateChange(State state)
            {
                return new State(state.Accumulator, state.NextLine + 1);
            }
        }

        public class Acc : Operation
        {
            public Acc(int value)
            {
                Value = value;
            }

            public int Value { get; }

            protected override State HandleStateChange(State state)
            {
                return new State(state.Accumulator + Value, state.NextLine + 1);
            }
        }

        public class Jmp : Operation
        {
            public Jmp(int offset)
            {
                Offset = offset;
            }

            public int Offset { get; }

            protected override State HandleStateChange(State state)
            {
                return new State(state.Accumulator, state.NextLine + Offset);
            }
        }
    }
}
