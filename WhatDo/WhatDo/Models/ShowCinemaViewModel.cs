using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatDo.Models
{
    public class ShowCinemaViewModel
    {
        public string Id { get; set; }
        public string Cinema_Id { get; set; }
        public string Start_At { get; set; }
        public string Cinema_Movie_Title { get; set; }
        public string Booking_Link { get; set; }
        public string Name { get; set; }
        public string Chain_Id { get; set; }
        public string Telephone { get; set; }
        public string Website { get; set; }
        public Location Location { get; set; }

    }
}