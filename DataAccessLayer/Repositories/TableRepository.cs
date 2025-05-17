using DataAccessLayer.Interfsce;
using DataAccessLayer.Exceptions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories
{
    public class TableRepository<T> : ITableRepository<T> where T : BaseTable
    {
        private readonly SchoolManagementSystemContext _DbEventManagementSystemContext;
        private readonly DbSet<T> _dbSet;
        private readonly ILogger<TableRepository<T>> _logger;

        public TableRepository(SchoolManagementSystemContext shippingDbContext, ILogger<TableRepository<T>> log)
        {
            _DbEventManagementSystemContext = shippingDbContext;
            _dbSet = _DbEventManagementSystemContext.Set<T>();
            _logger = log;
        }

        public List<T> GetAll()
        {
            try
            {
                return _dbSet.Where(a => a.CurrentState == 1).ToList();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, " ", _logger);
            }
        }

        public T GetById(Guid id)
        {
            try
            {
                return _dbSet.Where(a => a.Id == id).AsNoTracking().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "", _logger);
            }
        }

        public bool Add(T entity)
        {
            try
            {
                _dbSet.Add(entity);
                _DbEventManagementSystemContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "", _logger);
            }
        }

        public bool Update(T entity)
        {
            try
            {
                var dbData = GetById(entity.Id);
                entity.CreatedDate = dbData.CreatedDate;
                entity.CreatedBy = dbData.CreatedBy;
                entity.UpdatedDate = DateTime.Now;
                entity.CurrentState = dbData.CurrentState;
                       _DbEventManagementSystemContext.Entry(entity).State = EntityState.Modified;
                _DbEventManagementSystemContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "", _logger);
            }
        }
        public bool ShangeStatus(Guid id, Guid userId, int status = 1)
        {
            try
            {
                var entity = GetById(id);
                if (entity != null)
                {

                    entity.CurrentState = status;
                    entity.UpdatedBy = userId;
                    entity.UpdatedDate = DateTime.Now;
                    _DbEventManagementSystemContext.Entry(entity).State = EntityState.Modified;
                    _DbEventManagementSystemContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "", _logger);
            }
        }



        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            try
            {
                return _dbSet.Where(filter).AsNoTracking().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "", _logger);
            }
        }

        public List<T> GetList(Expression<Func<T, bool>> filter)
        {
            try
            {
                return _dbSet.Where(filter).AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "", _logger);
            }
        }
    }
}