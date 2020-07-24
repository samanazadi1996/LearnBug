using Models.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Models.Repositories
{
    public interface IMessageRepository
    {
        bool Add(Message entity, bool autoSave = true);
        bool Delete(int id, bool autoSave = true);
        bool Delete(Message entity, bool autoSave = true);
        void Dispose();
        Message Find(int id);
        int GetLastIdentity();
        int Save();
        IQueryable<Message> Select();
        IQueryable<TResult> Select<TResult>(Expression<Func<Message, TResult>> selector);
        bool Update(Message entity, bool autoSave = true);
        IQueryable<Message> Where(Expression<Func<Message, bool>> predicate);
    }
}