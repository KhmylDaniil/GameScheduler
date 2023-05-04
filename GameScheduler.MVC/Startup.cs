using Microsoft.AspNetCore.Authentication.Cookies;
using GameScheduler.DAL;
using GameScheduler.BLL.Services;
using GameScheduler.BLL.Abstractions;
using GameScheduler.BLL;

namespace GameScheduler.MVC
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvcCore().AddRazorViewEngine();
            services.AddControllersWithViews();

            services.AddSqlStorage(configuration);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => options.LoginPath = "/login");

            services.AddHttpContextAccessor();
            services.AddTransient<IUserContext, UserContext>();

            services.AddBLLServices(configuration);

            services.AddAutoMapper(typeof(Program));
        }
    }
}
