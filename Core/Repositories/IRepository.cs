namespace SocialPulse.Core.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T?> GetAsync(int id);
        Task<T> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);

    }
}
