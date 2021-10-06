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
    public class UniquePetReferenceAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            UnitOfWork uow = (UnitOfWork)context.GetService(typeof(UnitOfWork));
            if (uow.Get<IPetService>().GetByReference(value.ToString()))
            {
                return null;
            }
            return new ValidationResult("This Reference Already Exists");
        }
    }
}
