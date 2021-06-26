using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeService.DTOs
{
    public class AnimeTopAiringDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; }
        public string ImageUrl { get; set; }
        public string MaxEpisodes { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public double Score { get; set; }
    }
}
