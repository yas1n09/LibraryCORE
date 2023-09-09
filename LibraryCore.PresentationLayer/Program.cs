
using LibraryCore.BusinessLayer.Abstract;
using LibraryCore.BusinessLayer.Concrete;
using LibraryCore.DataAccessLayer.Abstract;
using LibraryCore.DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RoverCore.ToastNotification;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();






builder.Services.AddDbContext<Context>(); //Db Contexti Programa tanýttýk.//

//constructor kullandýgýmýz sýnýf ve interfaceleri burada belirtiyoruz.//

builder.Services.AddScoped<IAuthorService,AuthorManager>();
builder.Services.AddScoped<IAuthorDal,EfAuthorDal>();

builder.Services.AddScoped<IBookService, BookManager>();
builder.Services.AddScoped<IBookDal, EfBookDal>();

builder.Services.AddScoped<IBorrowedBookService, BorrowedBookManager>();
builder.Services.AddScoped<IBorrowedBookDal, EfBorrowedBookDal>();

builder.Services.AddScoped<IPositionService, PositionManager>();
builder.Services.AddScoped<IPositionDal, EfPositionDal>();

builder.Services.AddScoped<ITypeService, TypeManager>();
builder.Services.AddScoped<ITypeDal, EfTypeDal>();

builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IUserDal, EfUserDal>();


builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();


builder.Services.AddSession();


builder.Services.AddDistributedMemoryCache();


builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 4;
    config.IsDismissable = true;
    config.Position = NotyfPosition.BottomRight;
});





builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.LoginPath = "/Auth/Login/";
});










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

app.UseAuthentication();

app.UseAuthorization();


app.UseSession();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(name: "default", pattern: "{Controller=User}/{Action=Books}/{id?}");
});



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
