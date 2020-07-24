using Models.Entities;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace Services
{
    public interface IMessageService
    {
        IEnumerable<Message> Inbox();
        IEnumerable<MessageViewModel> GetAllMessages();
        string SendMessage(string text, int to);
        IEnumerable<Message> Sent();
    }
}