using biznis.Interfaces.Repository;
using ClassLibrary1;
using ClassLibrary1.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biznis.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly AppDbContext _context;
        protected DbSet<TEntity> Set => _context.Set<TEntity>();

        public BaseRepository(AppDbContext context) => _context = context;

        public Task<TEntity?> GetByIdAsync(long id) =>
            Set.FirstOrDefaultAsync(e => e.Id == id);

        public Task<TEntity?> GetByPublicIdAsync(Guid publicId) =>
            Set.FirstOrDefaultAsync(e => e.PublicId == publicId);

        public Task<List<TEntity>> GetAllAsync() => Set.ToListAsync();

        public Task AddAsync(TEntity entity) => Set.AddAsync(entity).AsTask();
        public void Update(TEntity entity) => Set.Update(entity);
        public void Delete(TEntity entity) => Set.Remove(entity);

        public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
    }
}
