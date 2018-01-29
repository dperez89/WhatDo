namespace WhatDo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializingLatestDatabaseVersion : DbMigration
    {
        public override void Up()
        {
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
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        FriendsListId = c.Int(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FriendsLists", t => t.FriendsListId)
                .Index(t => t.FriendsListId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
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
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
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
                        CuisineName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserToGenres", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserToGenres", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.UserToCuisines", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ShowTimeResults", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.MovieSuggestions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendsListInvites", "InvitedUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendsListInvites", "FriendsListId", "dbo.FriendsLists");
            DropForeignKey("dbo.FoodSuggestions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "FriendsListId", "dbo.FriendsLists");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserToGenres", new[] { "GenreId" });
            DropIndex("dbo.UserToGenres", new[] { "UserId" });
            DropIndex("dbo.UserToCuisines", new[] { "UserId" });
            DropIndex("dbo.ShowTimeResults", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.MovieSuggestions", new[] { "UserId" });
            DropIndex("dbo.FriendsListInvites", new[] { "InvitedUserId" });
            DropIndex("dbo.FriendsListInvites", new[] { "FriendsListId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "FriendsListId" });
            DropIndex("dbo.FoodSuggestions", new[] { "UserId" });
            DropTable("dbo.UserToGenres");
            DropTable("dbo.UserToCuisines");
            DropTable("dbo.ShowTimeResults");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.MovieSuggestions");
            DropTable("dbo.Genres");
            DropTable("dbo.FriendsListInvites");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.FriendsLists");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.FoodSuggestions");
        }
    }
}
