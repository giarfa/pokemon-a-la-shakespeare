using Microsoft.AspNetCore.Mvc;
using PokeApiNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pokemon_a_la_shakespeare.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly PokeApiClient pokeApiClient;

        public PokemonController(PokeApiClient pokeApiClient)
        {
            this.pokeApiClient = pokeApiClient;
        }

        [HttpGet]
        public string Get() => $"Try with a pokemon name such as pikachu!!";

        [HttpGet("{pokemonName}")]
        public async Task<string> Get(string pokemonName)
        {
            var pokemonSpecies = await pokeApiClient.GetResourceAsync<PokemonSpecies>(pokemonName);

            return $"Hello {pokemonSpecies.Name}, specie: {pokemonSpecies.FlavorTextEntries.First(x => x.Language.Name == "en").FlavorText}!!";
        }
    }
}
