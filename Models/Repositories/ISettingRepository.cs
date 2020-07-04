using Models.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Models.Repositories
{
    public interface ISettingRepository
    {
        bool Add(Setting entity, bool autoSave = true);
        bool Delete(int id, bool autoSave = true);
        bool Delete(Setting entity, bool autoSave = true);
        void Dispose();
        Setting Find(int id);
        int GetLastIdentity();
        int Save();
        IQueryable<Setting> Select();
        IQueryable<TResult> Select<TResult>(Expression<Func<Setting, TResult>> selector);
        bool Update(Setting entity, bool autoSave = true);
        IQueryable<Setting> Where(Expression<Func<Setting, bool>> predicate);
    }
}