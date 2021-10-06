using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShop.DAL.EF.Entities
{
    [Table("Role")]
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
