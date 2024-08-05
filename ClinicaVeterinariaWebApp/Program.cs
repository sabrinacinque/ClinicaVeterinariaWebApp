using Microsoft.EntityFrameworkCore;
using ClinicaVeterinariaWebApp.Data;
using ClinicaVeterinariaWebApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Aggiungi servizi al contenitore.
builder.Services.AddControllersWithViews();

// Configura il DbContext
builder.Services.AddDbContext<VeterinaryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClinicaVeterinaria")));

var app = builder.Build();

// Configura la pipeline HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
