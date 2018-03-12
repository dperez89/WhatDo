using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatDo.Models
{
    public class FriendsListViewModel
    {
        public string UserToFind { get; set; }
        public bool UserToFindIsFound { get; set; }
        public bool UserHasAttemptedASearch { get; set; }
        public List<ApplicationUser> FriendsList { get; set; }
        public List<UserToFriendsList> Invites { get; set; }
        public List<string> InvitingUserNames { get; set; }

        public FriendsListViewModel()
        {
            FriendsList = new List<ApplicationUser>();
            Invites = new List<UserToFriendsList>();
            InvitingUserNames = new List<string>();
            

        }
    }
}