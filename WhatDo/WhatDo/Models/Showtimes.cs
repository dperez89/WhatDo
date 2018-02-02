using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatDo.Models
{
    public class Showtimes
    {
        public string Id { get; set; }
        public string Cinema_Id { get; set; }
        public string Start_At { get; set; }
        public string Cinema_Movie_Title { get; set; }
        public string Booking_Link { get; set; }
    }
}