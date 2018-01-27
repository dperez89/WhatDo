using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatDo.Models
{
    public class GetPreferencesViewModel
    {
        public ApplicationUser User { get; set; }
        public string ZipCode { get; set; }
        public List<string> PreferredCuisines { get; set; }
        public List<string> PreferredGenres { get; set; }

        public GetPreferencesViewModel()
        {
            PreferredCuisines = new List<string> { };
            PreferredGenres = new List<string> { };
        }
    }
}