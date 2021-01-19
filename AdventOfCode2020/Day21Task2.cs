using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day21Task2
    {
        Day21Task1 task1 = new Day21Task1();

        public string Solve(string input)
        {
            var (allergens, ingredientsCount) = task1.ParseInput(input);
            HashSet<string> assignedIngredients = task1.AssignIngredients(allergens);

            return string.Join(",", 
                allergens.Where(x => assignedIngredients.Contains(x.Value.Ingredient))
                         .OrderBy(x => x.Key)
                         .Select(x => x.Value.Ingredient)
                );
        }
    }
}
