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
                new Models.Genre { Name = "Action", DatabaseId = "0" },
                new Models.Genre { Name = "Adventure", DatabaseId = "1" },
                new Models.Genre { Name = "Animation", DatabaseId = "2" },
                new Models.Genre { Name = "Comedy", DatabaseId = "3" },
                new Models.Genre { Name = "Crime", DatabaseId = "4" },
                new Models.Genre { Name = "Documentary", DatabaseId = "5" },
                new Models.Genre { Name = "Drama", DatabaseId = "6" },
                new Models.Genre { Name = "Family", DatabaseId = "7" },
                new Models.Genre { Name = "Fantasy", DatabaseId = "8" },
                new Models.Genre { Name = "Foreign", DatabaseId = "9" },
                new Models.Genre { Name = "History", DatabaseId = "10" },
                new Models.Genre { Name = "Horror", DatabaseId = "11" },
                new Models.Genre { Name = "Music", DatabaseId = "12" },
                new Models.Genre { Name = "Mystery", DatabaseId = "13" },
                new Models.Genre { Name = "Romance", DatabaseId = "14" },
                new Models.Genre { Name = "Science Fiction", DatabaseId = "15" },
                new Models.Genre { Name = "TV Movie", DatabaseId = "16" },
                new Models.Genre { Name = "Thriller", DatabaseId = "17" },
                new Models.Genre { Name = "War", DatabaseId = "18" },
                new Models.Genre { Name = "Western", DatabaseId = "19" }); 
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
