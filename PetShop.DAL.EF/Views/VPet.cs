using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.DAL.EF.Views
{
    public class VPet
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public DateTime BirthDate { get; set; }
        public int? BreedId { get; set; }
        public string BreedName { get; set; }
        public int? AnimalId { get; set; }
        public string AnimalName { get; set; }
        public byte[] Image { get; set; }
        public string ImageMimeType { get; set; }
        public bool Vaccinated { get; set; }
        public string Description { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? PetStatusId { get; set; }
        public string PetStatusName { get; set; }
        public int? UserId { get; set; }
    }
}
