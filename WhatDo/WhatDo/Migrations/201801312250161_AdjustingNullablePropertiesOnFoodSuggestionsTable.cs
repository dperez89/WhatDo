namespace WhatDo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdjustingNullablePropertiesOnFoodSuggestionsTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FoodSuggestions", "RestaurantId", c => c.Int());
            AlterColumn("dbo.FoodSuggestions", "AverageCostForTwo", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.FoodSuggestions", "UserRating", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.FoodSuggestions", "NumberOfVotes", c => c.Int());
            AlterColumn("dbo.FoodSuggestions", "HasOnlineDelivery", c => c.Boolean());
            AlterColumn("dbo.FoodSuggestions", "IsGoodSuggestion", c => c.Boolean());
            AlterColumn("dbo.FoodSuggestions", "IsChosenByUser", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FoodSuggestions", "IsChosenByUser", c => c.Boolean(nullable: false));
            AlterColumn("dbo.FoodSuggestions", "IsGoodSuggestion", c => c.Boolean(nullable: false));
            AlterColumn("dbo.FoodSuggestions", "HasOnlineDelivery", c => c.Boolean(nullable: false));
            AlterColumn("dbo.FoodSuggestions", "NumberOfVotes", c => c.Int(nullable: false));
            AlterColumn("dbo.FoodSuggestions", "UserRating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.FoodSuggestions", "AverageCostForTwo", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.FoodSuggestions", "RestaurantId", c => c.Int(nullable: false));
        }
    }
}
