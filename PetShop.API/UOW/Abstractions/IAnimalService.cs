using PetShop.API.Dto.Animal;
using System.Collections.Generic;

namespace PetShop.API.UOW.Abstractions
{
    public interface IAnimalService
    {
        bool Delete(int id);
        IEnumerable<AnimalDto> GetAll();
        bool GetByName(string name);
        void Insert(AnimalInsertDto dto);
    }
}