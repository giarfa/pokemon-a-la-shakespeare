using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pokemon_a_la_shakespeare.Models
{
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
