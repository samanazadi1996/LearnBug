using Models.Entities;
using Models.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ViewModels;

namespace Services
{
    public class FactorService
    {
        private readonly IFactorRepository _factorRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        public FactorService(IFactorRepository factorRepository,IPostRepository postRepository,IUserRepository userRepository)
        {
            _factorRepository = factorRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public PostsViewModel Index(int Page)
        {
            try
            {
                var factors = _factorRepository.Where(p => p.User.Username == HttpContext.Current.User.Identity.Name).OrderByDescending(p => p.InsertDateTime).Select(p => p.postId).AsQueryable();
                PostsViewModel model = new PostsViewModel
                {
                    PostId = factors,
                    CurrentPage = Page,
                    TotalItemCount = factors.Count()
                };
                return model;
            }
            catch (Exception)
            {
                return null; 
            }  
        }
        public IQueryable<Factor> GetMySoldPosts()
        {
            try
            {
                var Result = _factorRepository.Where(p => p.Post.User.Username == HttpContext.Current.User.Identity.Name).OrderByDescending(p => p.InsertDateTime).AsQueryable();
                return Result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Factor GetRowSelectelById(int id)
        {
            try
            {
                var Result = _factorRepository.Find(id);
                return Result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IQueryable<Factor> GetAllFactors()
        {
            try
            {
                var model = _factorRepository.Select();
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}

