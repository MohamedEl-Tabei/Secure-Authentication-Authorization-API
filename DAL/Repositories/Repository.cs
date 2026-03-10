using DAL.Context;
using DAL.IRepositories;

namespace DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MyContext _myContext;

        public Repository(MyContext myContext)
        {
            _myContext = myContext;
        }
        public async Task AddAsync(T entity)
        {
            await _myContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<T?> GetByIdAsync(string id)
        {
           return await _myContext.Set<T>().FindAsync(id);
        }

       
    }
}