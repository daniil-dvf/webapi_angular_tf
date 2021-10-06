using PetShop.DAL.EF.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.DAL.EF.Views
{
    public class VUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public Guid Salt { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime? BirthDate { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
