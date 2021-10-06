using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetShop.API.UOW;

namespace PetShop.API.Controllers
{
    public abstract class MasterController : ControllerBase
    {
        protected int UserId 
        { 
            get 
            {
                string identifier = User.FindFirst(ClaimTypes.PrimarySid).Value;
                int.TryParse(identifier, out int id);
                return id;
            } 
        }

        protected readonly UnitOfWork Uow;
        public MasterController(UnitOfWork uow)
        {
            Uow = uow;
        }
    }
}
