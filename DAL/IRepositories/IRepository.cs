namespace DAL.IRepositories
{
    public interface IRepository<T>
    {
        Task<T?> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Delete(T entity);
    }
}