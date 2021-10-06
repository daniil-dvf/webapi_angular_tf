using PetShop.Services.Abstractions.Jwt;
using System;

namespace PetShop.API.Dto.User
{
    public class UserDto
    {
        public int Id { get; private set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string RoleName { get; set; }
        public int RoleId { get; set; }
    }
}
