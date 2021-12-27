using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Models;
using Pokedex.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonsController : ControllerBase
    {
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonsController(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }
        // en httpget action som gör att vi kan använda dess metod
        [HttpGet]
        //kör en asynkron operation som vi kallar GetPokemons, som visar hela vår pokemon databas
        public async Task<IEnumerable<Pokemon>> GetPokemons()
        {
            return await _pokemonRepository.Get();
        }
        //en httpget action som vi kallar på och vi kallar på den baserat på vårt id
        [HttpGet("{id}")]
        //kör en asynkron operation som vi kallar GetPokemons som tar in parametern id och visa datan knyten till det id:et
        public async Task<ActionResult<Pokemon>> GetPokemons(int id)
        {
            return await _pokemonRepository.Get(id);
        }
        // en httppost action som gör att vi kan använda dess metod
        [HttpPost]
        //kör en asynkron operation som vi kallar Postpokemon som tar in parametern id och vi specifierar att den skall bindas genom att använda from body (sökrutan) och den ska binda från Pokemon
        public async Task<ActionResult<Pokemon>> PostPokemons([FromBody] Pokemon pokemon)
        {
            var newPokemon = await _pokemonRepository.Create(pokemon);
            return CreatedAtAction(nameof(GetPokemons), new { id = newPokemon.Id }, newPokemon);
        }
        // en httpput action som gör att vi kan använda dess metod
        [HttpPut]
        //kör en asynkron operation som vi kallar PutPokemon som tar in parametern id och vi specifierar att den skall bindas genom att använda from body (sökrutan) och den ska binda från Pokemon
        public async Task<ActionResult> PutPokemon(int id, [FromBody] Pokemon pokemon)
        {
            //om id:et inte är likamed ett id som finns returnera kod 404 annars updatera datan tillhörande id:et och skicka kod 204
            if (id != pokemon.Id)
            {
                return BadRequest();
            }
            await _pokemonRepository.Update(pokemon);
            return NoContent();
        }
        //en httpdelete action som vi kallar på och vi kallar på den baserat på vårt id
       [HttpDelete("{id}")]
       // kör en asynkron operation ActionResult som tar in ett parameter id
       public async Task<ActionResult> DeletePokemons(int id)
        {
            //ta emot id:et, om id:et är tomt: felkod 404 annars deleta datan till det id:et och returna kod 204
            var pokemonToDelete = await _pokemonRepository.Get(id);
            if (pokemonToDelete == null)
                return NotFound();

            await _pokemonRepository.Delete(pokemonToDelete.Id);
            return NoContent();
        }

    }
}
