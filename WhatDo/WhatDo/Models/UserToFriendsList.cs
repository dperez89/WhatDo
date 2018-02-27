using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WhatDo.Models
{
    public class UserToFriendsList
    {
        public int Id { get; set; }
        public int FriendsListId { get; set; }
        [ForeignKey("FriendsListId")]
        public FriendsList FriendsList { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsDenied { get; set; }

    }
}