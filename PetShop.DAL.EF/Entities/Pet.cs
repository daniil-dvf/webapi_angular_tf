using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShop.DAL.EF.Entities
{
    [Table("Pet")]
    public partial class Pet
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public DateTime BirthDate { get; set; }
        public int? BreedId { get; set; }
        public byte[] Image { get; set; }
        public string ImageMimeType { get; set; }
        public bool Vaccinated { get; set; }
        public string Description { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int PetStatusId { get; set; }
        public int? UserId { get; set; }

        public virtual Breed Breed { get; set; }
        public virtual PetStatus PetStatus { get; set; }
        public virtual User User { get; set; }
    }
}
