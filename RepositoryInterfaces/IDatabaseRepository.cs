using DomainInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RepositoryInterfaces
{
    public interface IDatabaseRepository
    {
        void AddOrEdit<TEntity>(TEntity entity) where TEntity : class, IIdentified;
        IQueryable<TEntity> GetQueryable<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class, IIdentified;
        Task<IEnumerable<TEntity>> GetAsync<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class, IIdentified;
        Task<TEntity> GetMaxAsync<TEntity>(Expression<Func<TEntity, object>> property, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class, IIdentified;
        Task<TEntity> GetMinAsync<TEntity>(Expression<Func<TEntity, object>> property, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class, IIdentified;
        void DeleteById<TEntity>(int id) where TEntity : class, IIdentified;
        bool Any<TEntity>(Func<TEntity, bool> predicate) where TEntity : class, IIdentified;
        void SaveChanges();
    }
}
