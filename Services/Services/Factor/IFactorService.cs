using Models.Entities;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace Services
{
    public interface IFactorService
    {
        JsonResultViewmodel Create(int Id);
        IEnumerable<Factor> GetAllFactors(string from = null, string to = null);
        IEnumerable<Factor> GetMySoldPosts();
        Factor GetRowSelectelById(int id);
        List<int> GetMyBoughtPosts();
    }
}