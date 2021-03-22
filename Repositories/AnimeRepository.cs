﻿using AnimeService.Data.Models;
using AnimeService.DTOs;
using AnimeService.Repositories.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Anime>> GetTopTenAsync()
        {
            string uri = _httpclient.BaseAddress + "/top/anime/1/airing";
            HttpResponseMessage response = await _httpclient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
            //var responseString = response.Content.ReadAsStringAsync().Result;
            //JObject jObject = JObject.Parse(responseString); 

            throw new NotImplementedException();
        }
        //return topAnime;
    
    }
}
