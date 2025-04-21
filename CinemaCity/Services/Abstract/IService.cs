using System.Linq.Expressions;

namespace CinemaCity.Services.Abstract
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        //T Get(Expression<Func<T, bool>> expression);
        Task<T> Get(string id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(string id);
    }
}
