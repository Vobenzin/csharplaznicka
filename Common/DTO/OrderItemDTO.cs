using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class OrderItemDTO
    {
        public string ProductName { get; set; } = "";
        public decimal UnitPrice { get; set; }
        public int Amount { get; set; }

        public decimal Subtotal => UnitPrice * Amount;
    }
}
