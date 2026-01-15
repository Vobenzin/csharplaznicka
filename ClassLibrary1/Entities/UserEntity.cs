using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enum;

namespace ClassLibrary1.Entities
{
    public class UserEntity : BaseEntity 
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public RoleEnum Role { get; set; }

    }
}
