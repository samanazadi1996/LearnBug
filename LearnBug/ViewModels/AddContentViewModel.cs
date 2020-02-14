using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnBug.ViewModels
{
    public class AddContentViewModel
    {
        public IEnumerable<LearnBug.Models.DomainModels.Group> Groups { get; set; }
        public LearnBug.Models.DomainModels.Content Content { get; set; }
    }
}