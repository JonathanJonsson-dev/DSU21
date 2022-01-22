using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21.Models
{
    public class Ship
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // navigation property 
        // captain
        public int? PirateID { get; set; } // ? medger att propertyn kan vara null

        public virtual Pirate Captain { get; set; }

        // naviaton properties
        public virtual ICollection<Pirate> Pirates { get; set; } = new List<Pirate>(); // virtual betyder att en datatyp kan ändras

    }
}
