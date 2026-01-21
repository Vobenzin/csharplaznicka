using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class CheckoutResultDTO
    {
        public Guid OrderPublicId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }
}
