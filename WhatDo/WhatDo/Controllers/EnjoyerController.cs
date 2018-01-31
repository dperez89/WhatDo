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
            return View(preferencesViewModel);
        }
       
        public ActionResult AddCuisinePreferences(string cuisineToAdd, string userId)
        {
            foreach (UserToCuisine element in db.UserToCuisines)
            {
                if (element.UserId == userId && element.CuisineName == cuisineToAdd)
                {
                    return View("GetCuisinePreferences");
                }
            }
            UserToCuisine userToCuisineToAdd = new UserToCuisine { UserId = userId, CuisineName = cuisineToAdd };
            db.UserToCuisines.Add(userToCuisineToAdd);
            db.SaveChanges();
            return RedirectToAction("GetCuisinePreferences", "Enjoyer");
        }

        public ActionResult RemoveCuisinePreferences(string cuisineToRemove, string userId)
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
            return RedirectToAction("GetCuisinePreferences", "Enjoyer");
        }
    }
}