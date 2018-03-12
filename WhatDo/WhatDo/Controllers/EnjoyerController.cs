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
            FriendsListViewModel friendsListModel = new FriendsListViewModel();
            var currentUser = db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
            foreach (UserToFriendsList invite in db.UserToFriendsLists)
            {
                if(invite.UserId == currentUser.Id && invite.IsAccepted == false && invite.IsDenied == false)
                {
                    friendsListModel.Invites.Add(invite);
                }
            }
            //foreach (UserToFriendsList row in db.UserToFriendsLists)
            //{
            //    if(row.FriendsListId == currentUser.FriendsListId && row.IsAccepted == true)
            //    {
            //        friendsListModel.FriendsList.Add(row.User);
            //    }
            //}
            return View(friendsListModel);
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
        public ActionResult GetShowtimes(Movies selectedMovie)
        {
            ShowTimeSearchViewModel showTimeSearchModel = new ShowTimeSearchViewModel();
            var currentUser = db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
            showTimeSearchModel.User = currentUser;

            var getIShowTimeCityIdClient = new WebClient();
            getIShowTimeCityIdClient.Headers.Add("X-API-Key", "aOpKxmKJDaHhhhg7IgRUISKvK4gMVJxx");
            var getIShowTimeCityIdResponse = getIShowTimeCityIdClient.DownloadString("https://api.internationalshowtimes.com/v4/cities?query=" + currentUser.City);
            var getIShowTimeCityResults = new JavaScriptSerializer().Deserialize<IShowTimeCityResultResponse>(getIShowTimeCityIdResponse);
            string cityId = getIShowTimeCityResults.Cities[0].Id;

            var showTimeSearchClient = new WebClient();
            showTimeSearchClient.Headers.Add("X-API-Key", "aOpKxmKJDaHhhhg7IgRUISKvK4gMVJxx");
            var showTimeSearchResponse = showTimeSearchClient.DownloadString("https://api.internationalshowtimes.com/v4/showtimes?movie_id=" + selectedMovie.Id + "&city_ids=" + cityId + "&all_fields=true");
            var showTimeSearchResults = new JavaScriptSerializer().Deserialize<ShowTimeSearchResponse>(showTimeSearchResponse);
            
            foreach(Showtimes showtime in showTimeSearchResults.Showtimes)
            {
                showTimeSearchModel.ShowtimeResults.Add(showtime);
            }

            var resolveCinemaIdClient = new WebClient();
            resolveCinemaIdClient.Headers.Add("X-API-Key", "aOpKxmKJDaHhhhg7IgRUISKvK4gMVJxx");
            var resolveCinemaResponse = resolveCinemaIdClient.DownloadString("https://api.internationalshowtimes.com/v4/cinemas/" + showTimeSearchModel.ShowtimeResults.First().Cinema_Id);
            var resolveCinemaResult = new JavaScriptSerializer().Deserialize<CinemaSearchResponse>(resolveCinemaResponse);
            showTimeSearchModel.ResolvedCinema = resolveCinemaResult.Cinema;
            showTimeSearchModel.Movie = selectedMovie;                                 
            return View(showTimeSearchModel);


        }
        // GET
        public ActionResult ManageFriends(FriendsListViewModel model)
        {
            FriendsListViewModel friendsListModel = new FriendsListViewModel();
            var currentUser = db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
            foreach (UserToFriendsList invite in db.UserToFriendsLists)
            {
                if (invite.UserId == currentUser.Id && invite.IsAccepted == false && invite.IsDenied == false)
                {
                    friendsListModel.Invites.Add(invite);
                }
            }
            foreach(UserToFriendsList invite in friendsListModel.Invites)
            {
                foreach(ApplicationUser user in db.Users)
                {
                    if(user.FriendsListId == invite.FriendsListId)
                    {
                        friendsListModel.InvitingUserNames.Add(user.UserName);
                    }
                }
            }
            foreach (UserToFriendsList row in db.UserToFriendsLists)
            {
                if (row.FriendsListId == currentUser.FriendsListId && row.IsAccepted == true)
                {
                    friendsListModel.FriendsList.Add(row.User);
                }
            }
            return View(friendsListModel);
        }

        // GET
        public ActionResult InviteFriend(FriendsListViewModel model)
        {
            model.UserToFindIsFound = false;
            var currentUser = db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
            UserToFriendsList newFriendRequest = new UserToFriendsList();
            foreach (ApplicationUser user in db.Users)
            {
                if(user.UserName == model.UserToFind)
                {
                    model.UserHasAttemptedASearch = true;
                    model.UserToFindIsFound = true;
                    newFriendRequest.FriendsListId = (int)currentUser.FriendsListId;
                    newFriendRequest.UserId = user.Id;
                    newFriendRequest.IsAccepted = false;
                    newFriendRequest.IsDenied = false;
                    break;
                }
            }
            if(model.UserHasAttemptedASearch == true && model.UserToFindIsFound == true)
            {
                db.UserToFriendsLists.Add(newFriendRequest);
                db.SaveChanges();
                return View("ManageFriends", model);
            }            
            return View("ManageFriends", model);
        }

        // GET
        [HttpGet]
        public ActionResult GetMovieSuggestions()
        {
            MovieSearchViewModel movieSearchModel = new MovieSearchViewModel();
            var currentUser = db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
            movieSearchModel.User = currentUser;

            var getIShowTimeCityIdClient = new WebClient();
            getIShowTimeCityIdClient.Headers.Add("X-API-Key", "aOpKxmKJDaHhhhg7IgRUISKvK4gMVJxx");
            var getIShowTimeCityIdResponse = getIShowTimeCityIdClient.DownloadString("https://api.internationalshowtimes.com/v4/cities?query=" + movieSearchModel.User.City);
            var getIShowTimeCityResults = new JavaScriptSerializer().Deserialize<IShowTimeCityResultResponse>(getIShowTimeCityIdResponse);
            movieSearchModel.CityId = getIShowTimeCityResults.Cities[0].Id;

            foreach (UserToGenre genre in db.UserToGenres)
            {
                if(genre.UserId == movieSearchModel.User.Id)
                {
                    string genreIdToAdd = genre.GenreId.ToString();
                    movieSearchModel.PreferredGenres.Add(genreIdToAdd);
                }
            }
            foreach (string genreId in movieSearchModel.PreferredGenres)
            {
                foreach (Genre genre in db.Genres)
                {
                    if (genreId == genre.Id.ToString())
                    {
                        string genreDatabaseIdToAdd = genre.DatabaseId;
                        movieSearchModel.ResolvedPreferredGenreIds.Add(genreDatabaseIdToAdd);
                    }
                }
            }
            movieSearchModel.ResolvedGenreIdsToSearch = movieSearchModel.ResolvedPreferredGenreIds.First();
            if (movieSearchModel.ResolvedPreferredGenreIds.Count > 1)
            {
                movieSearchModel.ResolvedGenreIdsToSearch = string.Join(",", movieSearchModel.ResolvedPreferredGenreIds);
            }
            var movieSearchClient = new WebClient();
            movieSearchClient.Headers.Add("X-API-Key", "aOpKxmKJDaHhhhg7IgRUISKvK4gMVJxx");
            var movieSearchResponse = movieSearchClient.DownloadString("https://api.internationalshowtimes.com/v4/movies?genre_ids="+movieSearchModel.ResolvedGenreIdsToSearch+"&city_ids="+movieSearchModel.CityId+"&all_fields=true");
            var movieSearchResults = new JavaScriptSerializer().Deserialize<MovieResultResponse>(movieSearchResponse);

            foreach (Movies movie in movieSearchResults.Movies)
            {
                movieSearchModel.MovieSearchResults.Add(movie);
            }

            return View(movieSearchModel);

        }

        // GET
        [HttpGet]
        public ActionResult GetFoodSuggestions()
        {
            RestaurantSearchViewModel restaurantSearchModel = new RestaurantSearchViewModel();
            var currentUser = db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
            restaurantSearchModel.User = currentUser;
            var geoCodeClient = new WebClient();
            geoCodeClient.Headers.Add("X-API-Key", "aOpKxmKJDaHhhhg7IgRUISKvK4gMVJxx");
            var geoCodeResponse = geoCodeClient.DownloadString("https://api.internationalshowtimes.com/v4/cities?query=" + restaurantSearchModel.User.City);
            var geoCodeResults = new JavaScriptSerializer().Deserialize<IShowTimeCityResultResponse>(geoCodeResponse);
            string lat = geoCodeResults.Cities[0].Lat;
            string lon = geoCodeResults.Cities[0].Lon;

            foreach (UserToCuisine cuisine in db.UserToCuisines)
            {
                if (cuisine.UserId == restaurantSearchModel.User.Id)
                {
                    restaurantSearchModel.CuisineIdsToSearch.Add(cuisine.CuisineId);
                }
            }
            restaurantSearchModel.ResolvedCuisineIdsToSearch = restaurantSearchModel.CuisineIdsToSearch.First();
            if (restaurantSearchModel.CuisineIdsToSearch.Count > 1)
            {
                restaurantSearchModel.ResolvedCuisineIdsToSearch = string.Join(",", restaurantSearchModel.CuisineIdsToSearch);
            }            
            var restaurantSearchClient = new WebClient();
            restaurantSearchClient.Headers.Add("user-key", "d846616ebd6c5c018f6cd8fd36a6fb68");
            var restaurantSearchResponse = restaurantSearchClient.DownloadString("https://developers.zomato.com/api/v2.1/search?lat=" + lat + "&lon=" + lon + "&cuisines=" + restaurantSearchModel.ResolvedCuisineIdsToSearch + "");
            var restaurantSearchResults = new JavaScriptSerializer().Deserialize<RestaurantResultResponse>(restaurantSearchResponse);

            for (int i = 0; i < 20; i++)
            {
                restaurantSearchResults.Restaurants[i].Restaurant.Address = restaurantSearchResults.Restaurants[i].Restaurant.Location.Address;
                restaurantSearchResults.Restaurants[i].Restaurant.Locality = restaurantSearchResults.Restaurants[i].Restaurant.Location.Locality;
                restaurantSearchResults.Restaurants[i].Restaurant.City = restaurantSearchResults.Restaurants[i].Restaurant.Location.City;
                restaurantSearchResults.Restaurants[i].Restaurant.State = restaurantSearchResults.Restaurants[i].Restaurant.Location.State;
                restaurantSearchResults.Restaurants[i].Restaurant.Latitude = restaurantSearchResults.Restaurants[i].Restaurant.Location.Latitude;
                restaurantSearchResults.Restaurants[i].Restaurant.Longitude = restaurantSearchResults.Restaurants[i].Restaurant.Location.Longitude;
                restaurantSearchResults.Restaurants[i].Restaurant.Zipcode = restaurantSearchResults.Restaurants[i].Restaurant.Location.Zipcode;
                restaurantSearchResults.Restaurants[i].Restaurant.Country_Id = restaurantSearchResults.Restaurants[i].Restaurant.Location.Country_Id;
                restaurantSearchModel.RestaurantResults.Add(restaurantSearchResults.Restaurants[i].Restaurant);
            }
            return View(restaurantSearchModel);
        }       

        public ActionResult AcceptFoodSuggestion(string restaurantId, string name, string url, string address, string locality, string city, string state, string latitude, string longitude, string zipcode, string country_id, string cuisines, string average_cost_for_two, string price_range)
        {
            GetRestaurantViewModel getRestaurantModel = new GetRestaurantViewModel();
            int restaurantIdNumber;
            var result = Int32.TryParse(restaurantId, out restaurantIdNumber);
            FoodSuggestion foodSuggestionToRecord = new FoodSuggestion
            {
                RestaurantId = restaurantIdNumber,
                Name = name,
                Address = address,
                City = city,
                ZipCode = zipcode,
                Latitude = latitude,
                Longitude = longitude,
                IsChosenByUser = true
            };
            db.FoodSuggestions.Add(foodSuggestionToRecord);
            db.SaveChanges();
            Location location = new Location { Address = address, Locality = locality, City = city, State = state, Latitude = latitude, Longitude = longitude, Zipcode = zipcode, Country_Id = country_id };
            Restaurant chosenRestaurant = new Restaurant
            {
                Id = restaurantId,
                Name = name,
                Url = url,
                Address = address,
                Locality = locality,
                City = city,
                State = state,
                Latitude = latitude,
                Longitude = longitude,
                Zipcode = zipcode,
                Country_Id = country_id,
                Cuisines = cuisines,
                Average_Cost_For_Two = average_cost_for_two,
                Price_Range = price_range
            };
            getRestaurantModel.Restaurant = chosenRestaurant;

            return View("GetRestaurant", getRestaurantModel);
        }

        public ActionResult GetRestaurant(List<Restaurant> restaurantResults)
        {
            GetRestaurantViewModel getRestaurantModel = new GetRestaurantViewModel();
            getRestaurantModel.Restaurant = restaurantResults[0];

            return View(getRestaurantModel);
        }
       
    }
}