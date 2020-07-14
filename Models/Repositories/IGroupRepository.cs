using Models.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Models.Repositories
{
    public interface IGroupRepository
    {
        bool Add(Group entity, bool autoSave = true);
        bool Delete(Group entity, bool autoSave = true);
        bool Delete(int id, bool autoSave = true);
        void Dispose();
        Group Find(int id);
        int GetLastIdentity();
        int Save();
        IQueryable<Group> Select();
        IQueryable<TResult> Select<TResult>(Expression<Func<Group, TResult>> selector);
        bool Update(Group entity, bool autoSave = true);
        IQueryable<Group> Where(Expression<Func<Group, bool>> predicate);
    }
}