using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services
{
   public interface IGroupServices
    {
        bool Create(Group Model);
        bool Edit(Group Model);
        bool Delete(int Id);
    }
}
