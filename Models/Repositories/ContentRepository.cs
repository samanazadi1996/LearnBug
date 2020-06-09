using Models.Entities;
using System;
using System.Linq;
using System.Web;

namespace Models.Repositories
{
    public class ContentRepository : IDisposable
    {
        DatabaseContext db = null;

        public ContentRepository()
        {
            db = new DatabaseContext();
        }

        public bool Add(Post entity, bool autoSave = true)
        {
            try
            {
                db.Posts.Add(entity);
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

        public bool Update(Post entity, bool autoSave = true)
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

        public bool Delete(Post entity, bool autoSave = true)
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
                var entity = db.Posts.Find(id);
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

        public Post Find(int id)
        {
            try
            {
                return db.Posts.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<Post> Where(System.Linq.Expressions.Expression<Func<Post, bool>> predicate)
        {
            try
            {
                return db.Posts.Where(predicate);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<Post> Select()
        {
            try
            {
                return db.Posts.AsQueryable();
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<Post, TResult>> selector)
        {
            try
            {
                return db.Posts.Select(selector);
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
                if (db.Posts.Any())
                    return db.Posts.OrderByDescending(p => p.Id).First().Id;
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

        ~ContentRepository()
        {
            Dispose(false);
        }
    }
}