using Models.Entities;
using System;
using System.Linq;
using System.Web;

namespace Models.Repositories
{
    public class FactorRepository : IDisposable
    {
        DatabaseContext db = null;

        public FactorRepository()
        {
            db = new DatabaseContext();
        }

        public bool Add(Factor entity, bool autoSave = true)
        {
            try
            {
                db.Factors.Add(entity);
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

        public bool Update(Factor entity, bool autoSave = true)
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

        public bool Delete(Factor entity, bool autoSave = true)
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
                var entity = db.Factors.Find(id);
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

        public Factor Find(int id)
        {
            try
            {
                return db.Factors.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<Factor> Where(System.Linq.Expressions.Expression<Func<Factor, bool>> predicate)
        {
            try
            {
                return db.Factors.Where(predicate);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<Factor> Select()
        {
            try
            {
                return db.Factors.AsQueryable();
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<Factor, TResult>> selector)
        {
            try
            {
                return db.Factors.Select(selector);
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
                if (db.Factors.Any())
                    return db.Factors.OrderByDescending(p => p.Id).First().Id;
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

        ~FactorRepository()
        {
            Dispose(false);
        }
    }
}