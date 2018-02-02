using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatDo.Models
{
    public class ShowTimeSearchViewModel
    {
        public ApplicationUser User { get; set; }
        public Movies Movie { get; set; }
        public List<Showtimes> ShowtimeResults { get; set; }
        public Cinema ResolvedCinema { get; set; }

        public ShowTimeSearchViewModel()
        {
            ShowtimeResults = new List<Showtimes>();
        }
    }
}