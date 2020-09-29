using pokemon_a_la_shakespeare.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pokemon_a_la_shakespeare.Services
{
    public class TranslationService
    {
        private readonly PokemonService pokemonService;
        private readonly ShakespeareService shakespeareService;
        private readonly TranslationCacheService translationCacheService;

        public TranslationService(PokemonService pokemonService, ShakespeareService shakespeareService, TranslationCacheService translationCacheService)
        {
            this.pokemonService = pokemonService;
            this.shakespeareService = shakespeareService;
            this.translationCacheService = translationCacheService;
        }

        public async Task<string> TranslateAsync(string pokemonName)
        {
            if (this.translationCacheService.Contains(pokemonName))
                return this.translationCacheService.Get(pokemonName);

            try
            {
                var pokemonDescription = await this.pokemonService.GetPokemonDescriptionAsync(pokemonName);
                var poetyzedDescription = await shakespeareService.PoetyzeAsync(pokemonDescription);
                this.translationCacheService.AddOrUpdate(pokemonName, poetyzedDescription);
                return poetyzedDescription;
            }
            catch (PokemonNotFoundException)
            {
                throw;
            }
            catch (TooManyPoetryzeRequestsException)
            {
                throw;
            }
            catch (BadException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new BadException();
            }
        }
    }
}
