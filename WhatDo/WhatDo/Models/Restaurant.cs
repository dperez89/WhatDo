using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatDo.Models
{
    public class Restaurant
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Location Location { get; set; }
        public string Address { get; set; }
        public string Locality { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Zipcode { get; set; }
        public string Country_Id { get; set; }
        public string Cuisines { get; set; }
        public string Average_Cost_For_Two { get; set; }
        public string Price_Range { get; set; }
    }
}