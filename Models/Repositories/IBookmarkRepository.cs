using Models.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Models.Repositories
{
    public interface IBookmarkRepository
    {
        bool Add(Bookmark entity, bool autoSave = true);
        bool Delete(Bookmark entity, bool autoSave = true);
        bool Delete(int id, bool autoSave = true);
        void Dispose();
        Bookmark Find(int id);
        int GetLastIdentity();
        int Save();
        IQueryable<Bookmark> Select();
        IQueryable<TResult> Select<TResult>(Expression<Func<Bookmark, TResult>> selector);
        bool Update(Bookmark entity, bool autoSave = true);
        IQueryable<Bookmark> Where(Expression<Func<Bookmark, bool>> predicate);
    }
}