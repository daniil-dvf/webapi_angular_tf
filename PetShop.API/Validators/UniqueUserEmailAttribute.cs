using PetShop.API.Services;
using PetShop.API.UOW;
using PetShop.API.UOW.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace PetShop.API.Validators
{
    public class UniqueUserEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            UnitOfWork uow = (UnitOfWork)context.GetService(typeof(UnitOfWork));
            if (uow.Get<IUserService>().GetByEmail(value.ToString()))
            {
                return null;
            }
            return new ValidationResult("This Email Already Exists");
        }
    }
}
