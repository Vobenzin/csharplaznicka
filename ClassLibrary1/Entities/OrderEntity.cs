using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Entities
{
    public class OrderEntity
    {
        public long Id { get; set; }
        public Guid PublicId { get; set; } = Guid.NewGuid();

        public long UserId { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public decimal TotalPrice { get; set; } // snapshot total at purchase time
        public List<OrderItemEntity> Items { get; set; } = new();
    }
}
