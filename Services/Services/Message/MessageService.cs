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
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;

        public MessageService(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }
        public IEnumerable<MessageViewModel> GetAllMessages()
        {
            var myUserName = HttpContext.Current.User.Identity.Name;
            var message = _messageRepository.Where(p => p.sender.Username == myUserName || p.Reciver.Username == myUserName);
            var model = message.Select(p => new MessageViewModel
            {
                User = (p.Reciver.Username == myUserName ? p.sender : p.Reciver)
            });
            return model.Distinct();
        }
        public IEnumerable<Message> Inbox()
        {
            var myUserName = HttpContext.Current.User.Identity.Name;
            var model = _messageRepository.Where(p => p.Reciver.Username == myUserName).OrderByDescending(p => p.InsertDateTime).AsQueryable();
            return model;
        }
        public IEnumerable<Message> Sent()
        {
            var myUserName = HttpContext.Current.User.Identity.Name;
            var model = _messageRepository.Where(p => p.sender.Username == myUserName).OrderByDescending(p => p.InsertDateTime).AsQueryable();
            return model;
        }
        public string SendMessage(string text, int to)
        {
            var me = _userRepository.Where(p => p.Username == HttpContext.Current.User.Identity.Name).Single();
            var message = new Message
            {
                senderId = me.Id,
                Text = text,
                reciverId = to,
                Status = 0
            };
            if (_messageRepository.Add(message))
                return "toastr.success('پیغام ارسال شد')";
            return "toastr.error('پیغام ارسال نشد')";
        }

    }
}

