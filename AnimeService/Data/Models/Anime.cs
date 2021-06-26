using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeService.Data.Models
{
    public class Anime
    {
        public int mal_id { get; set; }
        public int rank { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string image_url { get; set; }
        public string type { get; set; }
        public int episodes { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public double score { get; set; }
        public string Description { get; set; }
        public int MaxSeasons { get; set; }
    }
}
