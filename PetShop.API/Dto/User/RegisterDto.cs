using Newtonsoft.Json;
using PetShop.API.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace PetShop.API.Dto.User
{
    public class RegisterDto
    {
        [JsonProperty(Required = Required.DisallowNull)]
        [Required]
        [EmailAddress]
        [UniqueUserEmail]
        [MaxLength(255)]
        public string Email { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [NotAfterToday]
        public DateTime? BirthDate { get; set; }
    }
}
