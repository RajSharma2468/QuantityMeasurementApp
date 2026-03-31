using Microsoft.EntityFrameworkCore;
using QuantityMeasurementRepositoryLayer.Context;
using QuantityMeasurementRepositoryLayer.Interfaces;
using QuantityMeasurementRepositoryLayer.Repositories;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementBusinessLayer.Services;

namespace QuantityMeasurementAPILayer.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            
           
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));
            
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // UC18 Repositories
            services.AddScoped<IAuthRepository, AuthRepository>();
          //  services.AddScoped<IEncryptionRepository, EncryptionRepository>();
            
            // UC17 Repository
            services.AddScoped<IMeasurementRepository, MeasurementRepository>();
            
            return services;
        }
        
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // UC17 Business Service
            services.AddScoped<IMeasurementService, MeasurementService>();
            
            return services;
        }
    }
}