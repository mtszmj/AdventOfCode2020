using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day08Task2 : Day08Task1
    {
        public override int Solve(string[] input)
        {
            var originalOperations = ParseInput(input).ToList();
            int indexOfOperationToChange = 0;
            var operations = originalOperations.ToList();

            while(indexOfOperationToChange < operations.Count)
            {
                if (IsProgramValid(operations, out int? accumulatorValue))
                    return accumulatorValue.Value;

                operations = ChangeProgram(originalOperations, ref indexOfOperationToChange);
            }

            throw new InvalidOperationException("No solution found");
        }

        bool IsProgramValid(List<Operation> operations, out int? accumulatorValue)
        {
            var state = State.InitializationState();
            var nextOperation = operations[state.NextLine];

            while (!nextOperation.WasExecuted)
            {
                state = nextOperation.Handle(state);

                if (FinishedProgramCorrectly(state, operations))
                {
                    accumulatorValue = state.Accumulator;
                    return true;
                }

                nextOperation = operations[state.NextLine];
            }

            accumulatorValue = null;
            return false;

            static bool FinishedProgramCorrectly(State state, List<Operation> operations)
            {
                return state.NextLine == operations.Count;
            }
        }

        List<Operation> ChangeProgram(List<Operation> originalOperations, ref int indexOfOperationToChange)
        {
            while (originalOperations[indexOfOperationToChange] is Acc)
                indexOfOperationToChange++;

            var operations = originalOperations.ToList();
            operations.ForEach(x => x.Reset());
            if (indexOfOperationToChange < operations.Count
                && originalOperations[indexOfOperationToChange] is Nop nop
                )
                operations[indexOfOperationToChange] = new Jmp(nop.Value);
            else if (indexOfOperationToChange < operations.Count
                    && originalOperations[indexOfOperationToChange] is Jmp jmp
                    )
                operations[indexOfOperationToChange] = new Nop(jmp.Value);

            indexOfOperationToChange++;

            return operations;
        }
    }
}
