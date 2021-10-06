using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShop.DAL.EF.Entities
{
    [Table("Breed")]
    public partial class Breed
    {
        public Breed()
        {
            Pets = new HashSet<Pet>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int AnimalId { get; set; }

        public virtual Animal Animal { get; set; }
        public virtual ICollection<Pet> Pets { get; set; }
    }
}
