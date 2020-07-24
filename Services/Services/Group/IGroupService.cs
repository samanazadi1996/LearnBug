using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Services
{
    public interface IGroupService
    {
        bool AddGroup(string Name, string Image);
        bool DeleteGroup(int Id);
        bool EditGroup(int Id, string Name, string Image);
        IEnumerable<Group> GetAllGroups();
        Group GetRowSelectelById(int id);
        IEnumerable<Group> ManagementGroups();

    }
}
