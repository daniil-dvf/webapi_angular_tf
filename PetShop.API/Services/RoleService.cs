using PetShop.API.Dto.Role;
using PetShop.API.Dto.Status;
using PetShop.API.UOW.Abstractions;
using PetShop.DAL.EF.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolBox.Mappers.Extensions;

namespace PetShop.API.Services
{
    public class RoleService : BaseService, IRoleService
    {
        public RoleService(PetShopContext context) : base(context)
        {
        }

        public IEnumerable<RoleDto> GetAll()
        {
            return _context.Roles.ToList().Select(x => x.MapTo<RoleDto>());
        }
    }
}
