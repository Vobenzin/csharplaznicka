using biznis.Interfaces.Repository;
using biznis.Interfaces.Services;
using biznis.Repository;
using ClassLibrary1;
using ClassLibrary1.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Common.DTO;
using Common.Enum;

namespace biznis.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;


        public UserService(IUserRepository userRepository) {
            _userRepository = userRepository; 
        }


        public async Task<bool> CreateAsync(string name, string email, string password)
        {
            var userEntity = new UserEntity()
            {
                PublicId = Guid.NewGuid(),
                Name = name,
                Email = email,
                Password = password
            };
            await _userRepository.CreateAsync(userEntity);
            await _userRepository.SaveChangesAsync();
            return true;
        }
        public async Task<bool> CreateAdminAsync(string name, string email, string password)
        {
            var userEntity = new UserEntity()
            {
                PublicId = Guid.NewGuid(),
                Name = name,
                Email = email,
                Password = password,
                Role = RoleEnum.admin
            };
            await _userRepository.CreateAsync(userEntity);
            await _userRepository.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteAsync(Guid publicId)
        {
            var user = await _userRepository.GetByPublicIdAsync(publicId);
            if (user == null)
            {
                return false;
            }
            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            var userList = await _userRepository.GetAllAsync();
            var userModelList = new List<UserDTO>();

            foreach (var user in userList)
            {
                var userModel = new UserDTO()
                {
                    PublicId = user.PublicId,
                    Name = user.Name,
                    Email = user.Email
                };
                userModelList.Add(userModel);
            }

            return userModelList;
        }
        public async Task<UserEntity?> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.GetByCredentialsAsync(email, password);
            return user;
        }

        public async Task<UserEntity?> GetByPublicIdAsync(Guid publicId)
        {
            var user = await _userRepository.GetByPublicIdAsync(publicId);
            return user;
        }


        public async Task<bool> UpdateAsync(Guid publicId, string name, string email)
        {
            var user = _userRepository.GetByPublicIdAsync(publicId).Result;
            if (user == null)
            {
                return false;
            }
            if (name != "" && name != null)
            {
                user.Name = name;
            }
            if (email != "" && email != null)
            {
                user.Email = email;
            }
            await _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }
    }
}
