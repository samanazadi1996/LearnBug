using Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace Services
{
    public interface IBookmarkService
    {
        string CreateOrDeleteBookmark(int postId);
        Task<Bookmark> GetBookmarkById(int id);
        IEnumerable<int> GetAllBookmarks(string search = null);
    }
}