using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokeApiNet;
using pokemon_a_la_shakespeare.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokemon_a_la_shakespeare.Test
{
    [TestClass]
    public class PokemonServiceTest
    {
        private PokemonService pokemonService;
        private PokeApiClient pokeApiClient;

        [TestInitialize]
        public void Setup()
        {
            this.pokeApiClient = new PokeApiClient();
            this.pokemonService = new PokemonService(this.pokeApiClient);
        }

        [TestMethod]
        public async Task Pokemon_ExistingPokemon_DescriptionAreCorrectlyFetched()
        {
            var pikachu = "pikachu";

            var description = await this.pokemonService.GetPokemonDescriptionAsync(pikachu);

            StringAssert.Contains(description, "When several of");
            StringAssert.Contains(description, "these POKéMON");
            StringAssert.Contains(description, "gather, their");
            StringAssert.Contains(description, "electricity could");
            StringAssert.Contains(description, "build and cause");
            StringAssert.Contains(description, "lightning storms.");
        }

        [TestMethod]
        public async Task Pokemon_NonExistingPokemon_ShouldThrowPokemonNotFoundException()
        {
            var nonExistingPokemon = "NonExistingPokemon";

            await Assert.ThrowsExceptionAsync<Exceptions.PokemonNotFoundException>(async () => await this.pokemonService.GetPokemonDescriptionAsync(nonExistingPokemon));
        }
    }
}
