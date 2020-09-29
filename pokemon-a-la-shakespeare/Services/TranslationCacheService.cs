using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pokemon_a_la_shakespeare.Services
{
    public class TranslationCacheService
    {
        private readonly Dictionary<string, string> translationsCache;

        public TranslationCacheService()
        {
            this.translationsCache = new Dictionary<string, string>();
        }

        public bool Contains(string key) => this.translationsCache.ContainsKey(key);

        public void AddOrUpdate(string key, string value) => this.translationsCache[key] = value;

        public string Get(string key) => this.translationsCache[key];
    }
}
