using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.API.Validators
{
    public class UniqueAttribute : ValidationAttribute
    {
        public UniqueAttribute(Type controllerType, string methodName, string errorMessage, params string[] properties)
            : base(errorMessage)
        {
            ControllerType = controllerType;
            MethodName = methodName;
            Properties = properties;
        }

        public Type ControllerType { get; set; }

        public string MethodName { get; set; }

        public string[] Properties { get; set; }



        protected override ValidationResult IsValid(object value, ValidationContext context)
        {

            // TODO Not working !!!!
            object controller = context.GetService(ControllerType);
            var method = ControllerType.GetMethod(MethodName);

            object[] values = Properties.ToList().Select(p => context.ObjectType.GetProperty(p).GetValue(context.ObjectInstance)).ToArray();

            if((bool)method.Invoke(controller, values))
            {
                return null;
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}
