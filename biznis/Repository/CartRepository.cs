using biznis.Interfaces.Repository;
using ClassLibrary1;
using ClassLibrary1.Entities;
using Microsoft.EntityFrameworkCore;

namespace biznis.Repository
{
    public class CartRepository : BaseRepository<CartEntity>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context) { }

        public Task<CartEntity?> GetByUserIdAsync(long userId)
            => _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);

        public Task<CartEntity?> GetByUserIdWithItemsAsync(long userId)
            => _context.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
    }
}
