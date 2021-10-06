using Newtonsoft.Json;
using PetShop.API.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.API.Dto.Breed
{
    public class BreedInsertDto
    {
        [JsonProperty(Required = Required.DisallowNull)]
        [MaxLength(100)]
        [Required]
        [UniqueBreedNameAnimalId]
        public string Name { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        [Required]
        public int AnimalId { get; set; }
    }
}
