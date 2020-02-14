using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnBug.ViewModels
{
    public class myMessageViewModel
    {
        public IEnumerable<LearnBug.Models.DomainModels.Message> messages { get; set; }
        public IEnumerable<LearnBug.Models.DomainModels.User> Users { get; set; }

    }
}