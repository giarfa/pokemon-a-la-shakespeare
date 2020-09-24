using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PokeApiNet;
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
        private readonly PokeApiClient pokeApiClient;

        public PokemonController(PokeApiClient pokeApiClient)
        {
            this.pokeApiClient = pokeApiClient;
        }

        [HttpGet]
        public string Get() => $"Try with a pokemon name such as pikachu!!";

        [HttpGet("{pokemonName}")]
        public async Task<ActionResult<TranslatedPokemon>> Get(string pokemonName)
        {
            var pokemonSpecies = await pokeApiClient.GetResourceAsync<PokemonSpecies>(pokemonName);

            var textToTranslate = pokemonSpecies.FlavorTextEntries
                                                .First(x => x.Language.Name == "en") // english text seems to be always present
                                                .FlavorText;

            var h = new HttpClient();

            var formContent = new FormUrlEncodedContent(
                        new[]{
                        new KeyValuePair<string, string>("text", textToTranslate)
                        }
                    );

            var translatedResponse = await h.PostAsync(
                new Uri($"https://api.funtranslations.com/translate/shakespeare.json"),
                formContent
                );

            var jsonTranslationString = await translatedResponse.Content.ReadAsStringAsync();
            var traslationResult = JsonConvert.DeserializeObject<TranslationResult>(jsonTranslationString);

            if (traslationResult.Success != null)
                return Ok(new TranslatedPokemon(pokemonSpecies.Name, traslationResult.Content.Translated));
            else if (traslationResult.Error?.Code == "429")
                return StatusCode(StatusCodes.Status429TooManyRequests, new TranslatedPokemon(pokemonSpecies.Name, "Shakespeare is tired now, let's give him one hour or so to rest"));
            else
                return this.UnprocessableEntity();
        }
    }

    public class TranslatedPokemon
    {
        public TranslatedPokemon(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; }
        public string Description { get; }
    }

    public class TranslationResult
    {
        [JsonProperty("success")]
        public TranslationSuccess Success { get; set; }

        [JsonProperty("contents")]
        public TranslationContent Content { get; set; }

        [JsonProperty("error")]
        public TranslationError Error { get; set; }
    }

    public class TranslationSuccess
    {
        [JsonProperty("total")]
        public int Total { get; set; }
    }

    public class TranslationContent
    {
        [JsonProperty("translated")]
        public string Translated { get; set; }
    }

    public class TranslationError
    {
        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
