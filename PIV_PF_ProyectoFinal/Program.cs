using PIV_PF_PROYECTOFINAL.Models;
using Microsoft.EntityFrameworkCore;
using PIV_PF_PROYECTOFINAL.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<FARMACIA_PROGRA4Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FARMACIA_PROGRA4Context") ?? throw new InvalidOperationException("Connection string 'PROYECTOFINALContext' not found.")));

var app = builder.Build();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        