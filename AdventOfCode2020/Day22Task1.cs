using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day22Task1
    {
        public long Solve(string input)
        {
            var (p1, p2) = Parse(input);
            Play(p1, p2);

            return p1.Count > p2.Count ? CalculateResult(p1) : CalculateResult(p2);
        }

        public long CalculateResult(IReadOnlyCollection<int> player)
        {
            var count = player.Count;
            return player.Aggregate(0L, (result, current) => result += current * count--);
        }

        public void Play(Queue<int> p1, Queue<int> p2)
        {
            var round = 0;
            var winners = new List<int>();
            while (p1.Any() && p2.Any())
            {
                PlayOneRound(p1, p2, ref round, winners);
            }
        }

        public void PlayOneRound(Queue<int> p1, Queue<int> p2, ref int round, List<int> winners)
        {
            round++;
            var p1Card = p1.Dequeue();
            var p2Card = p2.Dequeue();

            if (p1Card > p2Card)
            {
                p1.Enqueue(p1Card);
                p1.Enqueue(p2Card);
                winners.Add(1);
            }
            else if (p1Card < p2Card)
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

        public (Queue<int> Player1, Queue<int> Player2) Parse(string input)
        {
            var players = input.Split($"{Environment.NewLine}{Environment.NewLine}", StringSplitOptions.RemoveEmptyEntries);

            var player1 = new Queue<int>(players[0].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse));
            var player2 = new Queue<int>(players[1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse));

            return (player1, player2);
        }
    }
}
