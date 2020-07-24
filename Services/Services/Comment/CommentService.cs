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
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public CommentService(ICommentRepository commentRepository, IPostRepository postRepository, IUserRepository userRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }
        public IEnumerable<Comment> GetAllCommentsByPost(int Id)
        {
            var model = _commentRepository.Where(p => p.postId == Id).ToList();
            return model;
        }
        public string Delete(int id)
        {
            var cmnt = _commentRepository.Find(id);
            if (IsMainComment(cmnt))
            {
                if (_commentRepository.Delete(cmnt))
                    return "toastr.success(''!کامنت شما حذف شد')";
                return "toastr.error('!کامنت شما حذف نشد')";
            }
            return "toastr.error('!کامنت شما حذف نشد')";
        }

        public IEnumerable<Comment> Create(int id, string text)
        {
            try
            {
                var post = _postRepository.Find(id);
                var me = _userRepository.Where(p => p.Username.ToLower() == HttpContext.Current.User.Identity.Name.ToLower()).Single();

                Comment comment = new Comment()
                {
                    Text = text,
                    postId = id,
                    userId = me.Id
                };
                if (_commentRepository.Add(comment))
                {
                    var model = post.Comments.AsQueryable();
                    return model;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool IsMainComment(Comment comment)
        {
            var user = HttpContext.Current.User;
            var result = comment.User.Username == user.Identity.Name || comment.Post.User.Username == user.Identity.Name || user.IsInRole("Admin");
            return result;
        }
    }
}

