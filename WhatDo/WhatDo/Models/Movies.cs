using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatDo.Models
{
    public class Movies
    {
        public string Id { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public string Runtime { get; set; }
        public List<Genre> Genres { get; set; }
        public string Poster_Image_Thumbnail { get; set; }
    }
}