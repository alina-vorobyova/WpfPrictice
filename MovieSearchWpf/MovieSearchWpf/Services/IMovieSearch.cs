using MovieSearchWpf.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieSearchWpf.Services
{
    interface IMovieSearch
    {
        List<Movie> SearchMovieByName(string title);

        MovieDetail SearchMovieById(string id);

    }
}
