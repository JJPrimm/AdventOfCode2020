using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class Day7
    {
        public static void Problem1()
        {
            var input = Utilities.ReadStringArray(7);
            var bags = new List<Bag>();

            foreach (var bag in input)
            {
                bags.Add(new Bag(bag));
            }

            var shinyGoldParents = bags.Find(b => b.Attribute == "shiny" && b.Color == "gold").GetParents(bags);

            Console.WriteLine(shinyGoldParents.Count);
        }

        public static void Problem2()
        {
            var input = Utilities.ReadStringArray(7);
            var bags = new List<Bag>();

            foreach (var bag in input)
            {
                bags.Add(new Bag(bag));
            }

            var shinyGoldCount = bags.Find(b => b.Attribute == "shiny" && b.Color == "gold").GetContainedBagsCount(bags);

            Console.WriteLine(shinyGoldCount);
        }
    }

    public class Bag
    {
        public Bag() { }

        public Bag(string input) {
            Attribute = input.Split(' ')[0];
            Color = input.Split(' ')[1];
            if (!input.EndsWith("no other bags."))
            {
                var containedBags = input.Replace(".", "").Split("contain ")[1].Split(", ");
                foreach (var containedBag in containedBags)
                {
                    var count = Convert.ToInt32(containedBag.Split(' ')[0]);
                    var attribute = containedBag.Split(' ')[1];
                    var color = containedBag.Split(' ')[2];
                    ContainedBags.Add(new Bag { Count = count, Attribute = attribute, Color = color });
                }
            }
        }

        public int Count { get; set; }
        public string Attribute { get; set; }
        public string Color { get; set; }
        public List<Bag> ContainedBags { get; set; } = new List<Bag>();

        public List<Bag> GetParents (List<Bag> bags)
        {
            var parents = bags.Where(b => b.ContainedBags.Any(cb => cb.Attribute == Attribute && cb.Color == Color));
            var response = parents.ToList();

            foreach (var parent in parents)
            {
                response.AddRange(parent.GetParents(bags));
            }
            return response.Distinct().ToList();
        }

        public int GetContainedBagsCount(List<Bag> bags)
        {
            int count = 0;
            foreach(var bag in ContainedBags)
            {
                count += bag.Count + bag.Count * bags.Find(b => b.Attribute == bag.Attribute && b.Color == bag.Color).GetContainedBagsCount(bags);
            }
            return count;
        }
    }


}
