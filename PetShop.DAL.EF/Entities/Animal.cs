using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PetShop.DAL.EF.Entities
{
    [Table("Animal")]
    public partial class Animal
    {
        public Animal()
        {
            Breeds = new HashSet<Breed>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Breed> Breeds { get; set; }
    }
}
