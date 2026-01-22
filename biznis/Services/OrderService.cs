using biznis.Interfaces.Repository;
using biznis.Interfaces.Services;
using ClassLibrary1;
using ClassLibrary1.Entities;
using Common.DTO;
using Microsoft.EntityFrameworkCore;

namespace biznis.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context; // easiest for multi-entity transaction

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CheckoutResultDTO> CheckoutAsync(long userId)
        {
            // load cart + items + product
            var cart = await _context.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || cart.Items.Count == 0)
                throw new Exception("Cart is empty.");

            // validate stock
            foreach (var item in cart.Items)
            {
                if (item.Product == null)
                    throw new Exception("Product missing.");
                if (item.Amount <= 0)
                    throw new Exception("Invalid amount.");
                if (item.Product.Amount < item.Amount)
                    throw new Exception($"Not enough stock for {item.Product.Name}.");
            }

            // create order + snapshot items
            var order = new OrderEntity
            {
                PublicId = Guid.NewGuid(),
                UserId = userId,
                CreatedAtUtc = DateTime.UtcNow,
            };

            decimal total = 0;

            foreach (var item in cart.Items)
            {
                var p = item.Product!;

                // reduce stock
                p.Amount -= item.Amount;

                var oi = new OrderItemEntity
                {
                    PublicId = Guid.NewGuid(),
                    ProductId = p.Id,
                    ProductName = p.Name,
                    UnitPrice = p.Price,
                    Amount = item.Amount
                };

                order.Items.Add(oi);
                total += oi.UnitPrice * oi.Amount;
            }

            order.TotalPrice = total;

            // clear cart
            _context.CartItems.RemoveRange(cart.Items);

            // save all at once (transaction by default)
            _context.Orders.Add(order);
            await _context.SaveChangesAsync(); // atomic with supported providers [web:509]

            return new CheckoutResultDTO
            {
                OrderPublicId = order.PublicId,
                TotalPrice = order.TotalPrice,
                CreatedAtUtc = order.CreatedAtUtc
            };
        }

        public async Task<List<OrderListDTO>> GetMyOrdersAsync(long userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAtUtc)
                .Select(o => new OrderListDTO
                {
                    PublicId = o.PublicId,
                    CreatedAtUtc = o.CreatedAtUtc,
                    TotalPrice = o.TotalPrice
                })
                .ToListAsync();
        }

        public async Task<OrderDetailDTO?> GetMyOrderDetailAsync(long userId, Guid orderPublicId)
        {
            var o = await _context.Orders
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.UserId == userId && x.PublicId == orderPublicId);

            if (o == null) return null;

            return new OrderDetailDTO
            {
                PublicId = o.PublicId,
                CreatedAtUtc = o.CreatedAtUtc,
                TotalPrice = o.TotalPrice,
                Items = o.Items.Select(i => new OrderItemDTO
                {
                    ProductName = i.ProductName,
                    UnitPrice = i.UnitPrice,
                    Amount = i.Amount
                }).ToList()
            };
        }
    }
}
