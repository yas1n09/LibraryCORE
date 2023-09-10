
using LibraryCore;
using LibraryCore.BusinessLayer.Abstract;
using LibraryCore.BusinessLayer.Concrete;
using LibraryCore.DataAccessLayer.Abstract;
using LibraryCore.DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RoverCore.ToastNotification;
using Serilog;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();


        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .UseSerilog((hostingContext, loggerConfiguration) =>
            {
                loggerConfiguration
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day) // Log dosyas�n�n yolu ve g�nl�k dosyas� olarak ayarlad���n�z k�sm� buraya ekleyin.
                    .ReadFrom.Configuration(hostingContext.Configuration);
            });

}
