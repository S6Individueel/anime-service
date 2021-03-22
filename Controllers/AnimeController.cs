using AnimeService.DTOs;
using AnimeService.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AnimeService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimeController : Controller
    {
        private readonly IAnimeRepository _animeRepository;

        public AnimeController(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        [HttpGet("/popular")]
        public async Task<IEnumerable<AnimeTopAiringDTO>> GetTopTenAsync()
        {
            var animes = (await _animeRepository.GetTopTenAsync())
                            .Select(anime => anime.AsTopDTO());
            return animes;
        }
    }
}
