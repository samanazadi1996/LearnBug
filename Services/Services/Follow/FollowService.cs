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
    public class FollowService : IFollowService
    {
        private readonly IFollowRepository _followRepository;
        private readonly IUserRepository _userRepository;
        public FollowService(IFollowRepository followRepository, IUserRepository userRepository)
        {
            _followRepository = followRepository;
            _userRepository = userRepository;
        }
        public string AddOrDeleteFollow(int id)
        {
            var myUser = _userRepository.Where(p => p.Username == HttpContext.Current.User.Identity.Name).Single();
            if (myUser.Follower.Any(p => p.followingId == id))
            {
                var follow = myUser.Follower.FirstOrDefault(p => p.followingId == id);
                _followRepository.Delete(follow);
                return "follow";
            }
            _followRepository.Add(new Follow { followingId = id, followerId = myUser.Id });
            return "Unfollow";
        }
        public IEnumerable<User> Followers(string id)
        {
            var myUser = _userRepository.Where(p => p.Username == id).Single();
            var model = myUser.Following.Select(p => p.Follower);
            return model;
        }
        public IEnumerable<User> Following(string id)
        {
            var myUser = _userRepository.Where(p => p.Username == id).Single();
            var model = myUser.Follower.Select(p => p.Following);
            return model;
        }
    }
}

