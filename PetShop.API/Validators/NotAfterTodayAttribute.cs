using System;
using System.ComponentModel.DataAnnotations;

namespace PetShop.API.Validators
{
    public class NotAfterTodayAttribute : ValidationAttribute
    {
        public NotAfterTodayAttribute()
        {
            ErrorMessage = "This Date Should be Before Today";
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;
            return (DateTime)value < DateTime.Now;
        }
    }
}