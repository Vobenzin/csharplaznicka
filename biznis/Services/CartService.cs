using biznis.Interfaces.Repository;
using biznis.Interfaces.Services;
using ClassLibrary1.Entities;
using Common.DTO;

namespace biznis.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepo;
        private readonly IBaseRepository<CartItemEntity> _cartItemRepo;
        private readonly IBaseRepository<ProductEntity> _productRepo;

        public CartService(
            ICartRepository cartRepo,
            IBaseRepository<CartItemEntity> cartItemRepo,
            IBaseRepository<ProductEntity> productRepo)
        {
            _cartRepo = cartRepo;
            _cartItemRepo = cartItemRepo;
            _productRepo = productRepo;
        }

        public async Task<CartDTO> GetMyCartAsync(long userId)
        {
            var cart = await _cartRepo.GetByUserIdWithItemsAsync(userId);

            if (cart == null)
            {
                cart = new CartEntity { PublicId = Guid.NewGuid(), UserId = userId };
                await _cartRepo.AddAsync(cart);
                await _cartRepo.SaveChangesAsync();
                cart = await _cartRepo.GetByUserIdWithItemsAsync(userId);
            }

            cart ??= new CartEntity { PublicId = Guid.NewGuid(), UserId = userId };

            return new CartDTO
            {
                PublicId = cart.PublicId,
                Items = cart.Items.Select(i => new CartItemDTO
                {
                    PublicId = i.PublicId,
                    ProductPublicId = i.Product!.PublicId,
                    ProductName = i.Product!.Name,
                    UnitPrice = i.Product!.Price,
                    Amount = i.Amount
                }).ToList()
            };
        }

        public async Task<bool> AddToCartAsync(long userId, Guid productPublicId, int amount)
        {
            if (amount <= 0) return false;

            var product = await _productRepo.GetByPublicIdAsync(productPublicId);
            if (product == null) return false;

            var cart = await _cartRepo.GetByUserIdWithItemsAsync(userId);
            if (cart == null)
            {
                cart = new CartEntity { PublicId = Guid.NewGuid(), UserId = userId };
                await _cartRepo.AddAsync(cart);
                await _cartRepo.SaveChangesAsync();
                cart = await _cartRepo.GetByUserIdWithItemsAsync(userId);
                if (cart == null) return false;
            }

            var existing = cart.Items.FirstOrDefault(i => i.ProductId == product.Id);
            if (existing != null)
            {
                existing.Amount += amount;
                _cartItemRepo.Update(existing);
            }
            else
            {
                var item = new CartItemEntity
                {
                    PublicId = Guid.NewGuid(),
                    CartId = cart.Id,
                    ProductId = product.Id,
                    Amount = amount
                };
                await _cartItemRepo.AddAsync(item);
            }

            await _cartItemRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateItemAmountAsync(long userId, Guid cartItemPublicId, int amount)
        {
            var cart = await _cartRepo.GetByUserIdWithItemsAsync(userId);
            if (cart == null) return false;

            var item = cart.Items.FirstOrDefault(i => i.PublicId == cartItemPublicId);
            if (item == null) return false;

            if (amount <= 0)
            {
                _cartItemRepo.Delete(item);
            }
            else
            {
                item.Amount = amount;
                _cartItemRepo.Update(item);
            }

            await _cartItemRepo.SaveChangesAsync();
            return true;
        }

        public Task<bool> RemoveItemAsync(long userId, Guid cartItemPublicId)
            => UpdateItemAmountAsync(userId, cartItemPublicId, 0);

        public async Task<bool> ClearCartAsync(long userId)
        {
            var cart = await _cartRepo.GetByUserIdWithItemsAsync(userId);
            if (cart == null) return true;

            foreach (var item in cart.Items)
                _cartItemRepo.Delete(item);

            await _cartItemRepo.SaveChangesAsync();
            return true;
        }
    }
}
