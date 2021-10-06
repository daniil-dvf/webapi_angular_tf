using PetShop.API.Dto.Animal;
using PetShop.API.UOW.Abstractions;
using PetShop.DAL.EF.Context;
using PetShop.DAL.EF.Entities;
using System.Collections.Generic;
using System.Linq;
using ToolBox.Mappers.Extensions;

namespace PetShop.API.Services
{
    public class AnimalService : BaseService, IAnimalService
    {
        public AnimalService(PetShopContext context) : base(context)
        {
        }


        public IEnumerable<AnimalDto> GetAll()
        {
            return _context.Animals
                .ToList()
                .Select(a => a.MapTo<AnimalDto>());
        }

        public bool GetByName(string name)
        {
            return _context.Animals.SingleOrDefault(a => a.Name.ToLower() == name.ToLower()) == null;
        }

        public void Insert(AnimalInsertDto dto)
        {
            _context.Animals.Add(dto.MapTo<Animal>());
        }

        public bool Delete(int id)
        {
            Animal toDelete = _context.Animals.Find(id);
            if (toDelete == null)
            {
                return false;
            }
            _context.Animals.Remove(toDelete);
            return true;
        }
    }
}
