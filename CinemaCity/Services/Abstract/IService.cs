using System.Linq.Expressions;

namespace CinemaCity.Services.Abstract
{
    public interface IService<T>
    {
        IEnumerable<T> GetAll();
        //T Get(Expression<Func<T, bool>> expression);
        T Get(string id);
        void Add(T entity);
        void Update(T entity);
        void Delete(string id);
    }
}
