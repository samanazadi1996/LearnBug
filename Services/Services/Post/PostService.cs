using Models.Entities;
using Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ViewModels;

namespace Services
{
    public class PostService : IPostService
    {
        private readonly ISettingRepository _settingRepository;
        private readonly IFactorRepository _factorRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IFollowRepository _followRepository;
        private readonly ICommentRepository _commentRepository;
        public PostService(IFactorRepository factorRepository, IPostRepository postRepository, IUserRepository userRepository, ISettingRepository settingRepository, IMessageRepository messageRepository, IFollowRepository followRepository, ICommentRepository commentRepository)
        {
            _factorRepository = factorRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
            _settingRepository = settingRepository;
            _messageRepository = messageRepository;
            _followRepository = followRepository;
            _commentRepository = commentRepository;
        }
        public IEnumerable<int> GetMyPosts()
        {
            var posts = _postRepository.Where(p => p.User.Username == HttpContext.Current.User.Identity.Name).OrderByDescending(o => o.InsertDateTime);

            var model = posts.Select(p => p.Id).ToList();
            return model;
        }
        public bool Create(Post post)
        {
            string path = HttpContext.Current.Server.MapPath("~/Files/Posts/");

            var result = Utility.ConvertBase64toFile.Convert_Htmlbase64_url_Image(post.Content, path, "/Files/Posts/");

            var user = _userRepository.Where(p => p.Username == HttpContext.Current.User.Identity.Name).Single();
            post.Status = 0;
            post.userId = user.Id;
            if (HttpContext.Current.User.IsInRole("User"))
                post.Price = null;
            post.Content = result.ToString();
            return _postRepository.Add(post);
        }
        public bool Edit(Post post)
        {
            string path = HttpContext.Current.Server.MapPath("~/Files/Posts/");

            var result = Utility.ConvertBase64toFile.Convert_Htmlbase64_url_Image(post.Content, path, "/Files/Posts/");

            var cntnt = _postRepository.Find(post.Id);
            if (cntnt.User.Username == HttpContext.Current.User.Identity.Name || HttpContext.Current.User.IsInRole("Admin"))
            {
                if (!HttpContext.Current.User.IsInRole("User"))
                    cntnt.Price = post.Price;

                cntnt.groupId = post.groupId;
                cntnt.Subject = post.Subject;
                cntnt.Content = result;
                cntnt.KeyWords = post.KeyWords;


                return _postRepository.Update(cntnt);
            }
            return false;

        }
        public bool IsMainPost(int id)
        {
            var model = _postRepository.Find(id);
            if (HttpContext.Current.User.IsInRole("Admin") || model.User.Username == HttpContext.Current.User.Identity.Name)
                return true;
            return false;
        }
        public bool Delete(Post post)
        {
            var model = _postRepository.Find(post.Id);

            if (HttpContext.Current.User.IsInRole("Admin") || model.User.Username == HttpContext.Current.User.Identity.Name && !model.Factors.Any())
            {
                foreach (var item in model.Comments) { _commentRepository.Delete(item); }
                //foreach (var item in model.Bookmarks) { model.Bookmarks.Remove(item); }

                return _postRepository.Delete(model);
            }
            return false;

        }
        public Post GetRowById(int id)
        {
            try
            {
                var Result = _postRepository.Find(id);
                return Result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public PostViewModel GetSinglePostById(int id)
        {
            try
            {
                var post = _postRepository.Find(id);
                var model = new PostViewModel() { 
                    Id=post.Id,
                    Subject=post.Subject,
                    UserName=post.User.Name,
                    GroupName=post.Group.Name,
                    CommentsCount=post.Comments.Count(),
                    InsertDateTime=post.InsertDateTime,
                    Price=post.Price,
                    KeyWords=post.KeyWords,
                    IsMainPost=post.User.Username ==HttpContext.Current.User.Identity.Name,
                    IBuyedPost=post.Factors.Any(p => p.User.Username ==HttpContext.Current.User.Identity.Name),
                };
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IEnumerable<Post> GetAllPosts()
        {
            try
            {
                var model = _postRepository.Select().ToList();
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Post GetLastPost()
        {
            try
            {
                var post = _postRepository.Where(p => p.User.Username == HttpContext.Current.User.Identity.Name).OrderByDescending(o => o.InsertDateTime).FirstOrDefault();
                return post;
            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}

