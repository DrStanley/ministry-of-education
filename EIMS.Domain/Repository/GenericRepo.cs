using EIMS.Domain.Data;
using EIMS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;

namespace EIMS.Domain.Repository
{
    public class GenericRepo<TEntity> : IGenericRepo<TEntity> where TEntity : class
    {
        protected DbContext _context;

        public GenericRepo(DbContext dbContext)
        {
            _context = dbContext;
        }

        private IQueryable<TEntity> GetAllIncluding(Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = _context.Set<TEntity>();
            return includeProperties.Aggregate(queryable,
                (current, includeProperty) => current.Include(includeProperty));
        }

        public void AddRange(IEnumerable<TEntity> obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> AllInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
	        return GetAllIncluding(includeProperties).ToList();

        }

        public void Delete(int id)
        {
            TEntity entity = _context.Set<TEntity>().Find(id);
            _context.Set<TEntity>().Remove(entity);
        }

        public TEntity FindOne(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

		public IEnumerable<TEntity> FindByInclude(Expression<Func<TEntity, bool>> predicate,
			params Expression<Func<TEntity, object>>[] includeProperties)
        {
	        var query = GetAllIncluding(includeProperties);
	        IEnumerable<TEntity> results = query.Where(predicate).ToList();
	        return results;

        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public TEntity FindOneInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = GetAllIncluding(includeProperties);
            TEntity entity = query.FirstOrDefault(predicate);
            return entity;
        }

        public void Insert(TEntity obj)
        {
            _context.Set<TEntity>().Add(obj);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            _context.Set<TEntity>().Add(obj);
            _context.Entry(obj).State = EntityState.Modified;

        }

        public IDbContextTransaction GetDbContextTransaction()
        {
	        return _context.Database.BeginTransaction();
        }

    }
}
