using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class OrderListDTO
    {
        public Guid PublicId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
