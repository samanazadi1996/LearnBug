using Models.Entities;
using ViewModels;

namespace Services
{
    public interface IAccountService
    {
        bool Login(LoginUserViewModel loginUser);
        void Logout();
        bool Register(RegisterVeiwModel user);
    }
}