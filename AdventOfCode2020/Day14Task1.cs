using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day14Task1
    {
        public static readonly Regex MaskRegex = new Regex("^mask = ([0,1,X]{36})$", RegexOptions.Compiled);
        public static readonly Regex MemoryRegex = new Regex(@"^mem\[(\d+)\] = (\d+)$", RegexOptions.Compiled);

        public long Solve(string input)
        {
            var system = new System(ParseInput(input));
            system.Run();
            return system.MemorySum();
        }

        public IEnumerable<ICommand> ParseInput(string input)
        {
            foreach (var line in input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
            {
                var result = MemoryRegex.Match(line);
                if(result.Success)
                {
                    yield return new MemoryCommand(result.Groups[1].Value, result.Groups[2].Value);
                    continue;
                }
                result = MaskRegex.Match(line);
                if (result.Success)
                {
                    yield return new MaskCommand(result.Groups[1].Value);
                    continue;
                }
            }
        }

        public interface ICommand 
        {
            public void Process(System system);        
        }

        public class MaskCommand : ICommand
        {
            public MaskCommand(string mask)
            {
                Value = mask;
                Set = Convert.ToInt64(mask.Replace('X', '0'), 2);
                Reset = Convert.ToInt64(mask.Replace('X', '1'), 2);
            }

            public string Value { get; }
            public long Set { get; }
            public long Reset { get; }

            public void Process(System system)
            {
                system.Mask = this;
            }

            public long ProcessValue(long value)
            {
                return value & Reset | Set;
            }
        }

        public class MemoryCommand : ICommand
        {
            public MemoryCommand(string address, string value)
            {
                Address = long.Parse(address);
                Value = long.Parse(value);
            }

            public long Address { get; }
            public long Value { get; }

            public void Process(System system)
            {
                system.Memory[Address] = system.MaskValue(Value);
            }
        }

        public class System : ISystem
        {
            public System(IEnumerable<ICommand> commands)
            {
                Commands = commands;
                Initialize();
            }

            public void Initialize()
            {
                Memory = new Dictionary<long, long>();
            }

            public MaskCommand Mask { get; set; }
            public Dictionary<long, long> Memory { get; private set; }
            public IEnumerable<ICommand> Commands { get; }

            public long MaskValue(long value)
            {
                return Mask.ProcessValue(value);
            }

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

        public interface ISystem
        {
            IEnumerable<Day14Task1.ICommand> Commands { get; }
            public Dictionary<long, long> Memory { get; }

            long MemorySum();
            void Run();
        }
    }
}
