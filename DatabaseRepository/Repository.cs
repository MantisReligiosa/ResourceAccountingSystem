using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BLL.Exceptions;
using DomainInterfaces;
using RepositoryInterfaces;

namespace DatabaseRepository
{
    public class Repository : IDatabaseRepository
    {
        private readonly DatabaseContext _dataContext;
        public Repository(string connectionString)
        {
            _dataContext = new DatabaseContext(connectionString);
        }

        public void SaveChanges()
        {
            _dataContext.ChangeTracker.DetectChanges();
            _dataContext.SaveChanges();
        }

        public void AddOrEdit<TEntity>(TEntity entity)
            where TEntity : class, IIdentified
        {
            try
            {
                if (entity.Id == default(int))
                {
                    _dataContext.Set<TEntity>().Add(entity);
                }
                else
                {
                    _dataContext.Entry(entity).State = EntityState.Modified;
                }
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Ошибка при добавлении/редактировании элемента", ex);
            }
        }

        public bool Any<TEntity>(Func<TEntity, bool> predicate)
            where TEntity : class, IIdentified
        {
            try
            {
                return _dataContext.Set<TEntity>().Any(predicate);
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Ошибка обработки данных", ex);
            }
        }

        public void DeleteById<TEntity>(int id)
            where TEntity : class, IIdentified
        {
            try
            {
                _dataContext.Entry(_dataContext.Set<TEntity>().Single(s => s.Id == id)).State = EntityState.Deleted;
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Ошибка удаления данных", ex);
            }
        }

        public async Task<TResult> GetMaxAsync<TEntity, TResult>(Expression<Func<TEntity, TResult>> selector, 
            params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class, IIdentified
        {
            IQueryable<TEntity> query = _dataContext.Set<TEntity>();
            var queryWithInclude = includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await queryWithInclude.MaxAsync(selector);
        }

        public async Task<TResult> GetMinAsync<TEntity, TResult>(Expression<Func<TEntity, TResult>> selector, 
            params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class, IIdentified
        {
            IQueryable<TEntity> query = _dataContext.Set<TEntity>();
            var queryWithInclude = includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await queryWithInclude.MinAsync(selector);
        }

        public IQueryable<TEntity> GetQueryable<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class, IIdentified
        {
            IQueryable<TEntity> query = _dataContext.Set<TEntity>();
            var queryWithInclude = includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return queryWithInclude;
        }

        public async Task<List<TEntity>> GetAllAsync<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class, IIdentified
        {
            IQueryable<TEntity> query = _dataContext.Set<TEntity>();
            var queryWithInclude = includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await queryWithInclude.ToListAsync();
        }
    }
}
