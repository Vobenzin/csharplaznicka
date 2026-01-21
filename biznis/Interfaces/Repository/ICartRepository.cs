using ClassLibrary1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biznis.Interfaces.Repository
{
    public interface ICartRepository : IBaseRepository<CartEntity>
    {
        Task<CartEntity?> GetByUserIdAsync(long userId);
        Task<CartEntity?> GetByUserIdWithItemsAsync(long userId);
    }
}
