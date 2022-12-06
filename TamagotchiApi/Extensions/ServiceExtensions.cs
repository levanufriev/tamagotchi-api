using Contracts;
using Entities;
using Entities.DataTransferObjects;
using FluentValidation;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Repository;
using TamagotchiApi.Validations;

namespace TamagotchiApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"),
                b => b.MigrationsAssembly("TamagotchiApi")));
        }
        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void ConfigureValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<PetForCreationDto>, PetForCreationValidator>();
        }
    }
}
