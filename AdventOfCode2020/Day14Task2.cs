using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day14Task2
    {
        public static readonly Regex MaskRegex = new Regex("^mask = ([0,1,X]{36})$", RegexOptions.Compiled);
        public static readonly Regex MemoryRegex = new Regex(@"^mem\[(\d+)\] = (\d+)$", RegexOptions.Compiled);

        public System2 System { get; private set; }
        
        public long Solve(string input)
        {
            var system = new System2(ParseInput(input));
            system.Run();
            System = system;
            return system.MemorySum();
        }

        public IEnumerable<ICommand2> ParseInput(string input)
        {
            foreach (var line in input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
            {
                var result = MemoryRegex.Match(line);
                if (result.Success)
                {
                    yield return new Memory2Command(result.Groups[1].Value, result.Groups[2].Value);
                    continue;
                }
                result = MaskRegex.Match(line);
                if (result.Success)
                {
                    yield return new AddressMaskCommand(result.Groups[1].Value);
                    continue;
                }
            }
        }

        public class AddressMaskCommand : ICommand2
        {
            public AddressMaskCommand(string value)
            {
                MaskValue = value;
                Set = Convert.ToInt64(value.Replace('X', '0'), 2);
            }

            public string MaskValue { get; }
            public long Set { get; }

            public HashSet<long> Values { get; private set; }

            public HashSet<long> ProcessAddress(long value)
            {
                Values = new HashSet<long>();

                value |= Set;

                if (!MaskValue.Contains('X'))
                    Values.Add(value);
                else
                    Combine(MaskValue, value, 0);

                return Values;
            }

            public void Combine(string mask, long value, int index)
            {
                var ready = true;
                for(var i = index; i < mask.Length; i++)
                {
                    if (mask[i] == 'X')
                    {

                        ready = false;
                        var zeroVal = value & ~(1L << mask.Length - i - 1);
                        var oneVal = value | (1L << (mask.Length - i - 1));
                        //Console.WriteLine($"{mask}\n{i}\n{zeroVal}\n{oneVal}");
                        Combine(mask, zeroVal, i+1); //0
                        Combine(mask, oneVal, i+1); //1
                    }
                }

                if (ready)
                {
                    Values.Add(value);
                }
            }

            public void Process(System2 system)
            {
                system.Mask = this;
            }
        }

        public class Memory2Command : ICommand2
        {
            public Memory2Command(string address, string value)
            {
                Address = long.Parse(address);
                Value = long.Parse(value);
            }

            public long Address { get; }
            public long Value { get; }

            public void Process(System2 system)
            {
                foreach (var address in system.Mask.ProcessAddress(Address))
                {
                    //Console.WriteLine($"mem[{address}] = {Value}");
                    system.Memory[address] = Value;
                }
            }
        }

        public class System2
        {
            public System2(IEnumerable<ICommand2> commands)
            {
                Commands = commands;
                Initialize();
            }

            public void Initialize()
            {
                Memory = new Dictionary<long, long>();
            }

            public AddressMaskCommand Mask { get; set; }
            public Dictionary<long, long> Memory { get; private set; }
            public IEnumerable<ICommand2> Commands { get; }

            public void Run()
            {
                foreach (var cmd in Commands)
                {
                    cmd.Process(this);
                }
            }

            public long MemorySum()
            {
                return Memory.Values.Sum();
            }
        }


        public interface ICommand2
        {
            public void Process(System2 system);
        }
    }
}
