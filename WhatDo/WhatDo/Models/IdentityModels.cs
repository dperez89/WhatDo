﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatDo.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public int? CityStateZipId { get; set; }
        [ForeignKey("CityStateZipId")]
        public CityStateZip CityStateZip { get; set; }
        public int? FriendsListId { get; set; }
        [ForeignKey("FriendsListId")]
        public FriendsList FriendsList { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<CityStateZip> CityStateZips { get; set; }
        public DbSet<Cuisine> Cuisines { get; set; }
        public DbSet<FoodSuggestion> FoodSuggestions { get; set; }
        public DbSet<FriendsList> FriendsLists { get; set; }
        public DbSet<FriendsListInvite> FriendsListInvites { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieSuggestion> MovieSuggestions { get; set; }
        public DbSet<ShowTimeResults> ShowTimeResults { get; set; }
        public DbSet<UserToCuisine> UserToCuisines { get; set; }
        public DbSet<UserToGenre> UserToGenres { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}