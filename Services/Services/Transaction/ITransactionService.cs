using Models.Entities;
using System.Linq;

namespace Services
{
    public interface ITransactionService
    {
        IQueryable<Transaction> GetAllTransaction();
    }
}