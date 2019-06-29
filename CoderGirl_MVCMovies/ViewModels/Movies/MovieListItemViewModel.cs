using CoderGirl_MVCMovies.Data;
using CoderGirl_MVCMovies.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.ViewModels.Movies
{
    public class MovieListItemViewModel
    {
        public static List<MovieListItemViewModel> GetMovies(RepositoryFactory factory)
        {
            return factory.GetMovieRepository()
                .GetModels()
                .Select(m => new MovieListItemViewModel(m))
                .ToList();
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Name { get; set; }
        public int Year { get; set; }
        public string DirectorNames { get; set; }
        public string AverageRating { get; set; }
        public int NumberOfRatings { get; set; }

        public MovieListItemViewModel(Movie movie, MoviesDbContext context)
        {
            this.Id = movie.Id;
            this.Name = movie.Name;
            this.Year = movie.Year;
            this.DirectorNames = GetDirectorNames(movie, context);
            this.AverageRating = movie.Ratings.Count > 0 ? Math.Round(movie.Ratings.Average(x => x.Rating), 2).ToString() : "none";
            this.NumberOfRatings = movie.Ratings.Count;
        }

        private string GetDirectorNames(Movie movie, MoviesDbContext context)
        {
            List<string> directorNames = movie.DirectorMovies
                .Select(dm => dm.Director)
                .Select(d => d.FullName)
                .ToList();
            
            //Do this if navigation properties are null and you can't fix that:
            //List<int> directorIds = movie.DirectorMovies.Select(dm => dm.DirectorId).ToList();
            //List<Director> directors = context.Directors.Where(d => directorIds.Contains(d.Id)).ToList();
            //directorNames = directors.Select(d => d.FullName).ToList();

            return String.Join(", " , directorNames);
        }
    }
}
