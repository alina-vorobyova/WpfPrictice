using MovieSearchWpf.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;

namespace MovieSearchWpf.Services
{
    class MovieService : IMovieSearch
    {
        const string apiKey = "2e73a2e7";
        const string url = "http://www.omdbapi.com/";

        public MovieDetail SearchMovieById(string id)
        {
            WebClient webClient = new WebClient();
            var json = webClient.DownloadString($"{url}?apikey={apiKey}&i={id}");
            var movieDetail = JsonSerializer.Deserialize<MovieDetail>(json);
            return movieDetail;
        }

        public List<Movie> SearchMovieByName(string title)
        {
            WebClient webClient = new WebClient();
            var json = webClient.DownloadString($"{url}?apikey={apiKey}&s={title}");
            var movies = JsonSerializer.Deserialize<MovieResult>(json).Search;
            return movies;
        }
    }
}
