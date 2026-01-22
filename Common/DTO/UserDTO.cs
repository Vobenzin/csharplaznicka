using Common.Enum;

namespace Common.DTO
{
    public class UserDTO
    {
        public Guid PublicId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public RoleEnum Role { get; set; }
    }
}
