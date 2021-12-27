using Pokedex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Repositories
{
   public interface IPokemonRepository
    {
        Task<IEnumerable<Pokemon>> Get();
        Task<Pokemon> Get(int id);
        Task<Pokemon> Create(Pokemon pokemon);
        Task Update(Pokemon pokemon);
        Task Delete(int id);
    }
}
