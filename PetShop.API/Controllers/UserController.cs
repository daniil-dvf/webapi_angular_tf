using Microsoft.AspNetCore.Mvc;
using PetShop.API.Dto;
using PetShop.API.Dto.User;
using PetShop.API.Filters;
using PetShop.API.UOW;
using PetShop.API.UOW.Abstractions;

namespace PetShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : MasterController
    {
        public UserController(UnitOfWork uow) : base(uow)
        {

        }

        [HttpPost]
        [Produces("application/json", Type = null)]
        public IActionResult Post([FromBody] RegisterDto user)
        {
            Uow.Get<IUserService>().Insert(user);
            Uow.Save();
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        [ApiAuthorize("ADMIN", "CUSTOMER")]
        [Produces("application/json", Type = typeof(UserDetailsDto))]
        public IActionResult Get(int id)
        {
            if (!User.IsInRole("ADMIN") && id != UserId)
            {
                return Unauthorized();
            }
            UserDetailsDto dto = Uow.Get<IUserService>().GetDetails(id);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        [HttpGet]
        [ApiAuthorize("ADMIN")]
        [Produces("application/json", Type = typeof(FilterResultsDto<UserDto>))]
        public IActionResult Get(
            [FromQuery] int limit = 20,
            [FromQuery] int offset = 0,
            [FromQuery] int? roleId = null,
            [FromQuery] string emailSearch = null
        )
        {
            return Ok(Uow.Get<IUserService>().GetAll(limit, offset, roleId, emailSearch));
        }

        [HttpDelete]
        [Route("{id}")]
        [ApiAuthorize("ADMIN", "CUSTOMER")]
        [Produces("application/json", Type = null)]
        public IActionResult Delete(int id)
        {
            if ((!User.IsInRole("ADMIN") && id != UserId) || Uow.Get<IUserService>().Get(id).RoleName == "ADMIN")
            {
                return Unauthorized();
            }
            if (Uow.Get<IUserService>().Delete(id))
            {
                Uow.Save();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        [ApiAuthorize("ADMIN", "CUSTOMER")]
        [Produces("application/json", Type = null)]
        public IActionResult Put([FromBody] UserUpdateDto user)
        {
            if (!User.IsInRole("ADMIN") && user.Id != UserId)
            {
                return Unauthorized();
            }
            if (Uow.Get<IUserService>().Update(user))
            {
                Uow.Save();
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("Unique/{email}")]
        [Produces("application/json", Type = typeof(bool))]
        public IActionResult GetByName(string email)
        {
            return Ok(Uow.Get<IUserService>().GetByEmail(email));
        }
    }
}
