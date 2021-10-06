using Newtonsoft.Json;
using PetShop.API.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace PetShop.API.Dto.Pet
{
    public class PetInsertDto
    {
        public int Id { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        [Required]
        [UniquePetReference]
        public string Reference { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        [Required]
        [NotAfterToday]
        public DateTime BirthDate { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        [Required]
        public int? BreedId { get; set; }


        public byte[] Image { get; set; }
        public string ImageMimeType { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        [Required]
        public bool Vaccinated { get; set; }
        public string Description { get; set; }
    }
}
