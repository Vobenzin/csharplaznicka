using Common.DTO;

namespace biznis.Interfaces.Services
{
    public interface ICartService
    {
        Task<CartDTO> GetMyCartAsync(long userId);
        Task<bool> AddToCartAsync(long userId, Guid productPublicId, int amount);
        Task<bool> UpdateItemAmountAsync(long userId, Guid cartItemPublicId, int amount);
        Task<bool> RemoveItemAsync(long userId, Guid cartItemPublicId);
        Task<bool> ClearCartAsync(long userId);
    }
}
