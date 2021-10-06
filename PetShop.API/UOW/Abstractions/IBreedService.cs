using PetShop.API.Dto.Breed;
using System.Collections.Generic;

namespace PetShop.API.UOW.Abstractions
{
    public interface IBreedService
    {
        bool Delete(int id);
        IEnumerable<BreedDetailsDto> GetAll();
        IEnumerable<BreedDetailsDto> GetAllByAnimal(int animalid);
        bool GetByNameAndAnimalId(string name, int animalId);
        void Insert(BreedInsertDto dto);
    }
}