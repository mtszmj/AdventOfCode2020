using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day10Task1
    {
        public long Solve(string input)
        {
            return default;
        }

        public long FindDeviceVoltage(long[] adapterJolts)
        {
            return adapterJolts.Max() + 3;
        }

        public long[] OrderAdaptersWithDevice(long[] adapterJolts)
        {
            return adapterJolts.OrderBy(x => x).Append(FindDeviceVoltage(adapterJolts)).ToArray();
        }

        public Dictionary<long,int> CountDifferences(long[] adapterJolts)
        {
            var jolts = OrderAdaptersWithDevice(adapterJolts);
            var last = 0L;
            var joltsDifferencesOccurance = new Dictionary<long, int>();
            for(int i = 0; i < jolts.Length; i++)
            {
                var difference = jolts[i] - last;
                last = jolts[i];
                if (joltsDifferencesOccurance.ContainsKey(difference))
                    joltsDifferencesOccurance[difference]++;
                else joltsDifferencesOccurance[difference] = 1;
            }

            return joltsDifferencesOccurance;
        }

        public long MultiplyDifferencesOf1And3JoltsOccurances(long[] adapterJolts)
        {
            var occurances = CountDifferences(adapterJolts);

            return occurances[1] * occurances[3];
        }
    }
}
