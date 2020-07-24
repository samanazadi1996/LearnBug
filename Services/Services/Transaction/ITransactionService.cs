using Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetAllTransaction();
    }
}