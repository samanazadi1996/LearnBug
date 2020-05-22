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

        public bool Add(Content entity, bool autoSave = true)
        {
            try
            {
                db.Contents.Add(entity);
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

        public bool Update(Content entity, bool autoSave = true)
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

        public bool Delete(Content entity, bool autoSave = true)
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
                var entity = db.Contents.Find(id);
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

        public Content Find(int id)
        {
            try
            {
                return db.Contents.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<Content> Where(System.Linq.Expressions.Expression<Func<Content, bool>> predicate)
        {
            try
            {
                return db.Contents.Where(predicate);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<Content> Select()
        {
            try
            {
                return db.Contents.AsQueryable();
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<Content, TResult>> selector)
        {
            try
            {
                return db.Contents.Select(selector);
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
                if (db.Contents.Any())
                    return db.Contents.OrderByDescending(p => p.Id).First().Id;
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