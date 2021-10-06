using Newtonsoft.Json;
using PetShop.API.Validators;
using System.ComponentModel.DataAnnotations;

namespace PetShop.API.Dto.Animal
{
    public class AnimalInsertDto
    {
        [JsonProperty(Required = Required.DisallowNull)]
        [MaxLength(50)]
        [Required]
        [UniqueAnimalName]
        public string Name { get; set; }
    }
}
