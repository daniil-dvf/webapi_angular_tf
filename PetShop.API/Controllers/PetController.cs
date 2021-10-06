using Microsoft.AspNetCore.Mvc;
using PetShop.API.Dto;
using PetShop.API.Dto.Pet;
using PetShop.API.Filters;
using PetShop.API.UOW;
using PetShop.API.UOW.Abstractions;
using ToolBox.Mappers.Extensions;

namespace PetShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : MasterController
    {
        public PetController(UnitOfWork uow) : base(uow)
        {
        }

        [HttpGet]
        [Route("{id}")]
        [Produces("application/json", Type = typeof(PetDetailsDto))]
        public IActionResult Get(int id)
        {
            PetDetailsDto toReturn = Uow.Get<IPetService>().Get(id);
            if (toReturn == null)
            {
                return NotFound();
            }
            return Ok(toReturn);
        }

        [HttpGet]
        [Produces("application/json", Type = typeof(FilterResultsDto<PetDto>))]
        public IActionResult Get(
            [FromQuery]int limit = 10,
            [FromQuery]int offset = 0,
            [FromQuery]int? breedId = null,
            [FromQuery]int? animalId = null, 
            [FromQuery]int? petStatusId = null,
            [FromQuery]string referenceSearch = null
        )
        {
            return Ok(Uow.Get<IPetService>().GetAll(limit, offset, referenceSearch, breedId, animalId, petStatusId));
        }

        [HttpPost]
        [ApiAuthorize("ADMIN")]
        [Produces("application/json", Type = null)]
        public IActionResult Post([FromBody] PetInsertDto dto)
        {
            Uow.Get<IPetService>().Insert(dto);
            Uow.Save();
            return Ok();
        }

        [HttpPut]
        [ApiAuthorize("ADMIN")]
        [Produces("application/json", Type = null)]
        public IActionResult Put([FromBody] PetUpdateDto dto)
        {
            if (Uow.Get<IPetService>().Update(dto))
            {
                Uow.Save();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("{id}")]
        [ApiAuthorize("ADMIN")]
        [Produces("application/json", Type = null)]
        public IActionResult Delete(int id)
        {
            if (Uow.Get<IPetService>().Delete(id))
            {
                Uow.Save();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPatch]
        [ApiAuthorize("ADMIN", "CUSTOMER")]
        [Produces("application/json", Type = null)]
        public IActionResult Patch(PetUpdateStatusDto dto)
        {
            PetUpdateStatusDto old = Uow.Get<IPetService>().Get(dto.Id).MapTo<PetUpdateStatusDto>();
            int freeId = Uow.Get<IPetStatusService>().GetByStatusName("FREE");
            if (User.IsInRole("ADMIN"))
            {
                old.PetStatusId = dto.PetStatusId;
                old.UserId = dto.UserId;
                if(dto.PetStatusId == freeId || dto.UserId == null)
                {
                    old.UserId = null;
                    old.PetStatusId = freeId;
                }
            }
            else
            {
                int soldId = Uow.Get<IPetStatusService>().GetByStatusName("SOLD");
                if(old.UserId == null || (old.UserId == UserId && old.PetStatusId != soldId))
                {
                    old.PetStatusId = dto.PetStatusId;
                    old.UserId = UserId;
                    if (dto.PetStatusId == freeId)
                    {
                        old.UserId = null;
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            if (Uow.Get<IPetService>().UpdateStatus(old))
            {
                Uow.Save();
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("Image/{id}")]
        [Produces("image/png")]
        public IActionResult GetImage(int id)
        {
            PetDetailsDto toReturn = Uow.Get<IPetService>().Get(id);
            if (toReturn == null || toReturn.Image == null)
            {
                return NotFound();
            }
            return File(toReturn.Image, toReturn.ImageMimeType);
        }

        [HttpGet]
        [Route("Unique/{reference}")]
        [Produces("application/json", Type = typeof(bool))]
        [ApiAuthorize("ADMIN")]
        public IActionResult Get(string reference)
        {
            return Ok(Uow.Get<IPetService>().GetByReference(reference));
        }
    }

    
}
