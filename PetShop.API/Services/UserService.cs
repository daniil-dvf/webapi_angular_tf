using Microsoft.EntityFrameworkCore;
using PetShop.API.Dto;
using PetShop.API.Dto.Pet;
using PetShop.API.Dto.User;
using PetShop.API.Mappers;
using PetShop.API.UOW.Abstractions;
using PetShop.DAL.EF.Context;
using PetShop.DAL.EF.Entities;
using PetShop.DAL.EF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using ToolBox.Mappers.Extensions;

namespace PetShop.API.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(PetShopContext context) : base(context)
        {
        }

        public bool Delete(int id)
        {
            User toDelete = _context.Users.Find(id);
            if (toDelete == null)
            {
                return false;
            }
            // delete all user bookings
            int bookedStatus = _context.PetStatuses.First(x => x.Name == "BOOKED").Id;
            int freeStatus = _context.PetStatuses.First(x => x.Name == "FREE").Id;
            _context.Pets.Where(x => x.PetStatusId == bookedStatus && x.UserId == id).ToList()
                .ForEach(p => p.PetStatusId = freeStatus);
            _context.Users.Remove(toDelete);
            return true;
        }

        public UserDto Get(int id)
        {
            return _context.Users.Find(id)
                ?.MapTo<UserDto>();
        }

        public UserDetailsDto GetDetails(int id)
        {
            return _context.VUsers.Find(id)
                ?.ToUserDetailsDto(_context.VPets);
        }

        public PayloadDto GetByCredentials(LoginDto login)
        {
            return _context.VUsers.FromSqlRaw(
                "dbo.SP_User_Select_By_Credentials @p0, @p1",
                login.Email,
                login.Password
            ).ToList()
            .FirstOrDefault()
            ?.MapTo<PayloadDto>();
        }

        public PayloadDto GetPayload(int id)
        {
            return _context.VUsers.Find(id)
                ?.MapTo<PayloadDto>();
        }

        public FilterResultsDto<UserDto> GetAll(int limit, int offset, int? roleId, string emailSearch)
        {
            int total = _context.VUsers.Count();

            IEnumerable<VUser> users = _context.VUsers
                .Where(u => u.Email.StartsWith(emailSearch) || emailSearch == null)
                .Where(u => u.RoleId == roleId || roleId == null);

            IEnumerable<UserDto> results = users.Skip(offset).Take(limit)
                .ToList()
                .Select(u => u.MapTo<UserDto>());

            int filterCount = users.Count();

            return new FilterResultsDto<UserDto>(results)
            {
                Total = total,
                FilterCount = filterCount,
                Limit = limit,
                Offset = offset
            };
        }

        public void Insert(RegisterDto entity)
        {
            _context.Users.FromSqlRaw(
                "dbo.SP_User_Insert @p0, @p1, @p2, @p3, @p4, @p5",
                entity.Email,
                entity.Password,
                entity.LastName,
                entity.FirstName,
                (object)entity.BirthDate ?? DBNull.Value,
                _context.Roles.FirstOrDefault(r => r.Name == "CUSTOMER")?.Id
            ).ToList();
        }

        public bool Update(UserUpdateDto dto)
        {
            User toUpdate = _context.Users.Find(dto.Id);
            if (toUpdate == null)
                return false;
            dto.MapToInstance(toUpdate);
            return true;
        }

        public bool GetByEmail(string email)
        {
            return _context.Users
                .SingleOrDefault(u => u.Email.ToLower() == email.ToLower()) == null;
        }
    }
}
