using Models.Entities;
using System.Linq;
using ViewModels;

namespace Services
{
    public interface IUserService
    {
        bool AddTransactionByUser(double price);
        IQueryable<User> AllUsers(string name = null, string username = null, string role = "نقش", string email = null);
        bool AutenticatorUseName(string Username);
        bool ChangePassword(ChangePasswordViewModel model);
        bool ChangeProfilePicture(string newPicture, string type);
        IQueryable<User> GetAllUser();
        IQueryable<User> GetBlockedUser();
        User GetCurrentUser();
        User UpdateBlockUser(int id);
        User GetRowSelectelById(int id);
        User ChangeUserByAdmin(User user);
        double GetWallet();
        ProfileViewModel Profile(string username);
        bool EditProfile(User user);
    }
}