using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using WhatDo.Models;

namespace WhatDo.Controllers
{
    public class CityIdResolver
    {
        public CityIdResolver()
        {

        }

        public string Resolve(ApplicationUser currentUser)
        {      
            var client = new WebClient();            
            client.Headers.Add("user-key", "d846616ebd6c5c018f6cd8fd36a6fb68");
            var response = client.DownloadString("https://developers.zomato.com/api/v2.1/cities?q="+currentUser.City);
            var citiesResults = new JavaScriptSerializer().Deserialize<ZomatoCityResultResponse>(response);
            //var citiesResults = new JavaScriptSerializer().Deserialize<location_suggestions>(response);
            string resolvedCityId = citiesResults.Location_Suggestions[0].Id;

            //string resolvedCityId = releases.['location_suggestions'][0].id;

            return resolvedCityId;
        }
    }
}