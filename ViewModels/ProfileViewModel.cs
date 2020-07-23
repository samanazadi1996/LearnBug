using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class ProfileViewModel
    {
        public User User { get; set; }
        public IEnumerable<User> mutualFollower { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}