using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PetShop.API.Dto.Pet
{
    public class PetUpdateStatusDto
    {
        [JsonProperty(Required = Required.DisallowNull)]
        [Required]
        public int Id { get; set; }

        public int PetStatusId { get; set; }

        public int? UserId { get; set; }
    }
}
