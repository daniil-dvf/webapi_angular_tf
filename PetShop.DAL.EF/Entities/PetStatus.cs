using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PetShop.DAL.EF.Entities
{
    [Table("PetStatus")]
    public partial class PetStatus
    {
        public PetStatus()
        {
            Pets = new HashSet<Pet>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Pet> Pets { get; set; }
    }
}
