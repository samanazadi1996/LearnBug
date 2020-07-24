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
    public class FactorService : IFactorService
    {
        private readonly IFactorRepository _factorRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        public FactorService(IFactorRepository factorRepository, IPostRepository postRepository, IUserRepository userRepository)
        {
            _factorRepository = factorRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public PostsViewModel GetMyBoughtPosts(int Page)
        {
            try
            {
                var factors = _factorRepository.Where(p => p.User.Username == HttpContext.Current.User.Identity.Name).OrderByDescending(p => p.InsertDateTime).Select(p => p.postId);
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
        public IEnumerable<Factor> GetMySoldPosts()
        {
            try
            {
                var Result = _factorRepository.Where(p => p.Post.User.Username == HttpContext.Current.User.Identity.Name).OrderByDescending(p => p.InsertDateTime);
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
        public IEnumerable<Factor> GetAllFactors(string from=null,string to=null)
        {
            try
            {          
                var model = _factorRepository.Select();

                if (!string.IsNullOrEmpty(from))
                {
                    DateTime datefrom = from.ToMiladiDate();
                    model = model.Where(p => p.InsertDateTime >= datefrom);
                }

                if (!string.IsNullOrEmpty(to))
                {
                    DateTime dateto = to.ToMiladiDate();
                    model = model.Where(p => p.InsertDateTime <= dateto);
                }

                return model.OrderByDescending(p=>p.InsertDateTime);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public JsonResultViewmodel Create(int Id)
        {
            var me = _userRepository.Where(p => p.Username == HttpContext.Current.User.Identity.Name).Single();

            var content = _postRepository.Find(Id);
            if (!content.Factors.Any(p => p.User.Username == me.Username))
            {
                if (me.Wallet >= content.Price)
                {
                    Factor factor = new Factor()
                    {
                        postId = Id,
                        Price = content.Price.Value,
                        Commission = content.User.Commission
                    };
                    _factorRepository.Add(factor);
                    return new JsonResultViewmodel { Success = true, Html = content.Content, Script = "toastr.success('خرید با موفقیت انجام شد')" };
                }
                else
                {
                    return new JsonResultViewmodel { Success = false, Html = "", Script = "toastr.info('کیف پول خود را شارژ کنید')" };
                }
            }
            else
            {
                return new JsonResultViewmodel { Success = false, Html = "", Script = "toastr.error('خطا');" };
            }
        }


    }
}

