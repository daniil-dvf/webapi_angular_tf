using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Services.Abstractions.Jwt
{
    public interface IPayload
    {
        int Id { get; }
        string RoleName { get; }
        string Email { get; }
        string Name { get; }
    }
}
