using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore; // Entity Framework ekleyin
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LibraryCore.DataAccessLayer.Concrete; // Projede kullanılan veri bağlantısı sınıfınıza uygun namespace ekleyin
using RoverCore.ToastNotification; // Notyf (bildirimler) için uygun namespace ekleyin
using LibraryCore.BusinessLayer.Abstract;
using LibraryCore.BusinessLayer.Concrete;
using LibraryCore.DataAccessLayer.Abstract;
using LibraryCore.DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using FluentValidation.AspNetCore;
using Serilog;
using Serilog.Events;
using System.Configuration;
using Microsoft.Extensions.Options;

namespace LibraryCore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<Context>();

            services.AddSingleton<IBookService, BookManager>();
            services.AddSingleton<IBookDal, EfBookDal>();
            services.AddSingleton<IBorrowedBookService, BorrowedBookManager>();
            services.AddSingleton<IBorrowedBookDal, EfBorrowedBookDal>();
            services.AddSingleton<IUserService, UserManager>();
            services.AddSingleton<IUserDal, EfUserDal>();
            services.AddSingleton<IAuthorService, AuthorManager>();
            services.AddSingleton<IAuthorDal, EfAuthorDal>();
            services.AddSingleton<ITypeService, TypeManager>();
            services.AddSingleton<ITypeDal, EfTypeDal>();
            services.AddSingleton<IPositionService, PositionManager>();
            services.AddSingleton<IPositionDal, EfPositionDal>();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddSession();
            services.AddDistributedMemoryCache();

            services.AddNotyf(config =>
            {
                config.DurationInSeconds = 4;
                config.IsDismissable = true;
                config.Position = NotyfPosition.BottomRight;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
            {
                x.LoginPath = "/Auth/Login/";
            });

            services.AddControllersWithViews().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSerilogRequestLogging(options =>
            {

                Log.Logger = new LoggerConfiguration()
            .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day) // Dosya adını ve günlükleme aralığını özelleştirin
            .CreateLogger();


                options.GetLevel = (context, elapsed, ex) =>
                {
                    if (ex != null || context.Response.StatusCode > 499)
                    {
                        return LogEventLevel.Error;
                    }
                    return LogEventLevel.Information;
                };
            });






            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }


            Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // İstenirse Microsoft loglarını sadece uyarı seviyesine ayarlayabilirsiniz
           .WriteTo.Console() // Console hedefini ekliyoruz
           .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
           .CreateLogger();

            app.UseSerilogRequestLogging(); // HTTP isteklerini loglama









            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{Controller=User}/{Action=Books}/{id?}");
            });
        }
    }
}
