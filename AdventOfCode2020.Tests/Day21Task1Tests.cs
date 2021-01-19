using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Tests
{
    [TestFixture]
    public class Day21Task1Tests
    {
        Day21Task1 Solver() => new Day21Task1();

        [Test]
        public void parses_input()
        {
            var input = @"mxmxvkd kfcds sqjhc nhms (contains dairy, fish)
trh fvjkl sbzzf mxmxvkd (contains dairy)
sqjhc fvjkl (contains soy)
sqjhc mxmxvkd sbzzf (contains fish)";

            var solver = Solver();
            var (allergens, ingredientsCount) = solver.ParseInput(input);

            allergens.Should().HaveCount(3);
            allergens.Should().ContainKeys("dairy", "fish", "soy");
            allergens["dairy"].IngredientsCount.Should().HaveCount(7);
            allergens["dairy"].IngredientsCount.Should().ContainKeys("mxmxvkd", "kfcds", "sqjhc", "nhms", "trh", "fvjkl", "sbzzf");
            allergens["dairy"].IngredientsCount["mxmxvkd"].Should().Be(2);
            allergens["dairy"].IngredientsCount["kfcds"].Should().Be(1);
            allergens["dairy"].IngredientsCount["sqjhc"].Should().Be(1);
            allergens["dairy"].IngredientsCount["nhms"].Should().Be(1);
            allergens["dairy"].IngredientsCount["trh"].Should().Be(1);
            allergens["dairy"].IngredientsCount["fvjkl"].Should().Be(1);
            allergens["dairy"].IngredientsCount["sbzzf"].Should().Be(1);
            allergens["dairy"].Occurances.Should().Be(2);

            allergens["fish"].IngredientsCount.Should().HaveCount(5);
            allergens["fish"].IngredientsCount.Should().ContainKeys("mxmxvkd", "kfcds", "sqjhc", "nhms", "sbzzf");
            allergens["fish"].IngredientsCount["mxmxvkd"].Should().Be(2);
            allergens["fish"].IngredientsCount["kfcds"].Should().Be(1);
            allergens["fish"].IngredientsCount["sqjhc"].Should().Be(2);
            allergens["fish"].IngredientsCount["nhms"].Should().Be(1);
            allergens["fish"].IngredientsCount["sbzzf"].Should().Be(1);
            allergens["fish"].Occurances.Should().Be(2);

            allergens["soy"].IngredientsCount.Should().HaveCount(2);
            allergens["soy"].IngredientsCount.Should().ContainKeys("sqjhc", "fvjkl");
            allergens["soy"].IngredientsCount["sqjhc"].Should().Be(1);
            allergens["soy"].IngredientsCount["fvjkl"].Should().Be(1);
            allergens["soy"].Occurances.Should().Be(1);

            ingredientsCount["mxmxvkd"].Should().Be(3);
            ingredientsCount["kfcds"].Should().Be(1);
            ingredientsCount["sqjhc"].Should().Be(3);
            ingredientsCount["nhms"].Should().Be(1);
            ingredientsCount["trh"].Should().Be(1);
            ingredientsCount["fvjkl"].Should().Be(2);
            ingredientsCount["sbzzf"].Should().Be(2);
        }

        [Test]
        public void solves_example()
        {
            var input = @"mxmxvkd kfcds sqjhc nhms (contains dairy, fish)
trh fvjkl sbzzf mxmxvkd (contains dairy)
sqjhc fvjkl (contains soy)
sqjhc mxmxvkd sbzzf (contains fish)";

            var solver = Solver();
            var result = solver.Solve(input);

            result.Should().Be(5);
        }

        [Test]
        public void solves_input()
        {
            var input = File.ReadAllText("Files\\Day21.txt");
            var solver = Solver();
            var result = solver.Solve(input);

            result.Should().Be(2282);
        }
    }
}
