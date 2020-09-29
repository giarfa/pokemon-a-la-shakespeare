using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pokemon_a_la_shakespeare.Services
{
    public class CacheService
    {
        private readonly Dictionary<string, string> cache;

        public CacheService()
        {
            this.cache = new Dictionary<string, string>();
        }

        public bool Contains(string key) => this.cache.ContainsKey(key);

        public void AddOrUpdate(string key, string value) => this.cache[key] = value;

        public string Get(string key) => this.cache[key];
    }
}
