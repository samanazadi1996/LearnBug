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
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
        public IQueryable<Group> ManagementGroups()
        {
            try
            {
                return _groupRepository.Select();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool AddGroup(string Name, string Image)
        {
            try
            {
                if (!_groupRepository.Where(p => p.Name.ToLower() == Name.ToLower()).Any())
                {
                    var filename = "/Files/GroupPicture/Group_" + (_groupRepository.Select().OrderByDescending(p => p.Id).FirstOrDefault().Id + 1).ToString() + ".jpg";
                    Utility.ConvertBase64toFile.Convert_base64_url_Image(Image, filename);
                    Group group = new Group
                    {
                        Name = Name,
                        Image = filename
                    };
                    return _groupRepository.Add(group);
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteGroup(int Id)
        {
            try
            {
                Group group = _groupRepository.Find(Id);
                return _groupRepository.Delete(group);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool EditGroup(int Id, string Name, string Image)
        {
            try
            {
                var group = _groupRepository.Find(Id);
                var filename = "/Files/GroupPicture/Group_" + group.Id + ".jpg";
                Utility.ConvertBase64toFile.Convert_base64_url_Image(Image, filename);
                group.Name = Name;
                group.Image = filename;
                return _groupRepository.Update(group);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Group GetRowSelectelById(int id)
        {
            try
            {
                var Result = _groupRepository.Find(id);
                return Result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IQueryable<Group> GetAllGroups()
        {
            try
            {
                var model = _groupRepository.Select();
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

