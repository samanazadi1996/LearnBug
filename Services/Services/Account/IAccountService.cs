using Models.Entities;

namespace Services
{
    public interface IAccountService
    {
        bool Login(string Username, string Password, string Rememberme);
        void Logout();
        bool Register(User user);
    }
}