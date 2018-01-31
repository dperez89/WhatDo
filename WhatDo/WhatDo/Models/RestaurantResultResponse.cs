using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatDo.Models
{
    public class RestaurantResultResponse
    {
        public List<Restaurants> Restaurants { get; set; }

        public RestaurantResultResponse()
        {
        }
    }
}