using Microsoft.AspNetCore.Mvc;
using PetShop.API.Dto.Animal;
using PetShop.API.Filters;
using PetShop.API.UOW;
using PetShop.API.UOW.Abstractions;
using System.Collections.Generic;

namespace PetShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : MasterController
    {
        public AnimalController(UnitOfWork uow) : base(uow)
        {
        }

        [HttpPost]
        [ApiAuthorize("ADMIN")]
        [Produces("application/json", Type = null)]
        public IActionResult Post([FromBody] AnimalInsertDto dto)
        {
            Uow.Get<IAnimalService>().Insert(dto);
            Uow.Save();
            return Ok();
        }

        [HttpGet]
        [Produces("application/json", Type = typeof(IEnumerable<AnimalDto>))]
        public IActionResult Get()
        {
            return Ok(Uow.Get<IAnimalService>().GetAll());
        }

        [HttpDelete]
        [Route("{id}")]
        [ApiAuthorize("ADMIN")]
        [Produces("application/json", Type = null)]
        public IActionResult Delete(int id)
        {
            if (Uow.Get<IAnimalService>().Delete(id))
            {
                Uow.Save();
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("Unique/{name}")]
        [Produces("application/json", Type = typeof(bool))]
        public IActionResult Get(string name)
        {
            return Ok(Uow.Get<IAnimalService>().GetByName(name));
        }
    }
}
