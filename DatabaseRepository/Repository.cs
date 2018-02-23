using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BLL.Exceptions;
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

        void IDatabaseRepository.AddOrEdit<TEntity>(TEntity entity)
        {
            try
            {

            }
            catch(Exception ex)
            {
                throw new DatabaseException("Ошибка при добавлении/редактировании элемента", ex);
            }
        }

        bool IDatabaseRepository.Any<TEntity>(Func<TEntity, bool> predicate)
        {
            throw new NotImplementedException();
        }

        void IDatabaseRepository.DeleteById<TEntity>(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<TEntity>> IDatabaseRepository.GetAsync<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        Task<TEntity> IDatabaseRepository.GetMaxAsync<TEntity>(Expression<Func<TEntity, object>> property, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        Task<TEntity> IDatabaseRepository.GetMinAsync<TEntity>(Expression<Func<TEntity, object>> property, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        IQueryable<TEntity> IDatabaseRepository.GetQueryable<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }
    }
}
