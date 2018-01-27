namespace WhatDo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingCityStateZipTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "CityStateZipId", "dbo.CityStateZips");
            DropIndex("dbo.AspNetUsers", new[] { "CityStateZipId" });
            AddColumn("dbo.AspNetUsers", "City", c => c.String());
            AddColumn("dbo.AspNetUsers", "State", c => c.String());
            AddColumn("dbo.AspNetUsers", "Zip", c => c.String());
            DropColumn("dbo.AspNetUsers", "CityStateZipId");
            DropTable("dbo.CityStateZips");
        }
        
        public override void Down()
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
            
            AddColumn("dbo.AspNetUsers", "CityStateZipId", c => c.Int());
            DropColumn("dbo.AspNetUsers", "Zip");
            DropColumn("dbo.AspNetUsers", "State");
            DropColumn("dbo.AspNetUsers", "City");
            CreateIndex("dbo.AspNetUsers", "CityStateZipId");
            AddForeignKey("dbo.AspNetUsers", "CityStateZipId", "dbo.CityStateZips", "Id");
        }
    }
}
