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
using Models.Entities;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;
        private readonly ISettingRepository _settingRepository;
        private readonly ITransactionRepository _transactionRepository;
        public UserService(IUserRepository UserRepository, ITransactionRepository transactionRepository, ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
            _UserRepository = UserRepository;
            _transactionRepository = transactionRepository;
        }
        public ProfileViewModel Profile(string username)
        {
            try
            {
                var user = _UserRepository.Where(p => p.Username.Trim() == username.Trim()).FirstOrDefault();
                var mutualFollower = user.Following.Select(p => p.Follower)
                    .Where(p => p.Following.Any(o => o.Follower.Username == HttpContext.Current.User.Identity.Name));

                ProfileViewModel model = new ProfileViewModel()
                {
                    mutualFollower = mutualFollower,
                    User = user,
                    Posts = user.Posts
                };

                return model;

            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public IEnumerable<User> AllUsers(string name = null, string username = null, string role = "نقش", string email = null)
        {
            var model = _UserRepository.Select().AsQueryable();

            if (!string.IsNullOrEmpty(name))
                model = model.Where(p => p.Name.Contains(name));

            if (!string.IsNullOrEmpty(username))
                model = model.Where(p => p.Username.Contains(username));

            if (!string.IsNullOrEmpty(email))
                model = model.Where(p => p.Email.Contains(email));

            if (role != "نقش")
                model = model.Where(p => p.Roles == role);
            return model.ToList();
        }
        public bool ChangePassword(ChangePasswordViewModel model)
        {
            model.OldPassword = model.OldPassword.Encrypt();
            var myuser = GetCurrentUser();
            if (myuser == null)
                return false;

            myuser.Password = model.NewPassword.Encrypt();
            return _UserRepository.Update(myuser);
        }
        public bool EditProfile(User user)
        {
            var myuser = GetCurrentUser();
            //myuser.Username = user.Username.ToLower();
            myuser.Name = user.Name;
            myuser.Email = user.Email;
            myuser.Dateofbirth = user.PersianDateofbirth.ToMiladiDate();
            myuser.Gender = user.Gender;
            myuser.Biography = user.Biography;
            myuser.Phone = user.Phone;
            myuser.Location = user.Location;
            return _UserRepository.Update(myuser);

        }
        public bool ChangeProfilePicture(string newPicture, string type)
        {
            var user = GetCurrentUser();
            if (type == "update")
            {
                var filename = "/Files/ProfilePicture/" + user.Username + ".jpg";
                Utility.ConvertBase64toFile.Convert_base64_url_Image(newPicture, filename);
                user.Image = filename;
                _UserRepository.Update(user);
                return true;
            }
            else
            {
                user.Image = null;
                _UserRepository.Update(user);
                return false;
            }
        }
        public bool AutenticatorUseName(string Username)
        {
            Username = Username.ToLower();

            if (Username.Length < 5 || Username.Length > 30)
                return false;

            if (_UserRepository.Where(p => p.Username == Username).Any())
                return false;

            var list = Username.ToCharArray();
            var firstChar = (int)list[0];

            if (firstChar <= 57 && firstChar >= 48)
                return false;

            foreach (var item in list)
            {
                var asc = (int)item;

                if ((asc < 97 || asc > 122) && (asc < 48 || asc > 57) && item != '_' && item != '.' && item != '-' && item != '@')
                    return false;
            }

            return true;
        }
        public User GetCurrentUser()
        {
            try
            {
                var Result = _UserRepository.Where(p => p.Username.Trim() == HttpContext.Current.User.Identity.Name.Trim()).FirstOrDefault();
                return Result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool AddTransactionByUser(double price)
        {
            var user = _UserRepository.Where(p => p.Username == HttpContext.Current.User.Identity.Name).Single();
            Transaction transaction = new Transaction
            {
                Price = price,
                Charge = true,
            };
            var result1 = _transactionRepository.Add(transaction);
            user.Wallet += price;
            var result2 = _UserRepository.Update(user);
            return result1 && result2;

        }
        public User GetRowSelectelById(int id)
        {
            try
            {
                var Result = _UserRepository.Find(id);
                return Result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IEnumerable<User> GetAllUser()
        {
            try
            {
                var model = _UserRepository.Select().ToList();
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public double GetWallet(string username)
        {
            try
            {
                var user = _UserRepository.Where(p => p.Username == username).Single();
                var transaction = user.Transactions.Sum(p => p.Price);
                var factor = user.Factors.Sum(p => p.Price);
                return transaction - factor;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public IEnumerable<User> GetBlockedUser()
        {
            try
            {
                var model = _UserRepository.Where(p => !p.IsActive).ToList();
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public User UpdateBlockUser(int id)
        {
            try
            {
                var user = _UserRepository.Find(id);
                user.IsActive = !user.IsActive;
                _UserRepository.Update(user);
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public AvatarViewModel Avatar()
        {
            try
            {
                var me = GetCurrentUser();
                var image = GetImgProfileByUsername(me.Username);
                var wallet = GetWallet(me.Username);
                var model = new AvatarViewModel()
                {
                    Id = me.Id,
                    Username = me.Username,
                    Name = me.Name,
                    CountOfFollowing = me.Following.Count(),
                    CountOfFollower = me.Follower.Count(),
                    HasBookmark = me.Bookmarks.Any(),
                    HasPost = me.Posts.Any(),
                    HasFactor = me.Factors.Any(),
                    Wallet = wallet,
                    IsActive = me.IsActive,
                    CountOfPosts = me.Posts.Count(),
                    Image = image
                };
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public User ChangeUserByAdmin(User user)
        {
            try
            {
                user.Dateofbirth = user.PersianDateofbirth.ToMiladiDate();
                user.Username = user.Username.ToLower().Trim();
                user.Password = user.Password.Encrypt();
                _UserRepository.Update(user);
                return GetRowSelectelById(user.Id);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string GetImgProfileByUsername(string username)
        {
            var user = _UserRepository.Where(p => p.Username == username).Single();
            if (user is null)
            {
                if (user.Gender == GenderType.Mele)
                {
                    return _settingRepository.Where(p => p.Name == "DefultProfilePictureMale").FirstOrDefault().Value;

                }
                else if (user.Gender == GenderType.Femele)
                {
                    return _settingRepository.Where(p => p.Name == "DefultProfilePictureFemale").FirstOrDefault().Value;
                }
                else
                {
                    return _settingRepository.Where(p => p.Name == "DefultProfilePicture").FirstOrDefault().Value;
                }

            }
            else
            {
                return user.Image;
            }
        }
    }
}
