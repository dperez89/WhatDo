namespace WhatDo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddinginUserToFriendsListTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserToFriendsLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FriendsListId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        IsAccepted = c.Boolean(nullable: false),
                        IsDenied = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FriendsLists", t => t.FriendsListId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.FriendsListId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserToFriendsLists", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserToFriendsLists", "FriendsListId", "dbo.FriendsLists");
            DropIndex("dbo.UserToFriendsLists", new[] { "UserId" });
            DropIndex("dbo.UserToFriendsLists", new[] { "FriendsListId" });
            DropTable("dbo.UserToFriendsLists");
        }
    }
}
