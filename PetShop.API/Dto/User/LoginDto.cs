using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PetShop.API.Dto.User
{
    public class LoginDto
    {
        [JsonProperty(Required = Required.DisallowNull)]
        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        [Required]
        [MaxLength(255)]
        public string Password { get; set; }
    }
}
