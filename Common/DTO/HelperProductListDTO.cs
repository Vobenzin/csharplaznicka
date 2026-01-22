using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enum;

namespace Common.DTO
{
    public class HelperProductListDTO
    {
        public CategoryEnum Category { get; set; }
        public PlatformEnum Platform { get; set; }
        public IEnumerable<ProductListDTO> enumProductListDTO { get; set; }

    }
}
