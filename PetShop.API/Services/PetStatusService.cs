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
    public class PetStatusService : BaseService, IPetStatusService
    {
        public PetStatusService(PetShopContext context) : base(context)
        {
        }

        public IEnumerable<PetStatusDto> GetAll()
        {
            return _context.PetStatuses.ToList().Select(x => x.MapTo<PetStatusDto>());
        }

        public int GetByStatusName(string name)
        {
            return _context.PetStatuses
                .FirstOrDefault(s => s.Name == name)?.Id ?? 0;
        }
    }
}
