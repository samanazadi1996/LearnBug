using Data;
using Entities;
using System;
using System.Linq;
using System.Web;

namespace LearnBug.Models.Repositories
{
    public class CommentRepository : IDisposable
    {
        DatabaseContext db = null;

        public CommentRepository()
        {
            db = new DatabaseContext();
        }

        public bool Add(Comment entity, bool autoSave = true)
        {
            try
            {
                db.comments.Add(entity);
                if (autoSave)
                    return Convert.ToBoolean(db.SaveChanges());
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Comment entity, bool autoSave = true)
        {
            try
            {

                db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                if (autoSave)
                    return Convert.ToBoolean(db.SaveChanges());
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Comment entity, bool autoSave = true)
        {
            try
            {
                db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                if (autoSave)
                    return Convert.ToBoolean(db.SaveChanges());
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id, bool autoSave = true)
        {
            try
            {
                var entity = db.comments.Find(id);
                db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                if (autoSave)
                {
                    return Convert.ToBoolean(db.SaveChanges());
                    ;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public Comment Find(int id)
        {
            try
            {
                return db.comments.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<Comment> Where(System.Linq.Expressions.Expression<Func<Comment, bool>> predicate)
        {
            try
            {
                return db.comments.Where(predicate);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<Comment> Select()
        {
            try
            {
                return db.comments.AsQueryable();
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<Comment, TResult>> selector)
        {
            try
            {
                return db.comments.Select(selector);
            }
            catch
            {
                return null;
            }
        }

        public int GetLastIdentity()
        {
            try
            {
                if (db.comments.Any())
                    return db.comments.OrderByDescending(p => p.Id).First().Id;
                else
                    return 0;
            }
            catch
            {
                return -1;
            }
        }

        public int Save()
        {
            try
            {
                return db.SaveChanges();
            }
            catch
            {
                return -1;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.db != null)
                {
                    this.db.Dispose();
                    this.db = null;
                }
            }
        }

        ~CommentRepository()
        {
            Dispose(false);
        }
    }
}