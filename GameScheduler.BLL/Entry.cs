using GameScheduler.BLL.Abstractions;
using GameScheduler.BLL.Exceptions;
using GameScheduler.BLL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameScheduler.BLL
{
    public static class Entry
    {
        public static void AddBLLServices(this IServiceCollection services, IConfiguration configuration)
        {
            string salt = configuration.Get<HasherOptions>().Salt ??
                            throw new ApplicationSystemBaseException($"Невозможно провести хеширование пароля. Отсутствует параметр salt.");

            services.AddTransient<IPasswordHasher>
                (o => new PasswordHasher(salt));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Entry).Assembly));

            services.AddScoped<IAuthorizationService, AuthorizationService>();

            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        }
    }
}
