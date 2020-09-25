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

        public TranslationService(PokemonService pokemonService, ShakespeareService shakespeareService)
        {
            this.pokemonService = pokemonService;
            this.shakespeareService = shakespeareService;
        }

        public async Task<string> TranslateAsync(string pokemonName)
        {
            try
            {
                var pokemonDescription = await this.pokemonService.GetPokemonDescriptionAsync(pokemonName);
                return await shakespeareService.PoetyzeAsync(pokemonDescription);
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
