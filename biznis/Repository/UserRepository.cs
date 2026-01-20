using biznis.Interfaces.Repository;
using ClassLibrary1;
using ClassLibrary1.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biznis.Repository
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;

        }
        public Task<bool> LoginAsync(string name, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Name == name && u.Password == password);
            return Task.FromResult(user != null);
        }
        
        public async Task<UserEntity> GetByCredentialsAsync(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }
}
