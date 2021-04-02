using AnimeService.Data.Models;
using AnimeService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeService.Repositories.Interfaces
{
    public interface IAnimeRepository
    {
        Task<IEnumerable<TopAnime>> GetTopTenAsync();
        Task<IEnumerable<Anime>> GetAnimeListAsync();
    }
}
