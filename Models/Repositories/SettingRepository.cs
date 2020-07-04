using Models.Entities;
using System;
using System.Linq;
using System.Web;

namespace Models.Repositories
{
    public class SettingRepository : IDisposable, ISettingRepository
    {
        DatabaseContext db = null;

        public SettingRepository()
        {
            db = new DatabaseContext();
        }

        public bool Add(Setting entity, bool autoSave = true)
        {
            try
            {
                db.Settings.Add(entity);
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

        public bool Update(Setting entity, bool autoSave = true)
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

        public bool Delete(Setting entity, bool autoSave = true)
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
                var entity = db.Settings.Find(id);
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

        public Setting Find(int id)
        {
            try
            {
                return db.Settings.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<Setting> Where(System.Linq.Expressions.Expression<Func<Setting, bool>> predicate)
        {
            try
            {
                return db.Settings.Where(predicate);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<Setting> Select()
        {
            try
            {
                return db.Settings.AsQueryable();
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<Setting, TResult>> selector)
        {
            try
            {
                return db.Settings.Select(selector);
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
                if (db.Settings.Any())
                    return db.Settings.OrderByDescending(p => p.Id).First().Id;
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

        ~SettingRepository()
        {
            Dispose(false);
        }
    }
}