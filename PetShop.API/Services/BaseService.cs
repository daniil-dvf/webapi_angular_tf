using PetShop.DAL.EF.Context;

namespace PetShop.API.Services
{
    public abstract class BaseService
    {
        protected readonly PetShopContext _context;

        protected BaseService(PetShopContext context)
        {
            _context = context;
        }
    }
}
