using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PetShop.DAL.EF.Entities
{

    [Table("User")]
    public partial class User
    {
        public User()
        {
            Pets = new HashSet<Pet>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public Guid Salt { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime? BirthDate { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Pet> Pets { get; set; }
    }
}
