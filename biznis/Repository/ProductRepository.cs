using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using biznis.Interfaces.Repository;
using ClassLibrary1;
using ClassLibrary1.Entities;

namespace biznis.Repository
{
    public class ProductRepository : BaseRepository<ProductEntity>, IProductRepository
    {

        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;

        }
    }
}
