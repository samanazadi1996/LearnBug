using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class MessageViewModel
    {
        public User User { get; set; }
        public string LastMessage { get; set; }
        public DateTime LastMessageDatetime { get; set; }
    }
}