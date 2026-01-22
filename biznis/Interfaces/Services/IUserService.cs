using ClassLibrary1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace biznis.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<Common.DTO.UserDTO>> GetAllAsync();
        Task<UserEntity> GetByPublicIdAsync(Guid publicId);

        Task<UserEntity?> AuthenticateAsync(string email, string password);
        Task<bool> CreateAsync(string name, string email, string password);
        Task<bool> CreateAdminAsync(string name, string email, string password);
        Task<bool> UpdateAsync(Guid publicId, string name, string email);
        Task<bool> DeleteAsync(Guid publicId);

    }
}
