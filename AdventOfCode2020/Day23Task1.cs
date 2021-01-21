using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day23Task1
    {
        public string Solve(string input)
        {
            var circle = CreateCircle(input);

            for(var i = 0; i < 100; i++)
            {
                circle.PlayRound();
            }

            var current = circle.Cups[1];
            var result = "";
            while(current.Next.Id != 1)
            {
                result += current.Next.ToString();
                current = current.Next;
            }

            return result;
        }

        private Circle CreateCircle(string input)
        {
            var cups = new List<Cup>();
            Cup cup = null;
            for (var i = 0; i < input.Length; i++)
            {
                cup = new Cup() { Id = int.Parse($"{input[i]}") };
                cups.Add(cup);
                if (i > 0)
                {
                    cups[i - 1].Next = cup;
                    cup.Previous = cups[i - 1];
                }
            }
            cup.Next = cups[0];
            cups[0].Previous = cup;

            var circle = new Circle()
            {
                Cups = cups.ToDictionary(x => x.Id, x => x),
                Current = cups[0]
            };
            
            return circle;
        }

        public class Circle 
        {
            private Dictionary<int, Cup> _cups;
            public Dictionary<int, Cup> Cups
            {
                get => _cups; 
                set
                {
                    _cups = value;
                    Max = value.Max(x => x.Key);
                }
            }
            public Cup Current { get; set; }
            public int Max { get; set; }

            public void PlayRound()
            {
                Console.WriteLine("--- MOVE ---");
                var current = Current;
                do
                {
                    Console.Write($"{current} ");
                    current = current.Next;
                }
                while (current != Current);
                Console.WriteLine();
                var pickUp = Current.RemoveCups(3);

                Console.WriteLine($"Pick up: {string.Join(",", pickUp)}");
                
                int destination = SelectDestination(pickUp);
                Console.WriteLine($"Destination: {destination}");
                Cups[destination].InsertCups(pickUp);

                Current = Current.Next;

                Console.WriteLine();
            }

            private int SelectDestination(List<Cup> pickUp)
            {
                var pickedUpIds = pickUp.Select(x => x.Id).ToHashSet();
                var currentId = Current.Id;
                while (pickedUpIds.Contains(currentId) || Current.Id == currentId)
                {
                    currentId--;
                    if (currentId <= 0)
                        currentId = Max;
                }

                return currentId;
            }
        }

        public class Cup
        {
            public int Id { get; set; }
            public Cup Previous { get; set; }
            public Cup Next { get; set; }

            public void InsertCups(IEnumerable<Cup> cups)
            {
                var next = Next;
                var current = this;
                foreach(var cup in cups)
                {
                    current.Next = cup;
                    current = cup;
                }

                current.Next = next;
                next.Previous = current;
            }

            public List<Cup> RemoveCups(int count)
            {
                var removed = new List<Cup>();
                var current = Next;
                for(var i = 0; i < count; i++)
                {
                    var next = current.Next;
                    removed.Add(current);
                    current.Previous = null;
                    current.Next = null;
                    current = next;
                }
                this.Next = current;
                current.Previous = this;

                return removed;
            }

            public void Remove()
            {
                Previous = null;
                Next = null;
            }

            public override string ToString()
            {
                return $"{Id}";
            }
        }
    }
}
