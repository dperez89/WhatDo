using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WhatDo.Models;

namespace WhatDo.Controllers
{
    public class EnjoyerController : Controller
    {
        ApplicationDbContext db;
        CityIdResolver cityIdResolver;

        public EnjoyerController()
        {
            cityIdResolver = new CityIdResolver();
            db = new ApplicationDbContext();
        }
        // GET: Enjoyer
        public ActionResult Index()
        {            
            return View();
        }

        // GET
        public ActionResult GetPreferences()
        {
            var currentUser = db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
            string currentUserCity = cityIdResolver.Resolve(currentUser);
            GetPreferencesViewModel preferencesViewModel = new GetPreferencesViewModel();
            var client = new WebClient();
            client.Headers.Add("user-key","d846616ebd6c5c018f6cd8fd36a6fb68");
            var response = client.DownloadString("https://developers.zomato.com/api/v2.1/cuisines?city_id="+currentUserCity);
            var cuisineResults = new JavaScriptSerializer().Deserialize<CuisineResultResponse>(response);
            foreach(Cuisines cuisine in cuisineResults.Cuisines)
            {
                string cuisineNameToAdd = cuisine.Cuisine.Cuisine_Name;
                preferencesViewModel.PreferredCuisines.Add(cuisineNameToAdd);
            }
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("http://localhost:9000/");
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //    // New code:
            //    HttpResponseMessage response = await client.GetAsync("api/products/1");
            //    if (response.IsSuccessStatusCode)
            //    {
            //        PreferencesViewModel.PreferredCuisines = response.Content.ReadAsAsync > Product > ();
            //        Console.WriteLine("{0}\t${1}\t{2}", product.Name, product.Price, product.Category);
            //    }
            //}
            return View(preferencesViewModel);
        }
    }
}