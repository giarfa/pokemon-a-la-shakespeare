using Newtonsoft.Json;
using pokemon_a_la_shakespeare.Exceptions;
using pokemon_a_la_shakespeare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace pokemon_a_la_shakespeare.Services
{
    public class ShakespeareService
    {
        public async Task<string> PoetyzeAsync(string textToPoetyze)
        {
            var h = new HttpClient();

            var formContent = new FormUrlEncodedContent(
                        new[]{
                        new KeyValuePair<string, string>("text", textToPoetyze)
                        }
                    );

            try
            {

                var translatedResponse = await h.PostAsync(
                    new Uri($"https://api.funtranslations.com/translate/shakespeare.json"),
                    formContent
                    );

                var jsonTranslationString = await translatedResponse.Content.ReadAsStringAsync();
                var traslationResult = JsonConvert.DeserializeObject<TranslationResult>(jsonTranslationString);

                if (traslationResult.Success != null)
                    return traslationResult.Content.Translated;

                else if (traslationResult.Error?.Code == "429")
                    throw new TooManyPoetryzeRequestsException();
                    //return StatusCode(StatusCodes.Status429TooManyRequests, new TranslatedPokemon(pokemonName, "Shakespeare is tired now, let's give him one hour or so to rest"));

                else
                    throw new BadException();
                    //return this.UnprocessableEntity();
            }
            catch (Exception)
            {
                throw new BadException();
            }
        }
    }
}
