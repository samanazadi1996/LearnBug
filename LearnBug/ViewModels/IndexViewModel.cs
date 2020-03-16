using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnBug.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<LearnBug.Models.DomainModels.Content> Contents { get; set; }
        public LearnBug.Models.DomainModels.User User { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItemCount { get; set; }
        public int UsersCount { get; set; }

    }
}