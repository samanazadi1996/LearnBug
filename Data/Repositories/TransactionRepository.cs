using Data;
using Entities;
using System;
using System.Linq;
using System.Web;

namespace Data.Repositories
{
    public class TransactionRepository : IDisposable
    {
        DatabaseContext db = null;

        public TransactionRepository()
        {
            db = new DatabaseContext();
        }

        public bool Add(Transaction entity, bool autoSave = true)
        {
            try
            {
                db.transactions.Add(entity);
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

        public bool Update(Transaction entity, bool autoSave = true)
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

        public bool Delete(Transaction entity, bool autoSave = true)
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
                var entity = db.transactions.Find(id);
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

        public Transaction Find(int id)
        {
            try
            {
                return db.transactions.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<Transaction> Where(System.Linq.Expressions.Expression<Func<Transaction, bool>> predicate)
        {
            try
            {
                return db.transactions.Where(predicate);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<Transaction> Select()
        {
            try
            {
                return db.transactions.AsQueryable();
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<Transaction, TResult>> selector)
        {
            try
            {
                return db.transactions.Select(selector);
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
                if (db.transactions.Any())
                    return db.transactions.OrderByDescending(p => p.Id).First().Id;
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

        ~TransactionRepository()
        {
            Dispose(false);
        }
    }
}