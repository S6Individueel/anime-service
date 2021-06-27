using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimeService.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using AnimeService.Repositories.Interfaces;
using AnimeService.Data.Models;

namespace AnimeService.Controllers.Tests
{
    [TestClass()]
    public class AnimeControllerTests
    {
        [TestMethod()]
        public async Task GetTopTenAsyncTest()
        {
            //Arange
            int animeCount = 10;
            var animeRepo = A.Fake<IAnimeRepository>(); //Fakes a repository to initialize the controller with
            var fakeAnimes = A.CollectionOfDummy<TopAnime>(10).AsEnumerable(); //Makes a dummy collection of the topanime type
            A.CallTo(() => animeRepo.GetTopTenAsync()).Returns(await Task.FromResult(fakeAnimes));//Configures the call to return the faked data, making it independent from the API and testing pure code.
            var controller = new AnimeController(animeRepo);
            //Act

            var result = await controller.GetTopTenAsync(); //Makes the call
            //Assert

            Assert.AreEqual(animeCount, result.Count()); //Checks if the list is filled
        }
    }
}