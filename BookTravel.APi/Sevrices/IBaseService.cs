

using BookTravel.APi.Models;
using System.Linq.Expressions;

namespace BookTravel.APi.Sevrices
{
    public interface IBaseService<T> where T : class
    {
        Task<T> CreateAsync(T Model);
        Task<T?> GetAsync(object Id);
        Task<T?> GetAsync(Expression<Func<T, bool>> Criteria, Expression<Func<T, object>> Include);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, object>> Include);
        Task<T?> GetAsync(Expression<Func<T, bool>> Criteria);
        Task<IEnumerable<T>> GetAsync();
        T Update(T Model);
        T Delete(T Model);
    }
}
