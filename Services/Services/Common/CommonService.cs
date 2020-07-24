using Models.Entities;
using Models.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace Services
{
    public class CommonService : ICommonService
    {
        private readonly ISettingRepository _settingRepository;
        private readonly IFactorRepository _factorRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IFollowRepository _followRepository;
        private readonly ICommentRepository _commentRepository;
        public CommonService(IFactorRepository factorRepository, IPostRepository postRepository, IUserRepository userRepository, ISettingRepository settingRepository, IMessageRepository messageRepository, IFollowRepository followRepository, ICommentRepository commentRepository)
        {
            _factorRepository = factorRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
            _settingRepository = settingRepository;
            _messageRepository = messageRepository;
            _followRepository = followRepository;
            _commentRepository = commentRepository;
        }
        public IEnumerable<NotificationViewModel> Notification()
        {
            var me = _userRepository.Where(p => p.Username == HttpContext.Current.User.Identity.Name).Single();
            var Message = _messageRepository.Where(p => p.reciverId == me.Id).OrderByDescending(p => p.InsertDateTime).Take(30).Select(p =>
                     new NotificationViewModel
                     {
                         User = p.sender,
                         DateTime = p.InsertDateTime,
                         Text = " Msg : " + p.Text.Substring(0, 20)
                     });
            var Follow = _followRepository.Where(p => p.followingId == me.Id).OrderByDescending(p => p.InsertDateTime).Take(30).Select(p =>
                 new NotificationViewModel
                 {
                     User = p.Follower,
                     DateTime = p.InsertDateTime,
                     Text = p.Follower.Name + " شما را فالو کرد "
                 });
            var Comment = _commentRepository.Where(p => p.Post.userId == me.Id).OrderByDescending(p => p.InsertDateTime).Take(30).Select(p =>
                 new NotificationViewModel
                 {
                     User = p.User,
                     DateTime = p.InsertDateTime,
                     Text = p.User.Name + " برای پست شما کامنت گذاشت "
                 });
            var buy = _factorRepository.Where(p => p.Post.userId == me.Id).OrderByDescending(p => p.InsertDateTime).Take(30).Select(p =>
                 new NotificationViewModel
                 {
                     User = p.User,
                     DateTime = p.InsertDateTime,
                     Text = p.User.Name + " مطلب شما را خریداری کرد "
                 });
            var model = Follow/*.Concat(Message).Concat(Comment).Concat(buy)*/;
            model = model.OrderByDescending(p => p.DateTime).Take(30);
            return model;
        }
        public Setting Footer()
        {
            var model = _settingRepository.Where(p => p.Name == "Footer").Single();
            return model;
        }

    }
}

