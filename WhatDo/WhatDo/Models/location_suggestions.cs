using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatDo.Models
{
    public class location_suggestions
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Country_Id { get; set; }
        public string Country_Name { get; set; }
        public string Should_Experiment_With { get; set; }
        public string Discovery_Enabled { get; set; }
        public string Has_New_Ad_Format { get; set; }
        public string Is_State { get; set; }
        public string State_Id { get; set; }
        public string State_Name { get; set; }
        public string State_Code { get; set; }


    }
}