using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WhatDo.Models
{
    public class MovieSuggestion
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public string MovieId { get; set; }
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public int RunTime { get; set; }
        public string Genres { get; set; }
        public string Website { get; set; }
        public decimal Rating { get; set; }
        public string ReleaseDate { get; set; }
        public string ImageUrl { get; set; }
        public bool IsGoodSuggestion { get; set; }
        public bool IsChosenByUser { get; set; }
    }
}