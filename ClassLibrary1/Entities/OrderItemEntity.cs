using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Entities
{
    public class OrderItemEntity
    {
        public long Id { get; set; }
        public Guid PublicId { get; set; } = Guid.NewGuid();

        public long OrderId { get; set; }
        public OrderEntity? Order { get; set; }

        public long ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public decimal UnitPrice { get; set; }
        public int Amount { get; set; }
    }
}
