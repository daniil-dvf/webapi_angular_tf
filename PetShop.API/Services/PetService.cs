using PetShop.API.Dto;
using PetShop.API.Dto.Pet;
using PetShop.API.Mappers;
using PetShop.API.UOW.Abstractions;
using PetShop.DAL.EF.Context;
using PetShop.DAL.EF.Entities;
using PetShop.DAL.EF.Views;
using System.Collections.Generic;
using System.Linq;
using ToolBox.Mappers.Extensions;

namespace PetShop.API.Services
{
    public class PetService : BaseService, IPetService
    {
        public PetService(PetShopContext context) : base(context)
        {
        }

        public bool Delete(int id)
        {
            Pet toDelete = _context.Pets.Find(id);
            if (toDelete == null)
            {
                return false;
            }
            _context.Pets.Remove(toDelete);
            return true;
        }

        public PetDetailsDto Get(int id)
        {
            return _context.VPets.Find(id)
                ?.ToPetDetailsDto();
        }

        public IEnumerable<PetDto> GetAllByUserId(int id)
        {
            return _context.VPets
                .Where(p => p.UserId == id)
                .ToList()
                .Select(p => p.ToPetDto());
        }

        public void Insert(PetInsertDto dto)
        {
            Pet toInsert = dto.MapTo<Pet>();
            int freeId = _context.PetStatuses.FirstOrDefault(x => x.Name == "FREE")?.Id ?? 0;
            toInsert.PetStatusId = freeId;
            _context.Pets.Add(toInsert);
        }

        public bool Update(PetUpdateDto dto)
        {
            Pet toUpdate = _context.Pets.Find(dto.Id);
            if (toUpdate == null)
                return false;
            dto.MapToInstance(toUpdate);
            return true;
        }

        public bool UpdateStatus(PetUpdateStatusDto dto)
        {
            Pet toUpdate = _context.Pets.Find(dto.Id);
            if (toUpdate == null)
                return false;
            dto.MapToInstance(toUpdate);
            return true;
        }

        public FilterResultsDto<PetDto> GetAll(int limit, int offset, string refrenceSearch, int? breedId, int? animalId, int? petStatusId)
        {
            int total = _context.VPets.Count();

            IEnumerable<VPet> pets = _context.VPets
                .Where(p => p.Reference.Contains(refrenceSearch) || refrenceSearch == null)
                .Where(p => p.BreedId == breedId || breedId == null)
                .Where(p => p.AnimalId == animalId || animalId == null)
                .Where(p => p.PetStatusId == petStatusId || petStatusId == null);

            IEnumerable<PetDto> results = pets.Skip(offset).Take(limit)
                .ToList()
                .Select(p => p.ToPetDto());

            int filterCount = pets.Count();

            return new FilterResultsDto<PetDto>(results)
            {
                Total = total,
                FilterCount = filterCount,
                Limit = limit,
                Offset = offset
            };
        }

        public bool GetByReference(string reference)
        {
            return _context.Pets
                .SingleOrDefault(p => p.Reference.ToLower() == reference.ToLower()) == null;
        }
    }
}
