using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models.Entities;
namespace ViewModels
{
    public class PostsViewModel
    {
        public IQueryable<int> postId { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItemCount { get; set; }

    }
}