using PetShop.API.Dto.Role;
using System.Collections.Generic;

namespace PetShop.API.UOW.Abstractions
{
    public interface IRoleService
    {
        IEnumerable<RoleDto> GetAll();
    }
}