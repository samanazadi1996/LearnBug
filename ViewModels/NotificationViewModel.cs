using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class NotificationViewModel
    {
        public User User { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
    }
}