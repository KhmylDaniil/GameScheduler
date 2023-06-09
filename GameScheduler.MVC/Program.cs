using GameScheduler.DAL;
using GameScheduler.MVC;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureSerilog();

Startup.ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

app.UseRequestLocalization();

Entry.MigrateDB(app.Services);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");


app.Run();
