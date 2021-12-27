using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pokedex.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RazorPageLab2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<Pokemon> Pokemons { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:44323/api/Pokemons").Result.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject<List<Pokemon>>(response);

                Pokemons = data;
            }
        }

        public async Task OnPostReturnByName(string name)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:44323/api/Pokemons").Result.Content.ReadAsStringAsync();
                 Pokemons = JsonConvert.DeserializeObject<List<Pokemon>>(response).Where(x => x.Name == name).ToList();

            }
           
        }

        public async Task OnPostAddPokemon(string name, string element, string region, string description)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44323/api/");

                var pokemon = new Pokemon();
                pokemon.Name = name;
                pokemon.Element = element;
                pokemon.Region = region;
                pokemon.Description = description;

                var json = JsonConvert.SerializeObject(pokemon);

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await client.PostAsync("Pokemons", content);
                string resContent = await result.Content.ReadAsStringAsync();




            }
            await OnGet();
        }



    }
}
