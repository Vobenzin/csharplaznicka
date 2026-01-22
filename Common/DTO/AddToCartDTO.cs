using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class AddToCartDto
    {
        public Guid ProductPublicId { get; set; }
        public int Amount { get; set; }
    }
}
