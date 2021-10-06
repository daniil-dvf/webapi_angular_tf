using System;
using Microsoft.Extensions.DependencyInjection;
using PetShop.DAL.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PetShop.API.Services;
using PetShop.API.UOW.Extensions;

namespace PetShop.API.UOW
{
    public class UnitOfWork : IDisposable
    {
        private readonly ServiceProvider _provider;

        public UnitOfWork(IConfiguration configuration)
        {
            ServiceCollection services = new ServiceCollection();
            services.AddDbContext<PetShopContext>(options => options.UseSqlServer(configuration.GetConnectionString("PetShop")));
            //services.AddScoped<UserService>();
            //services.AddScoped<AnimalService>();
            //services.AddScoped<PetService>();
            //services.AddScoped<PetStatusService>();
            //services.AddScoped<BreedService>();
            //services.AddScoped<RoleService>();
            //services.AddScoped<PetStatusService>();
            services.AddApiServices();
            _provider = services.BuildServiceProvider();
        }

        public TRepository Get<TRepository>()
        {
            return _provider.GetService<TRepository>();
        }

        public int Save()
        {
            return _provider.GetService<PetShopContext>().SaveChanges();
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _provider.Dispose();
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
        }
    }
}