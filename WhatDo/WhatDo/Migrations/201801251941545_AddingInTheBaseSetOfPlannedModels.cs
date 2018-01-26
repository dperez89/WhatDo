namespace WhatDo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingInTheBaseSetOfPlannedModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CityStateZips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cuisines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FoodSuggestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        RestaurantId = c.Int(nullable: false),
                        Name = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        ZipCode = c.String(),
                        PhoneNumber = c.String(),
                        Cuisine = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        AverageCostForTwo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RatingText = c.String(),
                        NumberOfVotes = c.Int(nullable: false),
                        HasOnlineDelivery = c.Boolean(nullable: false),
                        IsGoodSuggestion = c.Boolean(nullable: false),
                        IsChosenByUser = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.FriendsLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FriendsListInvites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FriendsListId = c.Int(nullable: false),
                        InvitedUserId = c.String(maxLength: 128),
                        IsAccepted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FriendsLists", t => t.FriendsListId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.InvitedUserId)
                .Index(t => t.FriendsListId)
                .Index(t => t.InvitedUserId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MovieSuggestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        MovieId = c.String(),
                        Title = c.String(),
                        Synopsis = c.String(),
                        RunTime = c.Int(nullable: false),
                        Genres = c.String(),
                        Website = c.String(),
                        Rating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReleaseDate = c.String(),
                        ImageUrl = c.String(),
                        IsGoodSuggestion = c.Boolean(nullable: false),
                        IsChosenByUser = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ShowTimeResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        MovieId = c.String(),
                        CinemaId = c.String(),
                        StartTime = c.String(),
                        CinemaName = c.String(),
                        Website = c.String(),
                        PhoneNumber = c.String(),
                        Longitude = c.Double(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Street = c.String(),
                        HouseNumber = c.String(),
                        ZipCode = c.String(),
                        City = c.String(),
                        State = c.String(),
                        StateAbbr = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserToCuisines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        CuisineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cuisines", t => t.CuisineId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CuisineId);
            
            CreateTable(
                "dbo.UserToGenres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        GenreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.GenreId);
            
            AddColumn("dbo.AspNetUsers", "CityStateZipId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "FriendsListId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "CityStateZipId");
            CreateIndex("dbo.AspNetUsers", "FriendsListId");
            AddForeignKey("dbo.AspNetUsers", "CityStateZipId", "dbo.CityStateZips", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUsers", "FriendsListId", "dbo.FriendsLists", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserToGenres", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserToGenres", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.UserToCuisines", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserToCuisines", "CuisineId", "dbo.Cuisines");
            DropForeignKey("dbo.ShowTimeResults", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MovieSuggestions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendsListInvites", "InvitedUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendsListInvites", "FriendsListId", "dbo.FriendsLists");
            DropForeignKey("dbo.FoodSuggestions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "FriendsListId", "dbo.FriendsLists");
            DropForeignKey("dbo.AspNetUsers", "CityStateZipId", "dbo.CityStateZips");
            DropIndex("dbo.UserToGenres", new[] { "GenreId" });
            DropIndex("dbo.UserToGenres", new[] { "UserId" });
            DropIndex("dbo.UserToCuisines", new[] { "CuisineId" });
            DropIndex("dbo.UserToCuisines", new[] { "UserId" });
            DropIndex("dbo.ShowTimeResults", new[] { "UserId" });
            DropIndex("dbo.MovieSuggestions", new[] { "UserId" });
            DropIndex("dbo.FriendsListInvites", new[] { "InvitedUserId" });
            DropIndex("dbo.FriendsListInvites", new[] { "FriendsListId" });
            DropIndex("dbo.AspNetUsers", new[] { "FriendsListId" });
            DropIndex("dbo.AspNetUsers", new[] { "CityStateZipId" });
            DropIndex("dbo.FoodSuggestions", new[] { "UserId" });
            DropColumn("dbo.AspNetUsers", "FriendsListId");
            DropColumn("dbo.AspNetUsers", "CityStateZipId");
            DropTable("dbo.UserToGenres");
            DropTable("dbo.UserToCuisines");
            DropTable("dbo.ShowTimeResults");
            DropTable("dbo.MovieSuggestions");
            DropTable("dbo.Genres");
            DropTable("dbo.FriendsListInvites");
            DropTable("dbo.FriendsLists");
            DropTable("dbo.FoodSuggestions");
            DropTable("dbo.Cuisines");
            DropTable("dbo.CityStateZips");
        }
    }
}
