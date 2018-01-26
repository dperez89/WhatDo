namespace WhatDo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingFriendsListIdOnUsersTableToNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "FriendsListId", "dbo.FriendsLists");
            DropIndex("dbo.AspNetUsers", new[] { "FriendsListId" });
            AlterColumn("dbo.AspNetUsers", "FriendsListId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "FriendsListId");
            AddForeignKey("dbo.AspNetUsers", "FriendsListId", "dbo.FriendsLists", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "FriendsListId", "dbo.FriendsLists");
            DropIndex("dbo.AspNetUsers", new[] { "FriendsListId" });
            AlterColumn("dbo.AspNetUsers", "FriendsListId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "FriendsListId");
            AddForeignKey("dbo.AspNetUsers", "FriendsListId", "dbo.FriendsLists", "Id", cascadeDelete: true);
        }
    }
}
