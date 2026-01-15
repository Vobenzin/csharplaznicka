using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using biznis.Interfaces.Repository;
using biznis.Interfaces.Services;
using biznis.Repository;
using Common.DTO;

namespace biznis.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;


        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductDTO>> GetAllAsync()
        {
            var productList = await _productRepository.GetAllAsync();
            var productModelList = new List<ProductDTO>();

            foreach (var product in productList)
            {
                var productModel = new ProductDTO()
                {
                    PublicId = product.PublicId,
                    Name = product.Name,
                    Description = product.Description
                };
                productModelList.Add(productModel);
            }

            return productModelList;
        }
    }
}
