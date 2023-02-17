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
    public class DetailsModel : PageModel
    {
        public class Movie
        {
            public string Title { get; set; }
            public string Genre { get; set; }
            public string Released { get; set; }
            public string Year { get; set; }
            public string Director { get; set; }
            public string Plot { get; set; }
            public string Actors { get; set; }
            public string Poster { get; set; }
            public List <Rating> Ratings { get; set; }
        }

        public class Rating 
        { 
        public string Source { get; set; }
        public string Value { get; set; }
        }


        public Movie movie;
        public async Task<IActionResult> OnGetAsync(string id)
        {
            Uri mb = new Uri("http://www.omdbapi.com/?apikey=616bc2a1&i=" + id + "&fmt=json");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "tmu.edu");
            HttpResponseMessage response = await client.GetAsync(mb.ToString());

            if (response.IsSuccessStatusCode)
            {
                string data1 = await response.Content.ReadAsStringAsync();
                var MoviesResults = JsonConvert.DeserializeObject<Movie>(data1);
                movie = MoviesResults;
            }
            return Page();
        }
    }
}
