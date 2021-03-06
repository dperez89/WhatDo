﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatDo.Models
{
    public class MovieSearchViewModel
    {
        public ApplicationUser User { get; set; }
        public List<string> PreferredGenres { get; set; }
        public List<string> ResolvedPreferredGenreIds { get; set; }
        public List<Movies> MovieSearchResults { get; set; }
        public string ResolvedGenreIdsToSearch { get; set; }
        public string CityId { get; set; }


        public MovieSearchViewModel()
        {
            PreferredGenres = new List<string>();
            ResolvedPreferredGenreIds = new List<string>();
            MovieSearchResults = new List<Movies>();
        }

    }
}