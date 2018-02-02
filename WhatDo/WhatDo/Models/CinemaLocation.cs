using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatDo.Models
{
    public class CinemaLocation
    {
        public Address Address { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }

    }
}