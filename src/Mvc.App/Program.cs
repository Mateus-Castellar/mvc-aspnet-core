using Microsoft.EntityFrameworkCore;
using Mvc.App.Configuration;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.AddEnviromentConfiguration();

builder.Services.AddIdentityConfiguration(connectionString);

builder.Services.AddAutoMapperConfiguration();

builder.Services.AddMvcConfiguration(connectionString);

builder.Services.ResolveDependences();

var app = builder.Build();

app.UseMvcConfiguration();

app.UseGlobalizationCulture();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
