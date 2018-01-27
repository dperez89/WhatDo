using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatDo.Models
{
    public class CityResultResponse
    {
        public List<location_suggestions> Location_Suggestions { get; set; }
        public string Status { get; set; }
        public string Has_More { get; set; }
        public string Has_Total { get; set; }
    }
}