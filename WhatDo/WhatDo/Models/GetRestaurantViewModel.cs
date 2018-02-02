using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatDo.Models
{
    public class GetRestaurantViewModel
    {
        public ApplicationUser User { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}