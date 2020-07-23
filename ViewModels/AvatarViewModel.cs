using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class AvatarViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public bool HasBookmark { get; set; }
        public bool HasFactor { get; set; }
        public bool HasPost { get; set; }
        public int CountOfFollower { get; set; }
        public int CountOfPosts { get; set; }
        public int CountOfFollowing { get; set; }
        public double Wallet { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; set; }

    }
}