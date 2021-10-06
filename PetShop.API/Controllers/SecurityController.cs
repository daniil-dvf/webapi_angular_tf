using Microsoft.AspNetCore.Mvc;
using PetShop.API.Dto.Error;
using PetShop.API.Dto.User;
using PetShop.API.Filters;
using PetShop.API.UOW;
using PetShop.API.UOW.Abstractions;
using PetShop.Services.Abstractions.Jwt;
using System;

namespace PetShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : MasterController
    {
        public SecurityController(UnitOfWork uow) : base(uow)
        {
        }

        [HttpPost]
        [Route("login")]
        [Produces("application/json", Type = typeof(string))]
        public IActionResult Login([FromBody] LoginDto dto, [FromServices] IJwtService jwtService)
        {
            PayloadDto user = Uow.Get<IUserService>().GetByCredentials(dto);
            if (user != null)
                return Ok(jwtService.EncodeJwt(user, DateTime.UtcNow.AddDays(7)));
            return BadRequest(new ErrorDto("Form", "Bad Credentials"));
        }

        [HttpGet]
        [Route("refreshToken")]
        [ApiAuthorize("ADMIN", "CUSTOMER")]
        [Produces("application/json", Type = typeof(string))]
        public IActionResult Login([FromServices] IJwtService jwtService)
        {
            PayloadDto user = Uow.Get<IUserService>().GetPayload(UserId);
            if (user != null)
                return Ok(jwtService.EncodeJwt(user));
            return Unauthorized();
        }
    }
}
