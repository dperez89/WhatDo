using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WhatDo.Models
{
    public class UserToCuisine
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public int CuisineId { get; set; }
        [ForeignKey("CuisineId")]
        public DatabaseCuisine Cuisine { get; set; }
    }
}