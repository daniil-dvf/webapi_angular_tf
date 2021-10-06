using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PetShop.API.UOW;
using PetShop.DAL.EF.Context;
using PetShop.Services.Abstractions.Jwt;
using ToolBox.Security.Jwt;

namespace PetShop.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PetShop API", Version = "v1" });

                OpenApiSecurityScheme securitySchema = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer", securitySchema);
                var securityRequirement = new OpenApiSecurityRequirement();
                securityRequirement.Add(securitySchema, new[] { "Bearer" });
                c.AddSecurityRequirement(securityRequirement);
            });

            services.AddCors(options => options.AddPolicy("PetShopApi", builder => {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            }));

            services.AddScoped(sp => new UnitOfWork(Configuration));

            services.AddScoped<IJwtService>(provider => {
                IConfiguration tokenConfig = Configuration.GetSection("JwtConfig");
                return new JwtService(new JwtConfig
                {
                    Issuer = tokenConfig.GetValue<string>("Issuer"),
                    Audience = tokenConfig.GetValue<string>("Audience"),
                    Signature = tokenConfig.GetValue<string>("Signature")
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("PetShopApi");

            app.UseSwagger();

            app.UseSwaggerUI(config => config.SwaggerEndpoint("v1/swagger.json", "PetShop API V1"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
