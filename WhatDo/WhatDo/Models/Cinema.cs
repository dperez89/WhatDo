using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatDo.Models
{
    public class Cinema
    {
        public string Id { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public string Chain_Id { get; set; }
        public string Telephone { get; set; }
        public string Website { get; set; }
        public CinemaLocation Location { get; set; }

    }
}