using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatDo.Models
{
    public class Address
    {
        public string Display_Text { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string State_Abbr { get; set; }
        public string Country { get; set; }
        public string Country_Code { get; set; }

    }
}