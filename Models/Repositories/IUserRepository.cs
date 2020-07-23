using Models.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Models.Repositories
{
    public interface IUserRepository
    {
        bool Add(User entity, bool autoSave = true);
        bool Delete(int id, bool autoSave = true);
        bool Delete(User entity, bool autoSave = true);
        void Dispose();
        User Find(int id);
        int GetLastIdentity();
        int Save();
        IQueryable<User> Select();
        IQueryable<TResult> Select<TResult>(Expression<Func<User, TResult>> selector);
        bool Update(User entity, bool autoSave = true);
        IQueryable<User> Where(Expression<Func<User, bool>> predicate);
    }
}