using Models.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Models.Repositories
{
    public interface ITransactionRepository
    {
        bool Add(Transaction entity, bool autoSave = true);
        bool Delete(int id, bool autoSave = true);
        bool Delete(Transaction entity, bool autoSave = true);
        void Dispose();
        Transaction Find(int id);
        int GetLastIdentity();
        int Save();
        IQueryable<Transaction> Select();
        IQueryable<TResult> Select<TResult>(Expression<Func<Transaction, TResult>> selector);
        bool Update(Transaction entity, bool autoSave = true);
        IQueryable<Transaction> Where(Expression<Func<Transaction, bool>> predicate);
    }
}