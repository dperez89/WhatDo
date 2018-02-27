namespace WhatDo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingdboFriendsListInvitesfromdatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FriendsListInvites", "FriendsListId", "dbo.FriendsLists");
            DropForeignKey("dbo.FriendsListInvites", "InvitedUserId", "dbo.AspNetUsers");
            DropIndex("dbo.FriendsListInvites", new[] { "FriendsListId" });
            DropIndex("dbo.FriendsListInvites", new[] { "InvitedUserId" });
            DropTable("dbo.FriendsListInvites");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FriendsListInvites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FriendsListId = c.Int(nullable: false),
                        InvitedUserId = c.String(maxLength: 128),
                        IsAccepted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.FriendsListInvites", "InvitedUserId");
            CreateIndex("dbo.FriendsListInvites", "FriendsListId");
            AddForeignKey("dbo.FriendsListInvites", "InvitedUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.FriendsListInvites", "FriendsListId", "dbo.FriendsLists", "Id", cascadeDelete: true);
        }
    }
}
