using Data;
using Entities;
using System;
using System.Linq;
using System.Web;

namespace Data.Repositories
{
    public class BookmarkRepository : IDisposable
    {
        DatabaseContext db = null;

        public BookmarkRepository()
        {
            db = new DatabaseContext();
        }

        public bool Add(Bookmark entity, bool autoSave = true)
        {
            try
            {
                db.Bookmarks.Add(entity);
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

        public bool Update(Bookmark entity, bool autoSave = true)
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

        public bool Delete(Bookmark entity, bool autoSave = true)
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
                var entity = db.Bookmarks.Find(id);
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

        public Bookmark Find(int id)
        {
            try
            {
                return db.Bookmarks.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<Bookmark> Where(System.Linq.Expressions.Expression<Func<Bookmark, bool>> predicate)
        {
            try
            {
                return db.Bookmarks.Where(predicate);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<Bookmark> Select()
        {
            try
            {
                return db.Bookmarks.AsQueryable();
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<Bookmark, TResult>> selector)
        {
            try
            {
                return db.Bookmarks.Select(selector);
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
                if (db.Bookmarks.Any())
                    return db.Bookmarks.OrderByDescending(p => p.Id).First().Id;
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

        ~BookmarkRepository()
        {
            Dispose(false);
        }
    }
}