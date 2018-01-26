using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WhatDo.Models
{
    public class FriendsListInvite
    {
        public int Id { get; set; }
        public int FriendsListId { get; set; }
        [ForeignKey("FriendsListId")]
        public FriendsList FriendsList { get; set; }
        public string InvitedUserId { get; set; }
        [ForeignKey("InvitedUserId")]
        public ApplicationUser InvitedUser { get; set; }
        public bool IsAccepted { get; set; }
    }
}