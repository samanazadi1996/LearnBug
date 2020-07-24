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

        public bool Login(string Username, string Password, string Rememberme)
        {
            Password = Password.Encrypt();
            if (_userRepository.Select(p => p.Username == Username.ToLower() && p.Password == Password && p.IsActive).Any())
            {
                FormsAuthentication.SetAuthCookie(Username.ToLower(), Convert.ToBoolean(Rememberme));
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Register(User user)
        {
            if (_userRepository.Select(p => p.Username == user.Username.ToLower().Trim()).Any())
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

