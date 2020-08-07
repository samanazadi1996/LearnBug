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
using System.Web.Security;
using ViewModels;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [Authorize]
        public void Logout()
        {
            FormsAuthentication.SignOut();
        }

        public bool Login(LoginUserViewModel loginUser)
        {
            loginUser.Password = loginUser.Password.Encrypt();
            if (_userRepository.Exist(loginUser.Username.ToLower() ,loginUser.Password))
            {
                FormsAuthentication.SetAuthCookie(loginUser.Username.ToLower(), loginUser.Rememberme);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Register(User user)
        {
            if (!_userRepository.Select(p => p.Username == user.Username.ToLower().Trim()).Any())
                return false;

            user.Status = 1;
            user.Roles = "User";
            user.Wallet = 0;
            user.Username = user.Username.ToLower().Trim();
            user.Password = user.Password.Encrypt();
            user.Dateofbirth = user.PersianDateofbirth.ToMiladiDate();

            if (_userRepository.Add(user))
                return true;

            return false;
        }


    }
}

