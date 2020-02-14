using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LearnBug.Models.Repositories
{
    public class GroupRepository : IDisposable
    {
        private LearnBug.Models.DomainModels.LearnBugDBEntities1 db = null;

        public GroupRepository()
        {
            db = new DomainModels.LearnBugDBEntities1();
        }

        public bool Add(LearnBug.Models.DomainModels.Group entity, bool autoSave = true)
        {
            try
            {
                db.Groups.Add(entity);
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

        public bool Update(LearnBug.Models.DomainModels.Group entity, bool autoSave = true)
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

        public bool Delete(LearnBug.Models.DomainModels.Group entity, bool autoSave = true)
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
                var entity = db.Groups.Find(id);
                db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                if (autoSave)
                {
                    bool result = Convert.ToBoolean(db.SaveChanges());
                   
                    return result;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public LearnBug.Models.DomainModels.Group Find(int id)
        {
            try
            {
                return db.Groups.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<LearnBug.Models.DomainModels.Group> Where(System.Linq.Expressions.Expression<Func<LearnBug.Models.DomainModels.Group, bool>> predicate)
        {
            try
            {
                return db.Groups.Where(predicate);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<LearnBug.Models.DomainModels.Group> Select()
        {
            try
            {
                return db.Groups.AsQueryable();
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<LearnBug.Models.DomainModels.Group, TResult>> selector)
        {
            try
            {
                return db.Groups.Select(selector);
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
                if (db.Groups.Any())
                    return db.Groups.OrderByDescending(p => p.Id).First().Id;
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

        ~GroupRepository()
        {
            Dispose(false);
        }
    }
}