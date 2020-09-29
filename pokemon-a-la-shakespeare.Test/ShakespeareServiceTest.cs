using Microsoft.VisualStudio.TestTools.UnitTesting;
using pokemon_a_la_shakespeare.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokemon_a_la_shakespeare.Test
{
    [TestClass]
    public class ShakespeareServiceTest
    {
        private ShakespeareService shakespeareService;

        [TestInitialize]
        public void Setup()
        {
            this.shakespeareService = new ShakespeareService();
        }

        [TestMethod]
        public async Task Poetyze_FromNormalToShakespeareText()
        {
            var normalText = "You gave Mr. Tim a hearty meal, but unfortunately what he ate made him die.";
            var expectedShakespeareText = "Thee did giveth mr. Tim a hearty meal,  but unfortunately what he did doth englut did maketh him kicketh the bucket.";
            var shakespeareText = await this.shakespeareService.PoetyzeAsync(normalText);

            Assert.AreEqual(expectedShakespeareText, shakespeareText);
        }
    }
}
