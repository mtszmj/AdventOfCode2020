using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day13Task2
    {
        public long Solve(string input, long startFrom = 0)
        {
            var data = ParseInput(input);
            return SolveV4(data.Bus0, data.Offsets, startFrom);
        }

        public long SolveV4(long bus0, Dictionary<long, long> offsets, long startFrom = 0)
        {
            var buses = ParseToObjects(bus0, offsets)
                .ToList(); // start from highest id - maximum jumps in one iteration

            int busChecked = 1;
            var t = 0L;
            var first = -1L;
            var add = buses[0].Id;
            while(true)
            {
                Console.Write($"{t}");
                if((t + buses[busChecked].Offset) % buses[busChecked].Id == 0)
                {
                    if(busChecked == buses.Count - 1)
                    {
                        return t;
                    }
                    if (first == -1L)
                    {
                        Console.Write($" F({busChecked})");
                        first = t;
                    }
                    else
                    {
                        add = t - first;
                        first = -1L;
                        busChecked++;
                        Console.Write($" S({busChecked - 1} - {add})");
                    }
                }

                t += add;
                Console.WriteLine();
            }
        }

        public long SolveV3(long bus0, Dictionary<long, long> offsets, long startFrom = 0)
        {
            var sizeAtOnce = 10000000L;

            var buses = ParseToObjects(bus0, offsets)
                .OrderByDescending(x => x.Id)
                .ToList(); // start from highest id - maximum jumps in one iteration

            Console.WriteLine(string.Join(",", buses.Select(x => x.Id)));

            var countFrom = buses.Select(x => /*(startFrom / x.Id * x.Id)*/ - x.Offset).ToArray();

            for(long retries = 0; retries < 30 || true; retries++)
            {
                var array = new bool[buses.Count, sizeAtOnce];
                long startLoopFrom = retries * sizeAtOnce;
                for (var i = 0; i < buses.Count; i++)
                {
                    long x = countFrom[i];
                    while (x < sizeAtOnce + startLoopFrom)
                    {
                        if (x >= startLoopFrom)
                        {
                            array[i, x - startLoopFrom] = true;
                            countFrom[i] = x;
                        }
                        x += buses[i].Id;
                    }
                }

                for (var i = 0 ; i < sizeAtOnce; i++)
                {
                    Console.Write($"{startLoopFrom + i}: ");
                    for (var j = 0; j < buses.Count; j++)
                    {
                        string print = array[j, i] ? "X" : ".";
                        Console.Write($"{print} ");
                    }
                    Console.WriteLine();
                }

                for (var i = 0; i < sizeAtOnce; i++)
                {
                    var all = true;
                    for (var j = 0; j < buses.Count; j++)
                    {
                        if (!array[j, i])
                        {
                            all = false;
                            break;
                        }
                    }
                    if (all)
                        return i + startLoopFrom;
                }
            }
            

            return -1;
        }

        public (long Bus0, Dictionary<long, long> Offsets) ParseInput(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var index = 0;
            var bus0 = 0L;
            var offsets = new Dictionary<long, long>();
            foreach (var value in lines[1].Split(",", StringSplitOptions.RemoveEmptyEntries))
            {
                if (index == 0)
                    bus0 = long.Parse(value);
                else if (value == "x")
                { }
                else
                    offsets[index] = long.Parse(value);

                index++;

            }
            return (bus0, offsets);
        }

        public List<Bus> ParseToObjects(long bus0, Dictionary<long, long> offsets)
        {
            var buses = new List<Bus> { new Bus { Id = bus0, Offset = 0 } };
            buses.AddRange(offsets.Select(x => new Bus { Id = x.Value, Offset = x.Key }));
            return buses;
        }

        public class Bus
        {
            public long Id { get; set; }
            public long Offset { get; set; }
        }

        public long SolveV2(long bus0, Dictionary<long, long> offsets, long startFrom = 0)
        {
            var buses = ParseToObjects(bus0, offsets)
                .OrderByDescending(x => x.Id)
                .ToList(); // start from highest id - maximum jumps in one iteration

            var time = -buses[0].Offset;
            long startFromMultiplier = startFrom / buses[0].Id;
            time += startFromMultiplier * buses[0].Id;

            for (long i = 1; i < 100000000000000; i++)
            {
                time += buses[0].Id;
                var correct = true;
                foreach (var bus in buses)
                {
                    if ((time + bus.Offset) % bus.Id != 0)
                    {
                        correct = false;
                        break;
                    }
                }

                if (correct)
                {
                    return time;
                }
            }

            throw new InvalidOperationException("No result found");
        }

        public long SolveV1(long bus0, Dictionary<long, long> offsets)
        {
            for (long i = 1; i < 100000000000000; i++)
            {
                var t = bus0 * i;
                var correct = true;
                foreach (var kv in offsets)
                {
                    if ((t + kv.Key) % kv.Value != 0)
                    {
                        correct = false;
                        break;
                    }
                }

                if (correct)
                {
                    return t;
                }
            }

            throw new InvalidOperationException("No result found");
        }
    }
}
