using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day21Task1
    {
        public (Dictionary<string, Allergen> Allergens, Dictionary<string, int> IngredientsCount) ParseInput(string input)
        {
            Dictionary<string, Allergen> allergens = new Dictionary<string, Allergen>();
            Dictionary<string, int> ingredientsCount = new Dictionary<string, int>();

            foreach(var line in input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
            {
                var ingredientsAndAllergens = line.Split("(contains ");
                var ingredients = ingredientsAndAllergens[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var aller = ingredientsAndAllergens[1].Replace(")", "").Split(", ");

                foreach(var a in aller)
                {
                    if (allergens.ContainsKey(a))
                        allergens[a].AddIngredients(ingredients);
                    else
                        allergens[a] = new Allergen(a, ingredients);
                }

                foreach(var i in ingredients)
                {
                    if (ingredientsCount.ContainsKey(i))
                        ingredientsCount[i]++;
                    else
                        ingredientsCount[i] = 1;
                }
            }

            return (allergens, ingredientsCount);
        }

        public class Allergen
        {
            public Allergen(string name, string[] ingredients)
            {
                Name = name;
                AddIngredients(ingredients);
            }

            public string Name { get; set; }
            public Dictionary<string, int> IngredientsCount { get; } = new Dictionary<string, int>();
            public int Occurances { get; set; }
            public string Ingredient { get; set; }

            public void AddIngredients(string[] ingredients)
            {
                Occurances++;
                foreach(var ing in ingredients)
                {
                    if (IngredientsCount.ContainsKey(ing))
                        IngredientsCount[ing] += 1;
                    else
                        IngredientsCount[ing] = 1;
                }
            }
        }

        public int Solve(string input)
        {
            var (allergens, ingredientsCount) = ParseInput(input);
            HashSet<string> assignedIngredients = AssignIngredients(allergens);

            return ingredientsCount.Where(x => !assignedIngredients.Contains(x.Key)).Sum(x => x.Value);
        }

        public HashSet<string> AssignIngredients(Dictionary<string, Allergen> allergens)
        {
            var copyOfAllergens = new Dictionary<string, Allergen>(allergens);
            var assignedIngredients = new HashSet<string>();
            while (copyOfAllergens.Any())
            {
                var copy = new Dictionary<string, Allergen>(copyOfAllergens);
                foreach (var all in copy.Values.Where(x => x.Ingredient == null).ToArray())
                {
                    var countOfMaxOccurances = all.IngredientsCount.Count(x => x.Value == all.Occurances && !assignedIngredients.Contains(x.Key));
                    if (countOfMaxOccurances == 1)
                    {
                        all.Ingredient = all.IngredientsCount.First(x => x.Value == all.Occurances && !assignedIngredients.Contains(x.Key)).Key;
                        assignedIngredients.Add(all.Ingredient);
                        copyOfAllergens.Remove(all.Name);
                    }
                }
            }

            return assignedIngredients;
        }
    }
}
