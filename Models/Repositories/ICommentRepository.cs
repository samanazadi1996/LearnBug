using Models.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Models.Repositories
{
    public interface ICommentRepository
    {
        bool Add(Comment entity, bool autoSave = true);
        bool Delete(Comment entity, bool autoSave = true);
        bool Delete(int id, bool autoSave = true);
        void Dispose();
        Comment Find(int id);
        int GetLastIdentity();
        int Save();
        IQueryable<Comment> Select();
        IQueryable<TResult> Select<TResult>(Expression<Func<Comment, TResult>> selector);
        bool Update(Comment entity, bool autoSave = true);
        IQueryable<Comment> Where(Expression<Func<Comment, bool>> predicate);
    }
}