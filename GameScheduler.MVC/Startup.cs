using Microsoft.AspNetCore.Authentication.Cookies;
using GameScheduler.DAL;
using GameScheduler.BLL.Services;
using GameScheduler.BLL.Abstractions;
using GameScheduler.BLL.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace GameScheduler.MVC
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvcCore().AddRazorViewEngine();
            services.AddControllersWithViews();

            services.AddSqlStorage(configuration);

            string salt = configuration.Get<HasherOptions>().Salt ??
                throw new ApplicationSystemBaseException($"Невозможно провести хеширование пароля. Отсутсвтует параметр salt.");

            services.AddTransient<IPasswordHasher>
                (o => new PasswordHasher(salt));


            //services.AddMediatR(typeof(Entry).Assembly);

            //services.AddScoped<IAuthorizationService, AuthorizationService>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => options.LoginPath = "/login");

            services.AddHttpContextAccessor();
            services.AddTransient<IUserContext, UserContext>();

            services.AddScoped<IDateTimeProvider, DateTimeProvider>();

            //services.AddCors(o => o.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            //services.AddSignalR();

            //services.AddAutoMapper(typeof(Program));
        }
    }
}
