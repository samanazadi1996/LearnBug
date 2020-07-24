using Models.Entities;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace Services
{
    public interface ICommonService
    {
        Setting Footer();
        IEnumerable<NotificationViewModel> Notification();
    }
}