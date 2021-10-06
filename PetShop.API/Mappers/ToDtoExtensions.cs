using PetShop.API.Dto.Breed;
using PetShop.API.Dto.Pet;
using PetShop.API.Dto.User;
using PetShop.DAL.EF.Entities;
using PetShop.DAL.EF.Views;
using System.Collections.Generic;
using System.Linq;
using ToolBox.Mappers.Extensions;

namespace PetShop.API.Mappers
{
    public static class ToDtoExtensions
    {
        public static PetDto ToPetDto(this VPet p)
        {
            PetDto result = p.MapTo<PetDto>();
            result.ImageUrl = p.Image != null ? "/Pet/Image/" + p.Id : null;
            return result;
        }

        public static PetDetailsDto ToPetDetailsDto(this VPet p)
        {
            PetDetailsDto result = p.MapTo<PetDetailsDto>();
            result.ImageUrl = p.Image != null ? "/Pet/Image/" + p.Id : null;
            return result;
        }

        public static UserDetailsDto ToUserDetailsDto(this VUser u, IEnumerable<VPet>pets)
        {
            UserDetailsDto result = u.MapTo<UserDetailsDto>();
            result.Pets = pets.Where(p => p.UserId == u.Id)
                .Select(p => p.MapTo<PetDto>());
            return result;
        }
    }
}
