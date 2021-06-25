using FakeItEasy;
using System;
using System.Threading.Tasks;
using Xunit;
using AnimeService.Data.Models;
using System.Linq;
using AnimeService.Repositories.Interfaces;
using AnimeService.Controllers;

namespace AnimeService.Tests
{
    public class AnimeControllerTests
    {
        [Fact]
        public async Task Get_Top_Ten_Movies()
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

            Assert.Equal(animeCount, result.Count()); //Checks if the list is filled
        }
    }
}
