using Models.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Models.Repositories
{
    public interface IFollowRepository
    {
        bool Add(Follow entity, bool autoSave = true);
        bool Delete(Follow entity, bool autoSave = true);
        bool Delete(int id, bool autoSave = true);
        void Dispose();
        Follow Find(int id);
        int GetLastIdentity();
        int Save();
        IQueryable<Follow> Select();
        IQueryable<TResult> Select<TResult>(Expression<Func<Follow, TResult>> selector);
        bool Update(Follow entity, bool autoSave = true);
        IQueryable<Follow> Where(Expression<Func<Follow, bool>> predicate);
    }
}