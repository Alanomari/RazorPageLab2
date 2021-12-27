using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Models
{
    //attributerna som vår pokemon kan ha
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Element { get; set; }
        public string Region { get; set; }
        public string Description { get; set; }
    }
}
