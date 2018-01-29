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
        [HttpGet]
        public ActionResult GetPreferences()
        {
            GetPreferencesViewModel preferencesViewModel = new GetPreferencesViewModel();
            var currentUser = db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
            preferencesViewModel.User = currentUser;
            string currentUserCity = cityIdResolver.Resolve(currentUser);            
            var client = new WebClient();
            client.Headers.Add("user-key","d846616ebd6c5c018f6cd8fd36a6fb68");
            var response = client.DownloadString("https://developers.zomato.com/api/v2.1/cuisines?city_id="+currentUserCity);
            var cuisineResults = new JavaScriptSerializer().Deserialize<CuisineResultResponse>(response);
            foreach(Cuisines cuisine in cuisineResults.Cuisines)
            {
                string cuisineNameToAdd = cuisine.Cuisine.Cuisine_Name;
                preferencesViewModel.AvailableCuisines.Add(cuisineNameToAdd);
            }
            foreach (UserToCuisine cuisine in db.UserToCuisines)
            {
                for (int i = 0; i <= preferencesViewModel.AvailableCuisines.Count-1; i++)
                {
                    if (cuisine.UserId == preferencesViewModel.User.Id && cuisine.CuisineName == preferencesViewModel.AvailableCuisines[i])
                    {
                        preferencesViewModel.AvailableCuisines.RemoveAt(i);
                        i--;
                    }
                }
            }
            foreach (UserToCuisine cuisine in db.UserToCuisines)
            {
                if(cuisine.UserId == preferencesViewModel.User.Id)
                {
                    preferencesViewModel.PreferredCuisines.Add(cuisine.CuisineName);
                }
            }
            preferencesViewModel.CuisineOptions = new SelectList(preferencesViewModel.AvailableCuisines);
            
            return View(preferencesViewModel);
        }
       
        public ActionResult AddPreferences(string cuisineToAdd, string userId)
        {
            foreach (UserToCuisine element in db.UserToCuisines)
            {
                if (element.UserId == userId && element.CuisineName == cuisineToAdd)
                {
                    return View("GetPreferences");
                }
            }
            UserToCuisine userToCuisineToAdd = new UserToCuisine { UserId = userId, CuisineName = cuisineToAdd };
            db.UserToCuisines.Add(userToCuisineToAdd);
            db.SaveChanges();
            return RedirectToAction("GetPreferences", "Enjoyer");
        }

        public ActionResult RemovePreferences(string cuisineToRemove, string userId)
        {
            foreach (UserToCuisine element in db.UserToCuisines)
            {
                if (element.UserId == userId && element.CuisineName == cuisineToRemove)
                {
                    //UserToCuisine userToCuisineToRemove = new UserToCuisine { UserId = userId, CuisineName = cuisineToRemove };
                    db.UserToCuisines.Remove(element);
                    break;                   
                }
            }
            db.SaveChanges();
            return RedirectToAction("GetPreferences", "Enjoyer");
        }
    }
}