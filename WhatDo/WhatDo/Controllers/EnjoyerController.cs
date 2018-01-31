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

        // GET:
        [HttpGet]
        public ActionResult GetGenrePreferences()
        {
            GetPreferencesViewModel preferencesViewModel = new GetPreferencesViewModel();
            var currentUser = db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
            preferencesViewModel.User = currentUser;
            foreach (Genre genre in db.Genres)
            {
                string genreNameToAdd = genre.Name;
                preferencesViewModel.AvailableGenres.Add(genreNameToAdd);
            }
            foreach (UserToGenre genre in db.UserToGenres)
            {
                for (int i = 0; i <= preferencesViewModel.AvailableGenres.Count-1; i++)
                {
                    if (genre.UserId == preferencesViewModel.User.Id && genre.Genre.Name == preferencesViewModel.AvailableGenres[i])
                    {
                        preferencesViewModel.AvailableGenres.RemoveAt(i);
                        i--;
                    }
                }
            }
            foreach (UserToGenre genre in db.UserToGenres)
            {
                if(genre.UserId == preferencesViewModel.User.Id)
                {
                    preferencesViewModel.PreferredGenres.Add(genre.Genre.Name);
                }
            }
            return View(preferencesViewModel);
        }

        public ActionResult AddGenrePreferences(string genreToAdd, string userId)
        {
            int genreToAddId = (from genre in db.Genres where genre.Name == genreToAdd select genre.Id).First();
            foreach (UserToGenre element in db.UserToGenres)
            {
                if (element.UserId == userId && element.GenreId == genreToAddId)
                {
                    return View("GetGenrePreferences");
                }
            }            
            UserToGenre userToGenreToAdd = new UserToGenre { UserId = userId, GenreId = genreToAddId };
            db.UserToGenres.Add(userToGenreToAdd);
            db.SaveChanges();
            return RedirectToAction("GetGenrePreferences", "Enjoyer");
        }

        public ActionResult RemoveGenrePreferences(string genreToRemove, string userId)
        {
            int genreToRemoveId = (from genre in db.Genres where genre.Name == genreToRemove select genre.Id).FirstOrDefault();
            foreach (UserToGenre element in db.UserToGenres)
            {
                if (element.UserId == userId && element.GenreId == genreToRemoveId)
                {
                    //UserToCuisine userToCuisineToRemove = new UserToCuisine { UserId = userId, CuisineName = cuisineToRemove };
                    db.UserToGenres.Remove(element);
                    break;
                }
            }
            db.SaveChanges();
            return RedirectToAction("GetGenrePreferences", "Enjoyer");
        }

        // GET
        [HttpGet]
        public ActionResult GetCuisinePreferences()
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
                Cuisine cuisineToAdd = cuisine.Cuisine;
                preferencesViewModel.AvailableCuisines.Add(cuisineToAdd);
            }
            foreach (UserToCuisine cuisine in db.UserToCuisines)
            {
                for (int i = 0; i <= preferencesViewModel.AvailableCuisines.Count-1; i++)
                {
                    if (cuisine.UserId == preferencesViewModel.User.Id && cuisine.CuisineName == preferencesViewModel.AvailableCuisines[i].Cuisine_Name)
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
                    preferencesViewModel.PreferredCuisines.Add(cuisine);
                }
            }            
            return View(preferencesViewModel);
        }
       
        public ActionResult AddCuisinePreferences(string cuisineNameToAdd, string userId, string cuisineIdToAdd)
        {
            foreach (UserToCuisine element in db.UserToCuisines)
            {
                if (element.UserId == userId && element.CuisineName == cuisineNameToAdd)
                {
                    return View("GetCuisinePreferences");
                }
            }
            UserToCuisine userToCuisineToAdd = new UserToCuisine { UserId = userId, CuisineName = cuisineNameToAdd, CuisineId = cuisineIdToAdd };
            db.UserToCuisines.Add(userToCuisineToAdd);
            db.SaveChanges();
            return RedirectToAction("GetCuisinePreferences", "Enjoyer");
        }

        public ActionResult RemoveCuisinePreferences(string cuisineNameToRemove, string userId, string cuisineIdToRemove)
        {
            foreach (UserToCuisine element in db.UserToCuisines)
            {
                if (element.UserId == userId && element.CuisineName == cuisineNameToRemove && element.CuisineId == cuisineIdToRemove)
                {
                    //UserToCuisine userToCuisineToRemove = new UserToCuisine { UserId = userId, CuisineName = cuisineToRemove };
                    db.UserToCuisines.Remove(element);
                    break;                   
                }
            }
            db.SaveChanges();
            return RedirectToAction("GetCuisinePreferences", "Enjoyer");
        }

        // GET
        [HttpGet]
        public void GetFoodSuggestions()
        {
            SearchResultsViewModel searchResultsViewModel = new SearchResultsViewModel();
            var currentUser = db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
            searchResultsViewModel.User = currentUser;
            string currentUserCity = cityIdResolver.Resolve(currentUser);
            var geoCodeClient = new WebClient();
            geoCodeClient.Headers.Add("X-API-Key", "aOpKxmKJDaHhhhg7IgRUISKvK4gMVJxx");
            var geoCodeResponse = geoCodeClient.DownloadString("https://api.internationalshowtimes.com/v4/cities?query=" + currentUserCity);
            var geoCodeResults = new JavaScriptSerializer().Deserialize<GeoCodeResponse>(geoCodeResponse);
            string lat = geoCodeResults.Cities[0].Lat;
            string lon = geoCodeResults.Cities[0].Lon;
            var restaurantSearchClient = new WebClient();
            restaurantSearchClient.Headers.Add("user-key", "d846616ebd6c5c018f6cd8fd36a6fb68");
            var restaurantSearchResponse = restaurantSearchClient.DownloadString("https://developers.zomato.com/api/v2.1/search?lat=" + lat + "&" + lon + "&cuisines=" + "");

        }
    }
}