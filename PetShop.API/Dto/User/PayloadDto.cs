using PetShop.Services.Abstractions.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.API.Dto.User
{
    public class PayloadDto : IPayload
    {
        public int Id { get; private set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime? BirthDate { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Name { get { return LastName + " " + FirstName; } }
    }
}
