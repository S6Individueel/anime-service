using AnimeService.Data.Models;
using AnimeService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeService
{
    public static class Extensions
    {
        public static AnimeTopAiringDTO AsTopDTO(this TopAnime anime)
        {
            return new AnimeTopAiringDTO
            {
                Id = anime.mal_id,
                Name = anime.title,
                ImageUrl = anime.image_url,
                Rank = anime.rank,
                Score = anime.score,
                StartDate = anime.start_date,
                EndDate = anime.end_date,
                MaxEpisodes = anime.episodes.Equals(null) ?  "Unknown" : anime.episodes.Value.ToString()
            };
        }
        public static ShowDTO AsShowDTO(this TopAnime show)
        {
            return new ShowDTO
            {
                Id = show.mal_id,
                Title = show.title,
                Media_type = show.type,
                Description = "To be implemented",
                Release_date = show.start_date,
                Image_url = show.image_url
            };
        }
    }
}
