using AnimeService.Data.Models;
using AnimeService.DTOs;
using AnimeService.Repositories.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AnimeService.Repositories
{
    public class AnimeRepository : IAnimeRepository
    {

        private readonly HttpClient _httpclient;
        public AnimeRepository(HttpClient httpClient)
        {
            _httpclient = httpClient;
        }
        public async Task<IEnumerable<Anime>> GetAnimeListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TopAnime>> GetTopTenAsync()
        {
            string uri = _httpclient.BaseAddress + "/top/anime/0/airing";
            HttpResponseMessage response = await _httpclient.GetAsync(uri);
            var content = await response.Content.ReadAsStringAsync();

            IList<JToken> results = JObject.Parse(content)["top"].Children().ToList(); //Parses content, gets the "top" list and converts to list.

            IList<TopAnime> topAnimes = new List<TopAnime>();
            foreach (JToken anime in results)
            {
                TopAnime topAnime = anime.ToObject<TopAnime>();
                topAnimes.Add(topAnime);
            }

            return topAnimes;
        }
    
    }
}
