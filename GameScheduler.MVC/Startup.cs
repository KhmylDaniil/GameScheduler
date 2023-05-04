using Microsoft.AspNetCore.Authentication.Cookies;
using GameScheduler.DAL;
using GameScheduler.BLL.Services;
using GameScheduler.BLL.Abstractions;
using GameScheduler.BLL;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using GameScheduler.MVC.Localization;
using Microsoft.AspNetCore.Mvc.Razor;

namespace GameScheduler.MVC
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvcCore().AddRazorViewEngine();
            services.AddControllersWithViews();

            services.RegisterLocaleProvider();

            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            services.AddSqlStorage(configuration);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => options.LoginPath = "/login");

            services.AddHttpContextAccessor();
            services.AddTransient<IUserContext, UserContext>();

            services.AddBLLServices(configuration);

            services.AddAutoMapper(typeof(Program));
        }

        public static void RegisterLocaleProvider(this IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Localization/Translations");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = SupportedCultures.SupportedCulturesIds
                    .Select(cid => new CultureInfo(cid)).ToList();

                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;

                options.DefaultRequestCulture =
                    new RequestCulture(
                        culture: SupportedCultures.EnUSCulture,
                        uiCulture: SupportedCultures.EnUSCulture
                        );

                options.AddInitialRequestCultureProvider(new CustomRequestCultureProvider(async context =>
                {
                    var cultureFromCookies = context.Request.Cookies["Culture"];
                    
                    if (cultureFromCookies != null && SupportedCultures.SupportedCulturesIds.Contains(cultureFromCookies))
                        return await Task.FromResult(new ProviderCultureResult(cultureFromCookies));

                    return null;
                }));
            });
        }
    }
}
