using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PetShop.API.Dto.Breed
{
    public class BreedDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AnimalId { get; set; }
    }
}
