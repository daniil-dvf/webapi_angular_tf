using Microsoft.Extensions.DependencyInjection;
using PetShop.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PetShop.API.UOW.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddApiServices(this IServiceCollection services)
        {
            Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.IsSubclassOf(typeof(BaseService)))
                .Join(
                    Assembly.GetExecutingAssembly().GetTypes()
                    .Where(t => t.IsInterface),
                    t => t.Name,
                    i => i.Name.Substring(1),
                    (t, i) => new { Interface = i, Type = t }
                )
                .ToList().ForEach(j => services.AddScoped(j.Interface, j.Type));
        } 
    }
}
