using Data;
using Entities;
using System;
using System.Linq;
using System.Web;

namespace Data.Repositories
{
    public class FollowRepository : IDisposable
    {
        DatabaseContext db = null;

        public FollowRepository()
        {
            db = new DatabaseContext();
        }

        public bool Add(Follow entity, bool autoSave = true)
        {
            try
            {
                db.Follows.Add(entity);
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

        public bool Update(Follow entity, bool autoSave = true)
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

        public bool Delete(Follow entity, bool autoSave = true)
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
                var entity = db.Follows.Find(id);
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

        public Follow Find(int id)
        {
            try
            {
                return db.Follows.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<Follow> Where(System.Linq.Expressions.Expression<Func<Follow, bool>> predicate)
        {
            try
            {
                return db.Follows.Where(predicate);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<Follow> Select()
        {
            try
            {
                return db.Follows.AsQueryable();
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<Follow, TResult>> selector)
        {
            try
            {
                return db.Follows.Select(selector);
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
                if (db.Follows.Any())
                    return db.Follows.OrderByDescending(p => p.Id).First().Id;
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

        ~FollowRepository()
        {
            Dispose(false);
        }
    }
}