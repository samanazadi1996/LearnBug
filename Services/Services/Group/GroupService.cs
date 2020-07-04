using Models.Entities;
using Models.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services
{
    public class GroupServices : IGroupService
    {
        GroupRepository GroupRepository = null;

        public bool Create(Group Model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int Id)
        {

            Group group = GroupRepository.Find(Id);
            var result = GroupRepository.Delete(group);
            return result;

        }
        public bool Edit(Group Model)
        {
            throw new NotImplementedException();
        }
    }
}
