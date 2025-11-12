using biznis.Interfaces.Services;
using ClassLibrary1;
using ClassLibrary1.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebApplication1.Models;

namespace biznis.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;


        public UserService(AppDbContext context) {  _context = context; }


        public Task<bool> CreateAsync(string name, string email)
        {
            var userEntity = new UserEntity()
            {
                Name = name,
                Email = email
            };
            _context.Users.Add(userEntity);
            _context.SaveChanges();
            return Task.FromResult(true);
        }


        public async Task<bool> DeleteAsync(Guid publicId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PublicId == publicId);
            if (user == null)
            {
                return false;
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }

        public async Task<List<User>> GetAllAsync()
        {
            var userList = await _context.Users.ToListAsync();
            var userModelList = new List<User>();

            foreach (var user in userList)
            {
                var userModel = new User()
                {
                    PublicId = user.PublicId,
                    Name = user.Name,
                    Email = user.Email
                };
                userModelList.Add(userModel);
            }

            return userModelList;
        }

        public async Task<UserEntity> GetByPublicIdAsync(Guid publicId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PublicId == publicId);
            return user;
        }


        public Task<bool> UpdateAsync(Guid publicId, string name, string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.PublicId == publicId);
            if (user == null)
            {
                return Task.FromResult(false);
            }
            if (name != "")
            {
                user.Name = name;
            }
            if (email != "")
            {
                user.Email = email;
            }
            _context.SaveChanges();
            return Task.FromResult(true);
        }
    }
}
