namespace WhatDo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class misc : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FoodSuggestions", "Latitude", c => c.String());
            AlterColumn("dbo.FoodSuggestions", "Longitude", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FoodSuggestions", "Longitude", c => c.Double(nullable: false));
            AlterColumn("dbo.FoodSuggestions", "Latitude", c => c.Double(nullable: false));
        }
    }
}
