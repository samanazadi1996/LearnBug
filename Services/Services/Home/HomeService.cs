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
        public IEnumerable<int> Index(string search)
        {
            var contents = _postRepository.Select().AsQueryable();

            var model = contents.Where(p => p.Subject.Contains(search) || p.Group.Name.Contains(search) || p.User.Name.Contains(search) || p.User.Username.Contains(search) || p.Price.ToString().Contains(search) || p.KeyWords.Contains(search)).OrderByDescending(p => p.InsertDateTime).Select(p => p.Id);
            return model;
        }

    }
}

