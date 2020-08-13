using Models.Entities;
using Models.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Globalization;
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
            if (_userRepository.Exist(loginUser.Username.ToLower(), loginUser.Password))
            {
                FormsAuthentication.SetAuthCookie(loginUser.Username.ToLower(), loginUser.Rememberme);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Register(RegisterVeiwModel register)
        {
            if (!_userRepository.Select(p => p.Username == register.Username.ToLower().Trim()).Any())
                return false;
            var user = FillUserByViewModel(register);
            if (_userRepository.Add(user))
                return true;

            return false;
        }
        private User FillUserByViewModel(RegisterVeiwModel register)
        {
            var user = new User()
            {
                Status = 1,
                Roles = "User",
                Wallet = 0,
                Username = register.Username.ToLower().Trim(),
                Password = register.Password.Encrypt(),
                Dateofbirth = register.PersianDateofbirth.ToMiladiDate(),
                Biography = register.Biography,
                Email = register.Email,
                Gender = register.Gender,
                Location = register.Location,
                Name = register.Name,
                Phone = register.Phone,
                PersianDateofbirth=register.PersianDateofbirth
            };
            if (register.leafletViewwModel.Lat != null && register.leafletViewwModel.Lng != null)
            {
                string strPointWellKnownText =
                 string.Format(CultureInfo.InvariantCulture.NumberFormat, "POINT({0} {1})", register.leafletViewwModel.Lng, register.leafletViewwModel.Lat);
                DbGeography oDbGeography =
                    DbGeography.PointFromText
                    (pointWellKnownText: strPointWellKnownText, coordinateSystemId: 4326);
                user.GeoLocation = oDbGeography;
            }
            return user;
        }

    }
}

