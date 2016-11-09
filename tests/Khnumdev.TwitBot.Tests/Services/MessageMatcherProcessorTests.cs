namespace Khnumdev.TwitBot.Tests.Services
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Data.Repositories;
    using TwitBot.Services;

    /// <summary>
    /// Summary description for MessageMatcherProcessorTests
    /// </summary>
    [TestClass]
    public class MessageMatcherProcessorTests
    {
        [TestMethod]
        public void CalculatePharseCoincidence()
        {
            ITwitterRepository twitterRepositoryMock = Mock.Of<ITwitterRepository>();

            var processor = new MessageMatcherProcessor(twitterRepositoryMock);

            //var keyPhrasesFromTwitter =  "mí ya se,Hoy empieza el otoño y,caen los pelos del coño";
            var keyPhrasesFromTwitter2 = "Follonero, como cuando te como el rabo entero,gusta cómo los dejas en evidencia, jordievole";
            var keyPhrasesFromRequest = "Te gusta el otoño";

            var result = processor.CalculatePharseCoincidence(keyPhrasesFromRequest, keyPhrasesFromTwitter2);
        }
    }
}
