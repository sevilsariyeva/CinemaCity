using System.Linq.Expressions;
using CinemaCity.Models;
using CinemaCity.Services.Abstract;

namespace CinemaCity.Services.Concrete
{
    public class UserService : IUserService
    {
        public void Add(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public User Get(Expression<Func<User, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
