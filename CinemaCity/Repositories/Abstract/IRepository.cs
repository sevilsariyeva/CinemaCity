namespace CinemaCity.Repositories.Abstract
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(string id);
        Task<IEnumerable<T>> GetAll();
        Task AddAsync(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
