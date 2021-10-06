using Newtonsoft.Json;
using PetShop.API.Validators;
using System.ComponentModel.DataAnnotations;

namespace PetShop.API.Dto.Animal
{
    public class AnimalDto
    {

        public int Id { get; set; }

        public string Name { get; set; }
    }
}