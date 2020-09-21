using Models.Entities;
using System.Collections.Generic;
using ViewModels;

namespace Services
{
    public interface IHomeService
    {
        Setting About();
        IEnumerable<int> Index(string search);
    }
}