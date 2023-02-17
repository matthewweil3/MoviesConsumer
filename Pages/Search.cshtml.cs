using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace MoviesConsumer.Pages
{
    public class SearchModel : PageModel
    {
        public class Movie
        {
            //Have to use fields pulled in from Search Data
            public string imdbID { get; set; }
            public string Title { get; set; }
            public string Poster { get; set; }
        }

        public class MovieList
        { 
            public string totalResults { get; set; }
            public List<Movie> Search { get; set; }
        }

        private readonly ILogger<SearchModel> _logger;

        public SearchModel(ILogger<SearchModel> logger)
        {
            _logger = logger;
        }

        public IList<Movie> movies { get; set; }

        public void OnGet()
        {
            movies = new List<Movie>();
        }

        [BindProperty]
        public string MovieName { get; set; }
        public async Task<IActionResult> OnPost()
        {
            Uri mb = new Uri("http://www.omdbapi.com/?apikey=616bc2a1&s=" + MovieName+ "&fmt=json");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "tmu.edu");
            HttpResponseMessage response = await client.GetAsync(mb.ToString());

            if (response.IsSuccessStatusCode)
            {
                string data1 = await response.Content.ReadAsStringAsync();
                var MoviesResults = JsonConvert.DeserializeObject<MovieList>(data1);
                movies = MoviesResults.Search;
            }
            return Page();
        }
    }
}
