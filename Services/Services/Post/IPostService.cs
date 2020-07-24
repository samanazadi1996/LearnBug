using Models.Entities;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace Services
{
    public interface IPostService
    {
        bool Create(Post post);
        bool Delete(Post post);
        bool Edit(Post post);
        IEnumerable<Post> GetAllPosts();
        PostsViewModel GetMyPosts(int Page = 1);
        Post GetRowById(int id);
        bool IsMainPost(int id);
        Post GetLastPost();
    }
}