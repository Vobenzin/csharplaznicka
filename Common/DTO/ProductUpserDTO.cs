using Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class ProductUpsertDTO
    {
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public CategoryEnum Category { get; set; }
        public PlatformEnum Platform { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; } = "";
    }
}
