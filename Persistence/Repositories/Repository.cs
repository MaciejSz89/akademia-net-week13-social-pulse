using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SocialPulse.Core.Repositories;
using System;

namespace SocialPulse.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SocialPulseContext _context;

        public Repository(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<SocialPulseContext>();
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            var entry = await _context.Set<T>().AddAsync(entity);
            return entry.Entity; 
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            var existingEntity = await _context.Set<T>().FindAsync(_context.Entry(entity).Property("Id").CurrentValue);
            if (existingEntity == null) return false;

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            if (entity == null) return false;

            _context.Set<T>().Remove(entity);
            return true;
        }
    }

}
