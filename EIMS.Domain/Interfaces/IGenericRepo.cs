using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EIMS.Domain.Interfaces
{
    public interface IGenericRepo<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        void Insert(TEntity obj);
        void Update(TEntity obj);
        void Delete(int id);
        int Save();
        TEntity FindOne(Expression<Func<TEntity, bool>> predicate);
        TEntity FindOneInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        void AddRange(IEnumerable<TEntity> obj);
        void RemoveRange(IEnumerable<TEntity> entities);
        IEnumerable<TEntity> AllInclude(params Expression<Func<TEntity, object>>[] includeProperties);

        IEnumerable<TEntity> FindByInclude(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties);

        IDbContextTransaction GetDbContextTransaction();
    }
}
