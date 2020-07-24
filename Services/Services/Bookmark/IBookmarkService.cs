using Models.Entities;
using System.Linq;
using ViewModels;

namespace Services
{
    public interface IBookmarkService
    {
        string CreateOrDeleteBookmark(int postId);
        Bookmark GetBookmarkById(int id);
        PostsViewModel GetAllBookmarks(int Page = 1, string search = null);
    }
}