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
    public class TranslationServiceTest
    {
        private PokemonService pokemonService;
        private PokeApiClient pokeApiClient;
        private ShakespeareService shakespeareService;
        private CacheService cacheService;
        private TranslationService translationService;

        [TestInitialize]
        public void Setup()
        {
            this.pokeApiClient = new PokeApiClient();
            this.pokemonService = new PokemonService(this.pokeApiClient);
            this.shakespeareService = new ShakespeareService();
            this.cacheService = new CacheService();

            this.translationService = new TranslationService(pokemonService, shakespeareService, cacheService);
        }

        [TestMethod]
        public async Task Translation_DescriptionsAreCorrectlyProduced()
        {
            var pikachu = "pikachu";
            var expectedPikachuDescription = "At which hour several of these pokémon gather,  their electricity couldst buildeth and cause lightning storms.";
            var actualPikachuDescription = await this.translationService.TranslateAsync(pikachu);

            Assert.AreEqual(expectedPikachuDescription, actualPikachuDescription);
        }
    }
}
