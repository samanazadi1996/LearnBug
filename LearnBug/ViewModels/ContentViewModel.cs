using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnBug.ViewModels
{
    public class ContentViewModel
    {
        public IQueryable<Models.DomainModels.Content> Contents { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItemCount { get; set; }

    }
}