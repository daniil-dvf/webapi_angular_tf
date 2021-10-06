using PetShop.API.Services;
using PetShop.API.UOW;
using PetShop.API.UOW.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.API.Validators
{
    public class UniqueBreedNameAnimalIdAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            UnitOfWork uow = (UnitOfWork)context.GetService(typeof(UnitOfWork));
            var p = context.ObjectInstance.GetType().GetProperty("AnimalId");
            int animalId = (int)p?.GetValue(context.ObjectInstance, null);
            if (uow.Get<IBreedService>().GetByNameAndAnimalId(value.ToString(), animalId))
            {
                return null;
            }
            return new ValidationResult("This Email Already Exists");
        }
    }
}
