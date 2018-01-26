using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WhatDo.Models
{
    public class ShowTimeResults
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public string MovieId { get; set; }
        public string CinemaId { get; set; }
        public string StartTime { get; set; }
        public string CinemaName { get; set; }
        public string Website { get; set; }
        public string PhoneNumber { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string StateAbbr { get; set; }
        public string Country { get; set; }
    }
}