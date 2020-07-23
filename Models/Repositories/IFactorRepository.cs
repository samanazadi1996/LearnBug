using Models.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Models.Repositories
{
    public interface IFactorRepository
    {
        bool Add(Factor entity, bool autoSave = true);
        bool Delete(Factor entity, bool autoSave = true);
        bool Delete(int id, bool autoSave = true);
        void Dispose();
        Factor Find(int id);
        int GetLastIdentity();
        int Save();
        IQueryable<Factor> Select();
        IQueryable<TResult> Select<TResult>(Expression<Func<Factor, TResult>> selector);
        bool Update(Factor entity, bool autoSave = true);
        IQueryable<Factor> Where(Expression<Func<Factor, bool>> predicate);
    }
}