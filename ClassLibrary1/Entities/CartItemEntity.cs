using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Entities
{
    public class CartItemEntity : BaseEntity
    {

        public long CartId { get; set; }
        public CartEntity Cart { get; set; } = null!;

        public long ProductId { get; set; }
        public ProductEntity Product { get; set; } = null!;

        public int Amount { get; set; }
    }
}
