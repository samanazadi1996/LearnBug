using Models.Entities;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace Services
{
    public interface IBookmarkService
    {
        string CreateOrDeleteBookmark(int postId);
        Task<Bookmark> GetBookmarkById(int id);
        PostsViewModel GetAllBookmarks(int Page = 1, string search = null);
    }
}