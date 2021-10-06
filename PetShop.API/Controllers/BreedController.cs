using Microsoft.AspNetCore.Mvc;
using PetShop.API.Dto.Breed;
using PetShop.API.Dto.Error;
using PetShop.API.Filters;
using PetShop.API.UOW;
using PetShop.API.UOW.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace PetShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreedController : MasterController
    {
        public BreedController(UnitOfWork uow) : base(uow)
        {
        }

        [HttpGet]
        [Produces("application/json", Type = typeof(IEnumerable<BreedDetailsDto>))]
        public IActionResult Get([FromQuery]int? animalId = null)
        {
            if(animalId is int id)
            {
                return Ok(Uow.Get<IBreedService>().GetAllByAnimal(id));
            }
            else
            {
                return Ok(Uow.Get<IBreedService>().GetAll());
            }
        }

        [HttpPost]
        [ApiAuthorize("ADMIN")]
        [Produces("application/json", Type = null)]
        public IActionResult Post([FromBody] BreedInsertDto dto)
        {
            IAnimalService aRepo = Uow.Get<IAnimalService>();
            if (aRepo.GetAll().FirstOrDefault(a => a.Id == dto.AnimalId) == null)
            {
                return BadRequest(new ErrorDto("animalId", "Verifie tes données"));
            }
            IEnumerable<BreedDetailsDto> breeds = Uow.Get<IBreedService>().GetAll();
            if (breeds.FirstOrDefault(b => b.Name == dto.Name && b.AnimalId == dto.AnimalId) != null)
            {
                return BadRequest(new ErrorDto("name", "This name already exists"));
            }
            Uow.Get<IBreedService>().Insert(dto);
            Uow.Save();
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        [ApiAuthorize("ADMIN")]
        [Produces("application/json", Type = null)]
        public IActionResult Delete(int id)
        {
            if (Uow.Get<IBreedService>().Delete(id))
            {
                Uow.Save();
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("Unique/{animalId}/{name}")]
        [Produces("application/json", Type = typeof(bool))]
        public IActionResult Get(string name, int animalId)
        {
            return Ok(Uow.Get<IBreedService>().GetByNameAndAnimalId(name, animalId));
        }
    }
}
