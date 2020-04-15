using MovieSearchWpf.Commands;
using MovieSearchWpf.Models;
using MovieSearchWpf.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace MovieSearchWpf.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private string movieName;
        private ObservableCollection<Movie> movies;
        private MovieDetail movieDetail;


        public MainViewModel()
        {
            Movies = new ObservableCollection<Movie>();
        }

        public MovieDetail MovieDetail { get => movieDetail; set => OnChanged(out movieDetail, value); }
        public string MovieName { get => movieName; set => OnChanged(out movieName, value); }
        public ObservableCollection<Movie> Movies { get => movies; set => OnChanged(out movies, value); }


        private Command searchMovieCommand = null;
        public Command SearchMovieCommand => searchMovieCommand ?? (searchMovieCommand = new Command(
            () =>
            {
                MovieService movieService = new MovieService();
                var result = movieService.SearchMovieByName(MovieName);
                if (result != null && result.Count() > 0)
                {
                    Clear();
                    MovieDetail = null;
                    foreach (var item in result)
                    {
                        Movies.Add(item);
                    }
                }
                else
                    MessageBox.Show("Movie not found!");
            }));

        private Command<Movie> searchMovieDetailCommand = null;
        public Command<Movie> SearchMovieDetailCommand => searchMovieDetailCommand ?? (searchMovieDetailCommand = new Command<Movie>(
            movie =>
            {
                MovieService movieService = new MovieService();
                MovieDetail = movieService.SearchMovieById(movie.imdbID);
                if (MovieDetail.Poster == "N/A")
                    MovieDetail.Poster = "placeholder.jpg";
                if (MovieDetail.Plot == "N/A")
                    MovieDetail.Plot = "";
            }));

        public void Clear()
        {
            if (Movies != null &&  Movies.Count() > 0)
                   Movies.Clear();
        }
    }
}
