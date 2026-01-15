using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biznis.Interfaces.Services
{
    public interface IProductService
    {
        Task<List<Common.DTO.ProductDTO>> GetAllAsync();
    }
}
