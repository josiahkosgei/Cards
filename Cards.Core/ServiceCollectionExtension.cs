using Cards.Core.Services;
using Cards.Core.Services.Interfaces;
using Cards.Data.IRepository;
using Cards.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System.Reflection;

namespace Cards.Core
{
    public static class ServiceCollectionExtension
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Inject Our services to the container
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<IUserService, UserService>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICardRepository, CardRepository>();
            return services;
        }
    }
}
