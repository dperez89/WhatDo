using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WhatDo.Models
{
    public class FoodSuggestion
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public int? RestaurantId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Cuisine { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public decimal? AverageCostForTwo { get; set; }
        public decimal? UserRating { get; set; }
        public string RatingText { get; set; }
        public int? NumberOfVotes { get; set; }
        public bool? HasOnlineDelivery { get; set; }
        public bool? IsGoodSuggestion { get; set; }
        public bool? IsChosenByUser { get; set; }
    }
}