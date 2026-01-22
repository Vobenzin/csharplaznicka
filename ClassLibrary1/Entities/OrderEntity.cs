using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Entities
{
    public class OrderEntity : BaseEntity
    {

        public long UserId { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public decimal TotalPrice { get; set; }
        public List<OrderItemEntity> Items { get; set; } = new();
    }
}
