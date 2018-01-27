namespace WhatDo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedDBOCuisinesToDBODatabaseCuisine : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Cuisines", newName: "DatabaseCuisines");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.DatabaseCuisines", newName: "Cuisines");
        }
    }
}
