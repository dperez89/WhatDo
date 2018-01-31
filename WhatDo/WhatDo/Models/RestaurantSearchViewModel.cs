using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatDo.Models
{
    public class RestaurantSearchViewModel
    {
        public ApplicationUser User { get; set; }
        public List<Restaurant> RestaurantResults { get; set; }
        public List<string> CuisineIdsToSearch { get; set; }
        public string ResolvedCuisineIdsToSearch { get; set; }

        public RestaurantSearchViewModel()
        {
            CuisineIdsToSearch = new List<string>();
            RestaurantResults = new List<Restaurant>();
        }
    }
}