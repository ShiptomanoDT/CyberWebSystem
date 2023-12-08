using CyberWebSystem.Context;
using Microsoft.EntityFrameworkCore;

namespace CyberWebSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add Connection String
            //Esta linea de codigo es para que se pueda leer el archivo appsettings.json
            builder.Services.AddDbContext<MiContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("CadenaConexion"))
            );

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
            // Inicio de la pagina web, este metodo se encarga de llamar a la pagina que se mostrara al principio del programa
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");

            app.Run();
        }
    }
}