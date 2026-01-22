using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using biznis.Interfaces.Repository;
using biznis.Interfaces.Services;
using biznis.Repository;
using ClassLibrary1.Entities;
using Common.DTO;
using Common.Enum;

namespace biznis.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ProductListDTO>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();

            return list.Select(p => new ProductListDTO
            {
                PublicId = p.PublicId,
                Name = p.Name,
                Price = p.Price,
                Category = p.Category,
                Platform = p.Platform,
                StockAmount = p.Amount
            }).ToList();
        }

        public async Task<HelperProductListDTO> GetHelperAsync(List<ProductListDTO> products, CategoryEnum cat, PlatformEnum plat)
        {
            return new HelperProductListDTO
            {
                Category = cat,
                Platform = plat,
                enumProductListDTO = products

            };
        }

        public async Task<ProductDTO?> GetByPublicIdAsync(Guid publicId)
        {
            var p = await _repo.GetByPublicIdAsync(publicId);
            if (p == null) return null;

            return new ProductDTO
            {
                PublicId = p.PublicId,
                Name = p.Name,
                Price = p.Price,
                Category = p.Category,
                Platform = p.Platform,
                Amount = p.Amount,
                Description = p.Description
            };
        }

        public async Task<bool> CreateAsync(ProductUpsertDTO dto)
        {
            var entity = new ProductEntity
            {
                PublicId = Guid.NewGuid(),
                Name = dto.Name,
                Price = dto.Price,
                Category = dto.Category,
                Platform = dto.Platform,
                Amount = dto.Amount,
                Description = dto.Description
            };

            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Guid publicId, ProductUpsertDTO dto)
        {
            var entity = await _repo.GetByPublicIdAsync(publicId);
            if (entity == null) return false;

            entity.Name = dto.Name;
            entity.Price = dto.Price;
            entity.Category = dto.Category;
            entity.Platform = dto.Platform;
            entity.Amount = dto.Amount;
            entity.Description = dto.Description;

            _repo.Update(entity);
            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid publicId)
        {
            var entity = await _repo.GetByPublicIdAsync(publicId);
            if (entity == null) return false;

            _repo.Delete(entity);
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}
