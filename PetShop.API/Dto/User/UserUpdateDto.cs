using Newtonsoft.Json;
using PetShop.API.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace PetShop.API.Dto.User
{
    public class UserUpdateDto
    {
        [JsonProperty(Required = Required.DisallowNull)]
        [Required]
        public int Id { get; set; }

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
