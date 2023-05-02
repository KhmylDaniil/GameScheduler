using GameScheduler.BLL.Abstractions;
using GameScheduler.BLL.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameScheduler.DAL
{
    public static class Entry
    {
        public static IServiceCollection AddSqlStorage
            (this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));

            services.AddTransient<IAppDbContext, AppDbContext>();

            return services;
        }

        public static void MigrateDB(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<AppDbContext>()
                ?? throw new ApplicationSystemBaseException("This should never happen, the DbContext couldn't recolve!");

            dbContext.Database.Migrate();
        }
    }
}