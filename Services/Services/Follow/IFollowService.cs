using Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public interface IFollowService
    {
        string AddOrDeleteFollow(int id);
        IEnumerable<User> Followers(string id);
        IEnumerable<User> Following(string id);
    }
}