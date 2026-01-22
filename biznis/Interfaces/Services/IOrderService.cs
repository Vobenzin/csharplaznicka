using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biznis.Interfaces.Services
{
    public interface IOrderService
    {
        Task<CheckoutResultDTO> CheckoutAsync(long userId);
        Task<List<OrderListDTO>> GetMyOrdersAsync(long userId);
        Task<OrderDetailDTO?> GetMyOrderDetailAsync(long userId, Guid orderPublicId);
    }
}
