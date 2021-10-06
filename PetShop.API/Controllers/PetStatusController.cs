using Microsoft.AspNetCore.Mvc;
using PetShop.API.Dto.Status;
using PetShop.API.UOW;
using PetShop.API.UOW.Abstractions;
using System.Collections.Generic;

namespace PetShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetStatusController : MasterController
    {
        public PetStatusController(UnitOfWork uow) : base(uow)
        {
        }

        [HttpGet]
        [Produces("application/json", Type = typeof(IEnumerable<PetStatusDto>))]
        public IActionResult Get()
        {
            return Ok(Uow.Get<IPetStatusService>().GetAll());
        }
    }
}
