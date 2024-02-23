using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MultiDataSourceExample.DataBase;
using MultiDataSourceExample.Repository;
using System;

namespace MultiDataSourceExample.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            builder.Services.AddDbContext<AContext>(options =>
                options.UseMySql("Server=127.0.0.1;Database=testa;User Id=root;Password=root;", new MySqlServerVersion(new Version()))
                .AddInterceptors(new LoggingInterceptor())
                );
            builder.Services.AddDbContext<BContext>(options =>
                options.UseMySql("Server=127.0.0.1;Database=testb;User Id=root;Password=root;", new MySqlServerVersion(new Version()))
                 .AddInterceptors(new LoggingInterceptor())
                );
        
            // ×¢²áUnitOfWork
           
            builder.Services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
     
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
        }
    }
}
