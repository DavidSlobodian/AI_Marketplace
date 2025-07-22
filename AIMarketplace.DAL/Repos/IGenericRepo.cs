using AIMarketplace.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AIMarketplace.DAL.Repos
{
    public interface IGenericRepo<T> where T : BaseEntity, new()
    {
        Task<Guid> Add (T entity);
        Task<bool> Update(T entity);
        Task<bool> DeleteById(Guid id);
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetByPredicate(Expression<Func<T, bool>> predicate);
    }
}
