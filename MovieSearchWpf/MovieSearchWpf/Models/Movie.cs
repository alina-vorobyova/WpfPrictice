﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MovieSearchWpf.Models
{

    public class MovieResult
    {
        public List<Movie> Search { get; set; }
        public string totalResults { get; set; }
        public string Response { get; set; }
    }

    public class Movie
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
    }

}
