namespace SocialPulse.Core.Repositories
{
    public interface IRepository<TEntity, TId> where TEntity : class where TId : struct
    {
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity?> GetAsync(TId id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TId id);

        IEnumerable<TEntity> Get();
        TEntity? Get(TId id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TId id);

    }
}
