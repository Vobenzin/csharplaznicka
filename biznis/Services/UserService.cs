using biznis.Interfaces.Repository;
using biznis.Interfaces.Services;
using ClassLibrary1.Entities;
using Common.DTO;
using Common.Enum;

namespace biznis.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CreateAsync(string name, string email, string password)
        {
            var userEntity = new UserEntity
            {
                PublicId = Guid.NewGuid(),
                Name = name,
                Email = email,
                Password = password,
                Role = RoleEnum.user // or RoleEnum.User depending on your enum
            };

            await _userRepository.AddAsync(userEntity);
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateAdminAsync(string name, string email, string password)
        {
            var userEntity = new UserEntity
            {
                PublicId = Guid.NewGuid(),
                Name = name,
                Email = email,
                Password = password,
                Role = RoleEnum.admin // match your enum casing
            };

            await _userRepository.AddAsync(userEntity);
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid publicId)
        {
            var user = await _userRepository.GetByPublicIdAsync(publicId);
            if (user == null) return false;

            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            var userList = await _userRepository.GetAllAsync();

            return userList.Select(u => new UserDTO
            {
                PublicId = u.PublicId,
                Name = u.Name,
                Email = u.Email
            }).ToList();
        }

        public Task<UserEntity?> AuthenticateAsync(string email, string password)
        {
            return _userRepository.GetByCredentialsAsync(email, password);
        }

        public Task<UserEntity?> GetByPublicIdAsync(Guid publicId)
        {
            return _userRepository.GetByPublicIdAsync(publicId);
        }

        public async Task<bool> UpdateAsync(Guid publicId, string name, string email)
        {
            var user = await _userRepository.GetByPublicIdAsync(publicId);
            if (user == null) return false;

            if (!string.IsNullOrWhiteSpace(name))
                user.Name = name;

            if (!string.IsNullOrWhiteSpace(email))
                user.Email = email;

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }
    }
}
