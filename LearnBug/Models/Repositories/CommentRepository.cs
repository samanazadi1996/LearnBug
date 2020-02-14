using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LearnBug.Models.Repositories
{
    public class CommentRepository : IDisposable
    {
        private LearnBug.Models.DomainModels.LearnBugDBEntities1 db = null;

        public CommentRepository()
        {
            db = new DomainModels.LearnBugDBEntities1();
        }

        public bool Add(LearnBug.Models.DomainModels.Comment entity, bool autoSave = true)
        {
            try
            {
                db.Comments.Add(entity);
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

        public bool Update(LearnBug.Models.DomainModels.Comment entity, bool autoSave = true)
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

        public bool Delete(LearnBug.Models.DomainModels.Comment entity, bool autoSave = true)
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
                var entity = db.Comments.Find(id);
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

        public LearnBug.Models.DomainModels.Comment Find(int id)
        {
            try
            {
                return db.Comments.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<LearnBug.Models.DomainModels.Comment> Where(System.Linq.Expressions.Expression<Func<LearnBug.Models.DomainModels.Comment, bool>> predicate)
        {
            try
            {
                return db.Comments.Where(predicate);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<LearnBug.Models.DomainModels.Comment> Select()
        {
            try
            {
                return db.Comments.AsQueryable();
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<LearnBug.Models.DomainModels.Comment, TResult>> selector)
        {
            try
            {
                return db.Comments.Select(selector);
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
                if (db.Comments.Any())
                    return db.Comments.OrderByDescending(p => p.Id).First().Id;
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