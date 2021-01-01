using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day10Task2
    {
        Day10Task1 task1 = new Day10Task1();

        public long Test(long[] adapterJolts)
        {
            var ordered = task1.OrderAdaptersWithDevice(adapterJolts);
            var mustHaves = GetMustHaves(ordered);
            var others = ordered.Except(mustHaves).OrderBy(x => x).ToList();
            var ranges = CreateRanges(mustHaves, others);
            ranges.ForEach(x => x.CountPossibilities());
            return ranges.Aggregate(1L, (acc, current) => acc * current.NumberOfPossibilities.Value);
        }

        public List<Range> CreateRanges(List<long> mustHaves, List<long> others)
        {
            var ranges = new List<Range>();
            var othersIndex = 0;
            for (int mh = 0; mh < mustHaves.Count - 1; mh++)
            {
                var begin = mustHaves[mh];
                var end = mustHaves[mh + 1];
                var possibleValuesInBetween = new List<long>();
                while (othersIndex < others.Count && others[othersIndex] < end)
                {
                    var value = others[othersIndex++];
                    if (value > begin && value < end)
                        possibleValuesInBetween.Add(value);
                }

                if (possibleValuesInBetween.Any())
                    ranges.Add(new Range { Begin = begin, End = end, PossibleValuesInBetween = possibleValuesInBetween });
            }
            return ranges;
        }

        /// <summary>
        /// Whenever there is a difference of 3 between two consecutive values - both must be used in solution
        /// </summary>
        /// <param name="orderedAdapterJolts"></param>
        /// <returns></returns>
        public List<long> GetMustHaves (long[] orderedAdapterJolts)
        {
            var last = 0L;
            var mustHaves = new HashSet<long>() { 0 };
            var others = new HashSet<long>();
            for (int i = 0; i < orderedAdapterJolts.Length; i++)
            {
                var difference = orderedAdapterJolts[i] - last;
                if (difference == 3)
                {
                    mustHaves.Add(last);
                    mustHaves.Add(orderedAdapterJolts[i]);
                }
                last = orderedAdapterJolts[i];
            }

            return mustHaves.OrderBy(x => x).ToList();
        }

        public class Range
        {
            public long Begin { get; set; }
            public long End { get; set; }
            public List<long> PossibleValuesInBetween { get; set; }
            public long? NumberOfPossibilities { get; set; }

            public long CountPossibilities()
            {
                TreeNode Root = new TreeNode()
                {
                    Value = Begin,
                    Head = new List<long>(),
                    Tail = PossibleValuesInBetween.Concat(new long[] { End }).ToList()
                };

                Root.CreateChildren();
                NumberOfPossibilities = Root.CountLeaves();
                return NumberOfPossibilities.Value;
            }
        }

        /// <summary>
        /// First try - working with small sets, but unable to solve big sets in reasonable time and memory
        /// </summary>
        /// <param name="adapterJolts"></param>
        /// <returns></returns>
        public long Test_bad_performance_impossible_solution_for_big_data(long[] adapterJolts)
        {
            var sorted = task1.OrderAdaptersWithDevice(adapterJolts);

            TreeNode Root = new TreeNode()
            {
                Value = 0,
                Head = new List<long>(),
                Tail = new List<long>(sorted)
            };

            Root.CreateChildren();
            return Root.CountLeaves();
        }

        public class TreeNode
        {
            public long Value { get; set; }
            public List<long> Head { get; set; }
            public List<long> Tail { get; set; }

            public List<TreeNode> Children { get; set; } = new List<TreeNode>();

            public void CreateChildren()
            {
                var valuesInRange3 = new List<long>();
                for(int i = 0; i < 3 && i < Tail.Count; i++)
                {
                    if (Tail[i] - 3 <= Value)
                        valuesInRange3.Add(Tail[i]);
                    else break;
                }

                if (valuesInRange3.Count > 1)
                {
                    foreach (var value in valuesInRange3)
                        Children.Add(CreateChild(value));
                }
                else if(valuesInRange3.Count == 1) Children.Add(CreateChild(valuesInRange3[0]));

                foreach (var child in Children)
                    child.CreateChildren();
            }

            private TreeNode CreateChild(long nodeValue)
            {
                var node = new TreeNode();
                node.Head = new List<long>(Head);
                node.Head.Add(Value);
                node.Value = nodeValue;
                node.Tail = new List<long>(Tail);
                for(int i = 0; i < 3 && i < node.Tail.Count;)
                {
                    if (node.Tail[i] <= nodeValue)
                        node.Tail.Remove(node.Tail[i]);
                    else i++;
                }
                return node;
            }

            public long CountLeaves()
            {
                var result = 0L;
                if (!Children.Any() && Tail.Count == 0)
                {
                    Console.WriteLine($"{string.Join(",", Head)},{Value}");
                    return 1;
                }
                else
                {
                    foreach (var child in Children)
                        result += child.CountLeaves();
                }

                return result;
            }
        }
    }
}
