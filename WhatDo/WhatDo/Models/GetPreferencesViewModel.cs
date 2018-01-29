using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WhatDo.Models
{
    public class GetPreferencesViewModel
    {
        public ApplicationUser User { get; set; }
        public string ZipCode { get; set; }
        public string PreferredCuisineToAdd { get; set; }
        public string PreferredGenreToAdd { get; set; }
        public List<string> PreferredCuisines { get; set; }
        public List<string> PreferredGenres { get; set; }
        public List<string> AvailableCuisines { get; set; }
        public List<string> AvailableGenres { get; set; }
        public GetPreferencesViewModel()
        {
            AvailableCuisines = new List<string> { };
            AvailableGenres = new List<string> { };
            PreferredCuisines = new List<string> { };
            PreferredGenres = new List<string> { };

        }
    }
}