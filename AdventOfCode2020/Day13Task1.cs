using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day13Task1
    {
        public (int EarliestTime, List<int> Buses) Parse(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var earliestTime = int.Parse(lines[0]);
            var buses = lines[1].Split(",").Where(x => x != "x").Select(x => int.Parse(x)).ToList();
            return (earliestTime, buses);
        }

        public Dictionary<int, int> FindDepartures(int earliestTime, List<int> buses)
        {
            var departures = new Dictionary<int, int>();
            foreach(var bus in buses)
            {
                var multiplier = earliestTime / bus;
                departures[bus] = bus * multiplier < earliestTime ? bus * (multiplier + 1) : bus * multiplier;
            }

            return departures;
        }

        public int Solve(string input)
        {
            var data = Parse(input);
            var departures = FindDepartures(data.EarliestTime, data.Buses);

            var kv = departures.Aggregate(new KeyValuePair<int, int>(-1, int.MaxValue), 
                (acc, current) => acc.Value <= current.Value ? acc : current);

            return (kv.Value - data.EarliestTime) * kv.Key;
        }
    }
}
