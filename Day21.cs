using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode
{
    public static class Day21
    {
        public static void Problem1()
        {
            bool useTestData = false;
            var recipes = Utilities.ReadStrings(21, useTestData).Select(i => new Recipe(i)).ToList();

            var allAllergens = new List<string>();
            var allIngredients = new List<string>();

            recipes.ForEach(r =>
            {
                allAllergens.AddRange(r.Allergens);
                allIngredients.AddRange(r.Ingredients);
            });

            var ingredients = allIngredients.Distinct().ToList();
            List<string> hypoallergenicIngredients = new List<string>(ingredients);

            foreach(var allergen in allAllergens.Distinct())
            {
                var potentialAllergens = new List<string>(ingredients);
                foreach (var recipe in recipes.Where(r => r.Allergens.Contains(allergen)))
                {
                    potentialAllergens = potentialAllergens.Intersect(recipe.Ingredients).ToList();
                }
                hypoallergenicIngredients = hypoallergenicIngredients.Where(i => !potentialAllergens.Contains(i)).ToList();
            }

            Console.WriteLine(allIngredients.Where(i => hypoallergenicIngredients.Contains(i)).Count());
        }

        public static void Problem2()
        {
            bool useTestData = false;
            var recipes = Utilities.ReadStrings(21, useTestData).Select(i => new Recipe(i)).ToList();

            var allAllergens = new List<string>();
            var allIngredients = new List<string>();

            recipes.ForEach(r =>
            {
                allAllergens.AddRange(r.Allergens);
                allIngredients.AddRange(r.Ingredients);
            });

            var ingredients = allIngredients.Distinct().ToList();
            List<string> hypoallergenicIngredients = new List<string>(ingredients);

            var allergens = new List<Allergen>();
            foreach (var allergen in allAllergens.Distinct())
            {

                var potentialAllergens = new List<string>(ingredients);
                foreach (var recipe in recipes.Where(r => r.Allergens.Contains(allergen)))
                {
                    potentialAllergens = potentialAllergens.Intersect(recipe.Ingredients).ToList();
                }
                hypoallergenicIngredients = hypoallergenicIngredients.Where(i => !potentialAllergens.Contains(i)).ToList();
                allergens.Add(new Allergen { Name = allergen, PotentialAllergenicIngredients = potentialAllergens });
            }

            while (allergens.Any(a => a.PotentialAllergenicIngredients.Count() > 1))
            {
                foreach(var foundAllergen in allergens.Where(a => a.PotentialAllergenicIngredients.Count() == 1))
                {
                    var ingredient = foundAllergen.PotentialAllergenicIngredients.First();
                    foreach (var excludedAllergen in allergens.Where(a => a.Name != foundAllergen.Name && a.PotentialAllergenicIngredients.Contains(ingredient)))
                    {
                        excludedAllergen.PotentialAllergenicIngredients.Remove(ingredient);
                    }
                }
            }

            allergens = allergens.OrderBy(a => a.Name).ToList();
            Console.WriteLine(string.Join(",", allergens.Select(a => a.PotentialAllergenicIngredients.First())));
        }

        public class Recipe
        {
            public List<string> Ingredients { get; set; }
            public List<string> Allergens { get; set; }

            public Recipe(string input)
            {
                Ingredients = input.Split(" (")[0].Split(' ').ToList();
                Allergens = input.Split(" (contains ")[1].Replace(")", "").Split(", ").ToList();
            }
        }

        public class Allergen
        {
            public string Name { get; set; }
            public List<string> PotentialAllergenicIngredients { get; set; }
        }
    }
}
