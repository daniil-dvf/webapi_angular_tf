using PetShop.API.Dto;
using PetShop.API.Dto.Pet;
using System.Collections.Generic;

namespace PetShop.API.UOW.Abstractions
{
    public interface IPetService
    {
        bool Delete(int id);
        PetDetailsDto Get(int id);
        FilterResultsDto<PetDto> GetAll(int limit, int offset, string refrenceSearch, int? breedId, int? animalId, int? petStatusId);
        IEnumerable<PetDto> GetAllByUserId(int id);
        bool GetByReference(string reference);
        void Insert(PetInsertDto dto);
        bool Update(PetUpdateDto dto);
        bool UpdateStatus(PetUpdateStatusDto dto);
    }
}