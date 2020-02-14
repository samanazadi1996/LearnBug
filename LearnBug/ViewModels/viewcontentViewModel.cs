using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnBug.ViewModels
{
    public class viewcontentViewModel
    {
        public IEnumerable<LearnBug.Models.DomainModels.Comment> Comments { get; set; }
        public LearnBug.Models.DomainModels.Content Content { get; set; }
        public LearnBug.Models.DomainModels.Group Group { get; set; } 
        public IEnumerable<LearnBug.Models.DomainModels.User> Users { get; set; }
    }
}