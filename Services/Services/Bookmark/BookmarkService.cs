using Models.Entities;
using Models.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace Services
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IBookmarkRepository _bookmarkRepository;
        private readonly IUserRepository _userRepository;
        public BookmarkService(IBookmarkRepository bookmarkRepository, IUserRepository userRepository)
        {
            _bookmarkRepository = bookmarkRepository;
            _userRepository = userRepository;
        }
        public string CreateOrDeleteBookmark(int postId)
        {
            var myUser = _userRepository.Where(p => p.Username == HttpContext.Current.User.Identity.Name).Single();
            if (!myUser.Bookmarks.Any(p => p.postId == postId))
            {
                _bookmarkRepository.Add(new Bookmark { postId = postId, userId = myUser.Id });
                return "toastr.success('مطلب نشانه گذاری شد .')";

            }
            else
            {
                var bookmark = myUser.Bookmarks.FirstOrDefault(p => p.postId == postId);
                _bookmarkRepository.Delete(bookmark);
                return "toastr.success('مطلب از لیست نشانه گذاری ها حذف شد .')";
            }
        }
        public IEnumerable<int> GetAllBookmarks(string search = null)
        {
            var Posts = _userRepository.Where(p => p.Username == HttpContext.Current.User.Identity.Name).Single().Bookmarks.Select(o => o.Post).AsQueryable();
            Posts = Posts.Where(p => p.Subject.Contains(search) || p.Group.Name.Contains(search) || p.User.Name.Contains(search) || p.User.Username.Contains(search) || p.Price.ToString().Contains(search) || p.KeyWords.Contains(search));

            var model = Posts.OrderByDescending(i => i.InsertDateTime).Select(p => p.Id);
            return model;
        }
        public async Task<Bookmark> GetBookmarkById(int id)
        {
            try
            {
                var Result = _bookmarkRepository.Find(id);
                return Result;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}

