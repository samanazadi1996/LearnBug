using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnBug.ViewModels
{
    public class myContentsViewModel
    {
        public IEnumerable<LearnBug.Models.DomainModels.Comment> comments { get; set; }
        public IEnumerable<LearnBug.Models.DomainModels.Content>  contents { get; set; }
        public IEnumerable<LearnBug.Models.DomainModels.Group> groups { get; set; }
    }
}