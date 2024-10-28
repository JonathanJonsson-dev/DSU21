using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21.Helpers
{
    public class Allergies
    {
        public string Name { get; }

        [Flags]
        public enum Allergen
        {
            Eggs = 1,
            Peanuts = 2,
            Shellfish = 4,
            Strawberries = 8,
            Tomatoes = 16,
            Chocolate = 32,
            Pollen = 64,
            Cats = 128
        }

        public Allergies(string name)
        {
            Name = name;
        }

        public Allergies(string name, string allergies) : this(name) // Ärver från sig själv? name
        {


        }

        public Allergies(string name, int allergyScore) : this(name)
        {

        }

        public override string ToString()
        {
            return $"{Name} has no allergies!";
        }

    }
}
