using Models.Entities;
using ViewModels;

namespace Services
{
    public interface IHomeService
    {
        Setting About();
        PostsViewModel Index(string search, int Page);
    }
}