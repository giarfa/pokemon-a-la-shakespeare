using PokeApiNet;
using pokemon_a_la_shakespeare.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace pokemon_a_la_shakespeare.Services
{
    public class PokemonService
    {
        private readonly PokeApiClient pokeApiClient;

        public PokemonService(PokeApiClient pokeApiClient)
        {
            this.pokeApiClient = pokeApiClient;
        }

        public async Task<string> GetPokemonDescriptionAsync(string pokemonName)
        {
            try
            {
                var pokemonSpecies = await pokeApiClient.GetResourceAsync<PokemonSpecies>(pokemonName);

                return pokemonSpecies.FlavorTextEntries
                                        .First(x => x.Language.Name == "en") // english text seems to be always present
                                        .FlavorText;
            }
            catch (HttpRequestException ex) 
            {
                if (ex.Message.Contains("404 (Not Found)"))
                    throw new PokemonNotFoundException();
                else
                    throw new BadException();
            }
        }
    }
}
