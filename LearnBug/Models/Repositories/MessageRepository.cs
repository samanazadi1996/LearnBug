using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LearnBug.Models.Repositories
{
    public class MessageRepository : IDisposable
    {
        private LearnBug.Models.DomainModels.LearnBugDBEntities1 db = null;

        public MessageRepository()
        {
            db = new DomainModels.LearnBugDBEntities1();
        }

        public bool Add(LearnBug.Models.DomainModels.Message entity, bool autoSave = true)
        {
            try
            {
                db.Messages.Add(entity);
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

        public bool Update(LearnBug.Models.DomainModels.Message entity, bool autoSave = true)
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

        public bool Delete(LearnBug.Models.DomainModels.Message entity, bool autoSave = true)
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
                var entity = db.Messages.Find(id);
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

        public LearnBug.Models.DomainModels.Message Find(int id)
        {
            try
            {
                return db.Messages.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<LearnBug.Models.DomainModels.Message> Where(System.Linq.Expressions.Expression<Func<LearnBug.Models.DomainModels.Message, bool>> predicate)
        {
            try
            {
                return db.Messages.Where(predicate);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<LearnBug.Models.DomainModels.Message> Select()
        {
            try
            {
                return db.Messages.AsQueryable();
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<LearnBug.Models.DomainModels.Message, TResult>> selector)
        {
            try
            {
                return db.Messages.Select(selector);
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
                if (db.Messages.Any())
                    return db.Messages.OrderByDescending(p => p.Id).First().Id;
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

        ~MessageRepository()
        {
            Dispose(false);
        }
    }
}