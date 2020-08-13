using Models.Entities;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace Services
{
    public interface IUserService
    {
        bool AddTransactionByUser(double price);
        IEnumerable<User> AllUsers(string name = null, string username = null, string role = "نقش", string email = null);
        bool AutenticatorUseName(string Username);
        AvatarViewModel Avatar();
        bool ChangePassword(ChangePasswordViewModel model);
        bool ChangeProfilePicture(string newPicture, string type);
        User ChangeUserByAdmin(User user);
        bool EditProfile(User user);
        IEnumerable<User> GetAllUser();
        IEnumerable<User> GetBlockedUser();
        User GetCurrentUser();
        string GetImgProfileByUsername(string username);
        User GetRowSelectelById(int id);
        double GetWallet(string username);
        ProfileViewModel Profile(string username);
        User UpdateBlockUser(int id);
        bool UserExist(LoginUserViewModel user);
    }
}