using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class CartDTO
    {
        public Guid PublicId { get; set; }
        public List<CartItemDTO> Items { get; set; } = new();

        public int TotalItems => Items.Sum(i => i.Amount);
        public decimal TotalPrice => Items.Sum(i => i.Subtotal);
    }
}
