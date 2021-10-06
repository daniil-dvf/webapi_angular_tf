using PetShop.API.Dto.Breed;
using PetShop.API.Mappers;
using PetShop.API.UOW.Abstractions;
using PetShop.DAL.EF.Context;
using PetShop.DAL.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolBox.Mappers.Extensions;

namespace PetShop.API.Services
{
    public class BreedService : BaseService, IBreedService
    {
        public BreedService(PetShopContext context) : base(context)
        {
        }

        public bool Delete(int id)
        {
            Breed toDelete = _context.Breeds.Find(id);
            if (toDelete == null)
            {
                return false;
            }
            _context.Breeds.Remove(toDelete);
            return true;
        }

        public IEnumerable<BreedDetailsDto> GetAll()
        {
            return _context.VBreeds
                .ToList()
                .Select(b => b.MapTo<BreedDetailsDto>());
        }

        public IEnumerable<BreedDetailsDto> GetAllByAnimal(int animalid)
        {
            return _context.Breeds
                .Where(b => b.AnimalId == animalid)
                .ToList()
                .Select(b => b.MapTo<BreedDetailsDto>());
        }

        public void Insert(BreedInsertDto dto)
        {
            _context.Breeds.Add(dto.MapTo<Breed>());
        }

        public bool GetByNameAndAnimalId(string name, int animalId)
        {
            return _context.Breeds
                .SingleOrDefault(b => b.Name.ToLower() == name.ToLower() && b.AnimalId == animalId) == null;
        }
    }
}
