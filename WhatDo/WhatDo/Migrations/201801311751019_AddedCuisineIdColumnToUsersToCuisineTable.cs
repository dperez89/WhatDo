namespace WhatDo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCuisineIdColumnToUsersToCuisineTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserToCuisines", "CuisineId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserToCuisines", "CuisineId");
        }
    }
}
