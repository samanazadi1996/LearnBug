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
    public class HomeService : IHomeService
    {
        private readonly ISettingRepository _settingRepository;
        private readonly IPostRepository _postRepository;
        private readonly IGroupRepository _groupRepository;

        public HomeService(ISettingRepository settingRepository, IPostRepository postRepository, IGroupRepository groupRepository)
        {
            _settingRepository = settingRepository;
            _postRepository = postRepository;
            _groupRepository = groupRepository;
        }
        public Setting About()
        {
            var model = _settingRepository.Where(p => p.Name == "About").Single();
            return model;
        }
        public PostsViewModel Index(string search, int Page)
        {
            var contents = _postRepository.Select().AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                contents = contents.Where(p => p.Subject.Contains(search) || p.Group.Name.Contains(search) || p.User.Name.Contains(search) || p.User.Username.Contains(search) || p.Price.ToString().Contains(search) || p.KeyWords.Contains(search));
            }
            PostsViewModel model = new PostsViewModel
            {
                PostId = contents.OrderByDescending(p => p.InsertDateTime).Skip((Page - 1) * 12).Take(12).Select(p => p.Id).ToList(),
                CurrentPage = Page,
                TotalItemCount = contents.Count(),
                Groups = _groupRepository.Select().ToList()
            };
            return model;
        }

    }
}

