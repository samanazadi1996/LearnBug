using Models.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Models.Repositories
{
    public interface IPostRepository
    {
        bool Add(Post entity, bool autoSave = true);
        bool Delete(int id, bool autoSave = true);
        bool Delete(Post entity, bool autoSave = true);
        void Dispose();
        Post Find(int id);
        int GetLastIdentity();
        int Save();
        IQueryable<Post> Select();
        IQueryable<TResult> Select<TResult>(Expression<Func<Post, TResult>> selector);
        bool Update(Post entity, bool autoSave = true);
        IQueryable<Post> Where(Expression<Func<Post, bool>> predicate);
    }
}