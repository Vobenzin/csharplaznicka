using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Entities
{
    public class CartEntity : BaseEntity
    {

        public long UserId { get; set; }  // FK to your UserEntity.Id
        public List<CartItemEntity> Items { get; set; } = new();
    }
}
