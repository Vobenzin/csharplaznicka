using ClassLibrary1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biznis.Interfaces.Repository
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        Task<bool> LoginAsync(string name, string password);
    }
}
