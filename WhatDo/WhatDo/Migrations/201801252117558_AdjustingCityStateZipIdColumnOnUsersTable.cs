namespace WhatDo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdjustingCityStateZipIdColumnOnUsersTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "CityStateZipId", "dbo.CityStateZips");
            DropIndex("dbo.AspNetUsers", new[] { "CityStateZipId" });
            AlterColumn("dbo.AspNetUsers", "CityStateZipId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "CityStateZipId");
            AddForeignKey("dbo.AspNetUsers", "CityStateZipId", "dbo.CityStateZips", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "CityStateZipId", "dbo.CityStateZips");
            DropIndex("dbo.AspNetUsers", new[] { "CityStateZipId" });
            AlterColumn("dbo.AspNetUsers", "CityStateZipId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "CityStateZipId");
            AddForeignKey("dbo.AspNetUsers", "CityStateZipId", "dbo.CityStateZips", "Id", cascadeDelete: true);
        }
    }
}
