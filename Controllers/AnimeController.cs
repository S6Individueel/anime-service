using AnimeService.DTOs;
using AnimeService.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AnimeService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AnimeController : Controller
    {
        private readonly IAnimeRepository _animeRepository;
/*        private ConcurrentDictionary<string, string> valuePairs = new ConcurrentDictionary<string, string>();
*/        public AnimeController(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }
        
        [HttpGet("popular")]
        public async Task<IEnumerable<ShowDTO>> GetTopTenAsync()
        {
            var animes = (await _animeRepository.GetTopTenAsync())
                            .Select(anime => anime.AsShowDTO());
            return animes;
        }

/*        [HttpGet("host/{name}")]
        public async Task<ReturnModel> GetTestAsync(string name)
        {
            Guid g = Guid.NewGuid();

            if (valuePairs.ContainsKey(g.ToString()))
            {return new ReturnModel(); }
            else {valuePairs.TryAdd(g.ToString(), name); }

            return new ReturnModel(name, g.ToString());
        }

        [HttpPost("join/{roomName}/{name}")]
        public async Task<string> GetTestJoinAsync(string roomName, string name)
        {
            Guid g = Guid.NewGuid();

            if (valuePairs.ContainsKey(roomName))
            {
                if (valuePairs.TryAdd(g.ToString(), name))
                {
                    //Newly Added, return other user
                    await 
                }else
                {
                    //Already exists in lobby
                }
            }
            return number;
        }

        public class ReturnModel{
            public ReturnModel()
            {

            }
            public ReturnModel(string _name, string _roomName)
            {
                name = _name;
                roomName = _roomName;
            }
            public string name { get; set; }
            public string roomName { get; set; }
            public string user1 { get; set; }
            public string user2 { get; set; }
        }*/
    }
}
