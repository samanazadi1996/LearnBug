using System.Threading.Tasks;
using ViewModels;

namespace Services
{
    public interface IJwtService
    {
        string GenerateAsync(LoginUserViewModel user);
    }
}