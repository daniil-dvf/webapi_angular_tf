using Microsoft.AspNetCore.Mvc;
using PetShop.API.Dto.Role;
using PetShop.API.UOW;
using PetShop.API.UOW.Abstractions;
using System.Collections.Generic;

namespace PetShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : MasterController
    {
        public RoleController(UnitOfWork uow) : base(uow)
        {
        }

        [HttpGet]
        [Produces("application/json", Type = typeof(IEnumerable<RoleDto>))]
        public IActionResult Get()
        {
            return Ok(Uow.Get<IRoleService>().GetAll());
        }
    }
}
