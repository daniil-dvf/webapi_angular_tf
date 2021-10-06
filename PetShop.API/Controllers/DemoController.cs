using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        [HttpGet("{nb}")]
        public int Get(int nb)
        {
            System.Threading.Thread.Sleep(new Random().Next(1000, 5000));
            return nb * nb;
        }
    }
}
