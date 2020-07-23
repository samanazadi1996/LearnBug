using Models.Entities;
using Models.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _TransactionRepository;
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _TransactionRepository = transactionRepository;
        }
        public IQueryable<Transaction> GetAllTransaction()
        {
            var transactions = _TransactionRepository.Where(p => p.User.Username == HttpContext.Current.User.Identity.Name).OrderByDescending(p => p.InsertDateTime).AsQueryable();
            return transactions;
        }
    }
}


