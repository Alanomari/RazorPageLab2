using Microsoft.EntityFrameworkCore;
using Pokedex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly PokemonContext _context;
        public PokemonRepository(PokemonContext context)
        {
            _context = context;
        }
        // skapa en asynkron operation för att skapa en pokemon
        public async Task<Pokemon> Create(Pokemon pokemon)
        {
            //skapa och lägg till pokemonen och spara ändringar, returnera pokemonen
            _context.Pokemons.Add(pokemon);
            await _context.SaveChangesAsync();

            return pokemon;
        }
        // en asynkron operation för att deleta en pokemon baserat på pokemonets id och spara ändringen
        public async Task Delete(int id)
        {
            var pokemonToDelete = await _context.Pokemons.FindAsync(id);
            _context.Pokemons.Remove(pokemonToDelete);
            await _context.SaveChangesAsync();
        }

        //en asynkron operation för att lista alla pokemon
        public async Task<IEnumerable<Pokemon>> Get()
        {
            return await _context.Pokemons.ToListAsync();
        }
        //en asynkron operation för att lista pokemon med korresponderande id
        public async Task<Pokemon> Get(int id)
        {
            return await _context.Pokemons.FindAsync(id);
        }
        //updatera när en pokemons data blivit modifierat och spara ändringar
        public async Task Update(Pokemon pokemon)
        {
            _context.Entry(pokemon).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
