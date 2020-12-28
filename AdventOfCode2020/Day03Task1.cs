using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day03Task1
    {
        public const char Tree = '#';

        public int Count(string input, int right, int down)
        {
            var lines = input.Split(Environment.NewLine);

            var tileWidth = lines[0].Length;
            var tileHeight = lines.Length;

            var trees = 0;
            var currentWidth = 0;

            for (var height = 0; height < tileHeight; height += down)
            {
                var currentField = lines[height][currentWidth % tileWidth];
                var isTree = currentField == Tree;
                trees += isTree ? 1 : 0;

                currentWidth += right;
            }

            return trees;
        }
    }
}
