using Microsoft.VisualStudio.TestTools.UnitTesting;
using pokemon_a_la_shakespeare.Services;

namespace pokemon_a_la_shakespeare.Test
{
    [TestClass]
    public class CacheServiceTest
    {
        private CacheService cacheService;

        [TestInitialize]
        public void Setup()
        {
            this.cacheService = new CacheService();
        }

        [TestMethod]
        public void Cache_ValuesAreStoredCorrectly()
        {
            var key = "key";
            var value = "value";

            this.cacheService.AddOrUpdate(key, value);

            Assert.AreEqual(value, this.cacheService.Get(key));
        }

        [TestMethod]
        public void Cache_KeysAreStoredCorrectly()
        {
            var key = "key";
            var value = "value";

            this.cacheService.AddOrUpdate(key, value);

            Assert.AreEqual(true, this.cacheService.Contains(key));
        }
    }
}
