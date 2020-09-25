using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PokeApiNet;
using pokemon_a_la_shakespeare.Exceptions;
using pokemon_a_la_shakespeare.Models;
using pokemon_a_la_shakespeare.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace pokemon_a_la_shakespeare.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly TranslationService translationService;

        public PokemonController(TranslationService translationService)
        {
            this.translationService = translationService;
        }

        [HttpGet]
        public string Get() => $"Try with a pokemon name such as pikachu!!";

        [HttpGet("{pokemonName}")]
        public async Task<ActionResult<TranslatedPokemon>> Get(string pokemonName)
        {
            try
            {
                var translatedDescription = await this.translationService.TranslateAsync(pokemonName);
                return Ok(new TranslatedPokemon(pokemonName, translatedDescription));
            }
            catch (PokemonNotFoundException)
            {
                return NotFound(new TranslatedPokemon(pokemonName, "Pokemon not found"));
            }
            catch (TooManyPoetryzeRequestsException)
            {
                return StatusCode(StatusCodes.Status429TooManyRequests, new TranslatedPokemon(pokemonName, "Shakespeare is tired now, let's give him one hour or so to rest"));
            }
            catch (BadException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new TranslatedPokemon(pokemonName, "Something went wrong, please try again later"));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new TranslatedPokemon(pokemonName, "Something went wrong, please try again later"));
            }
        }
    }
}
