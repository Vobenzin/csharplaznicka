using ClassLibrary1.Entities;
using Common.DTO;
using Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biznis.Interfaces.Services
{
    public interface IProductService
    {
        Task<List<ProductListDTO>> GetAllAsync();

        Task<HelperProductListDTO> GetHelperAsync(List<ProductListDTO> products, CategoryEnum cat, PlatformEnum plat);

        Task<ProductDTO?> GetByPublicIdAsync(Guid publicId);

        Task<bool> CreateAsync(ProductUpsertDTO dto);
        Task<bool> UpdateAsync(Guid publicId, ProductUpsertDTO dto);
        Task<bool> DeleteAsync(Guid publicId);
    }
}
