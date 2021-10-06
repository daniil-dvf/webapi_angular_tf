using PetShop.API.Dto;
using PetShop.API.Dto.User;

namespace PetShop.API.UOW.Abstractions
{
    public interface IUserService
    {
        bool Delete(int id);
        UserDto Get(int id);
        FilterResultsDto<UserDto> GetAll(int limit, int offset, int? roleId, string emailSearch);
        PayloadDto GetByCredentials(LoginDto login);
        bool GetByEmail(string email);
        UserDetailsDto GetDetails(int id);
        PayloadDto GetPayload(int id);
        void Insert(RegisterDto entity);
        bool Update(UserUpdateDto dto);
    }
}