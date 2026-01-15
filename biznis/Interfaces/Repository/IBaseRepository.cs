using ClassLibrary1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biznis.Interfaces.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity?> GetByPublicIdAsync(Guid publicId);
        Task<bool> CreateAsync(TEntity entity);
        Task<bool> Update(TEntity entity);
        void Delete(TEntity entity);

        Task<int> SaveChangesAsync();
    }
}
