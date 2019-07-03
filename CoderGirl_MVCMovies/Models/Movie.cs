﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.Models
{
    public class Movie : IModel
    {
        public int Id { set; get; }
        public string Name { get; set; }
        public int Year { get; set; }
        public List<MovieRating> Ratings { get; set; }
        public List<DirectorMovie> DirectorMovies { get; set; }
    }
}
