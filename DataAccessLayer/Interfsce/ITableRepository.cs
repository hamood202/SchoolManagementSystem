using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfsce
{
    public interface ITableRepository<T> where T : BaseTable
    { 
        List<T> GetAll();
        T GetById(Guid id);
        bool Add(T entity);
        bool Update(T entity);
        bool ShangeStatus(Guid id, Guid userId ,int status=1 );
        T GetFirstOrDefault(Expression<Func<T, bool>> filter);
        List<T> GetList(Expression<Func<T, bool>> filter);
    }
}
