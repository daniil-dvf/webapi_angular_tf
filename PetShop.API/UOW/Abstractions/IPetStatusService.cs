using PetShop.API.Dto.Status;
using System.Collections.Generic;

namespace PetShop.API.UOW.Abstractions
{
    public interface IPetStatusService
    {
        IEnumerable<PetStatusDto> GetAll();
        int GetByStatusName(string name);
    }
}