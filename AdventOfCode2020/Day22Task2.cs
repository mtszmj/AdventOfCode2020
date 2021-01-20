using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day22Task2
    {
        Day22Task1 task1 = new Day22Task1();

        public long Solve(string input)
        {
            var (p1, p2) = task1.Parse(input);
            Play(p1, p2, 1);

            return p1.Count > p2.Count ? task1.CalculateResult(p1) : task1.CalculateResult(p2);
        }

        public int Play(Queue<int> p1, Queue<int> p2, int game)
        {
            Console.WriteLine($"=== GAME {game} ===");
            var round = 0;
            var winners = new List<int>();
            var previousHands = new HashSet<(string, string)>();
            while (p1.Any() && p2.Any())
            {
                var currentHands = (string.Join(",", p1), string.Join(",", p2));
                if (previousHands.Contains(currentHands))
                    return 1;
                previousHands.Add(currentHands);

                PlayOneRound(p1, p2, ref round, winners, game);
            }

            return p1.Any() ? 1 : 2;
        }

        public void PlayOneRound(Queue<int> p1, Queue<int> p2, ref int round, List<int> winners, int game)
        {
            round++;
            var p1Card = p1.Dequeue();
            var p2Card = p2.Dequeue();
            var hasToPlaySubgame = p1.Count >= p1Card && p2.Count >= p2Card;
            var subgameWinner = 0;

            if (hasToPlaySubgame)
            {
                Console.WriteLine($"Playing a subgame ({game}->{game+1}) at round {round}: p1={p1Card}, p2={p2Card}");
                var p1Copy = new Queue<int>(p1.Take(p1Card));
                var p2Copy = new Queue<int>(p2.Take(p2Card));
                subgameWinner = Play(p1Copy, p2Copy, game + 1);
            }
            if (hasToPlaySubgame && subgameWinner == 1 || (!hasToPlaySubgame && p1Card > p2Card))
            {
                p1.Enqueue(p1Card);
                p1.Enqueue(p2Card);
                winners.Add(1);
            }
            else if (hasToPlaySubgame && subgameWinner == 2 || (!hasToPlaySubgame && p1Card < p2Card))
            {
                p2.Enqueue(p2Card);
                p2.Enqueue(p1Card);
                winners.Add(2);
            }
            else
            {
                throw new ArgumentException($"Unexpected: Draw in round {round}!");
            }
        }
    }
}
