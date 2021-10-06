using PetShop.API.Services;
using PetShop.API.UOW;
using PetShop.API.UOW.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PetShop.API.Validators
{
    public class UniqueAnimalNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            UnitOfWork uow = (UnitOfWork)context.GetService(typeof(UnitOfWork));
            if (uow.Get<IAnimalService>().GetByName(value.ToString()))
            {
                return null;
            }
            return new ValidationResult("This Name Already Exists");
        }
    }
}