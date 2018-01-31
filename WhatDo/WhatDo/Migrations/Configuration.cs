namespace WhatDo.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WhatDo.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WhatDo.Models.ApplicationDbContext context)
        {
            context.Genres.AddOrUpdate(
                g => g.Name,
                new Models.Genre { Name = "Action" },
                new Models.Genre { Name = "Adventure" },
                new Models.Genre { Name = "Animation" },
                new Models.Genre { Name = "Comedy" },
                new Models.Genre { Name = "Crime" },
                new Models.Genre { Name = "Documentary" },
                new Models.Genre { Name = "Drama" },
                new Models.Genre { Name = "Family" },
                new Models.Genre { Name = "Fantasy" },
                new Models.Genre { Name = "History" },
                new Models.Genre { Name = "Horror" },
                new Models.Genre { Name = "Music" },
                new Models.Genre { Name = "Mystery" },
                new Models.Genre { Name = "Romance" },
                new Models.Genre { Name = "Science Fiction" },
                new Models.Genre { Name = "TV Movie" },
                new Models.Genre { Name = "Thriller" },
                new Models.Genre { Name = "War" },
                new Models.Genre { Name = "Western" });
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
