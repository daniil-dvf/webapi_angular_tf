using System;
using System.Security.Claims;
using System.Security.Principal;

namespace PetShop.Services.Abstractions.Jwt
{
    public interface IJwtService
    {
        string EncodeJwt(IPayload payload);
        string EncodeJwt(IPayload payload, DateTime exp);
        ClaimsPrincipal DecodeJwt(string jwt);
    }
}
