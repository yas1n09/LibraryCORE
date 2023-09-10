using LibraryCore.BusinessLayer.ValidationRules;
using LibraryCore.BusinessLayer.Abstract;
using LibraryCore.EntityLayer.Concrete;
using LibraryCore.PresentationLayer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RoverCore.ToastNotification.Abstractions;
using System.Security.Claims;
using Serilog;

namespace LibraryCore.PresentationLayer.Controllers
{
    public class AuthController : Controller
    {
        INotyfService _notyf;
        IUserService _userService;
        IPositionService _positionService;

        public AuthController(INotyfService notyf, IUserService userService, IPositionService positionService)
        {
            _notyf = notyf;
            _userService = userService;
            _positionService = positionService;
        }


        #region UserLogin



        public IActionResult Login()
        {
            var model = new UserModel
            {
                User = new User()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            try
            {
                // Kullanıcıyı kontrol et
                var userCheckResult = _userService.CheckUser(user);

                if (!userCheckResult.Success)
                {
                    _notyf.Error("Hatalı Parola veya Kullanıcı Adı", 3);
                    ModelState.AddModelError("", "Hatalı Parola veya Kullanıcı Adı"); // Model durumuna hata ekleyin
                    return View(new UserModel { User = user });
                }

                // Kullanıcıyı bul
                var userDetailsResult = _userService.GetByUsername(user.UserName);
                var userDetails = userDetailsResult.Data;

                // Kullanıcının oturum bilgilerini sakla
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName)
        };
                var userIdentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

                // Oturum verilerini sakla
                HttpContext.Session.SetString("id", userDetails.Id.ToString());
                HttpContext.Session.SetString("name", userDetails.FirstName);
                HttpContext.Session.SetString("lastname", userDetails.LastName);
                HttpContext.Session.SetString("position", userDetails.Position.Name);

                _notyf.Success("Hoşgeldiniz..", 3);

                // Kullanıcı rolüne göre yönlendirme yap
                return RedirectToAction(userDetails.Position.Name == "KULLANICI" ? "Books" : "Books", userDetails.Position.Name == "KULLANICI" ? "User" : "Admin");
            }
            catch (Exception ex)
            {
                // Serilog ile hata loglama
                Log.Error(ex, "Kullanıcı oturum açma işlemi sırasında bir hata oluştu");

                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapabilirsiniz.
            }
        }




        #endregion


        #region SignUp



        public IActionResult UserSignup()
        {
            try
            {
                var model = new UserModel
                {
                    User = new User()
                };
                return View(model);
            }
            catch (Exception ex)
            {
                // Serilog ile hata loglama
                Log.Error(ex, "Kullanıcı kayıt sayfası görüntülenirken bir hata oluştu");

                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapabilirsiniz.
            }
        }

        [HttpPost]
        public IActionResult UserSignup(User user)
        {
            try
            {
                // Kullanıcının durumu ve pozisyonu ayarlanıyor
                user.Status = true;
                user.PositionId = _positionService.GetByName("KULLANICI").Data.Id;

                var validator = new UserValidators(); // FluentValidation kurallarını kullanacak nesne
                var validationResult = validator.Validate(user); // Modeli doğrula

                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage); // Hata mesajlarını ModelState'e ekle
                    }

                    var model = new UserModel
                    {
                        User = user
                    };

                    _notyf.Error("Kayıt Başarısız.");
                    return View(model); // Hata durumunda kayıt sayfasına geri dönün
                }

                // FluentValidation doğrulaması başarılı olduğunda burada kayıt işlemi kodları
                var result = _userService.Add(user);

                if (!result.Success)
                {
                    _notyf.Error("Üye işlemi yapılırken bir hata oluştu.");

                    var model = new UserModel
                    {
                        User = user
                    };

                    return View(model);
                }

                _notyf.Success("Üye işlemi başarıyla tamamlandı.");

                // Başarılı kayıt işlemi sonrası kullanıcıyı giriş sayfasına yönlendirin
                return RedirectToAction("Login", "Auth");
            }
            catch (Exception ex)
            {
                // Serilog ile hata loglama
                Log.Error(ex, "Kullanıcı kayıt işlemi sırasında bir hata oluştu");

                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapabilirsiniz.
            }
        }


        #endregion



        #region LogOut


        public async Task<IActionResult> Logout()
        {
            try
            {
                // Oturum verilerini sil
                Response.Cookies.Delete("id");
                Response.Cookies.Delete("name");
                Response.Cookies.Delete("lastname");
                Response.Cookies.Delete("position");

                // Kullanıcıyı oturumdan çıkart
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                return RedirectToAction("Login", "Auth");
            }
            catch (Exception ex)
            {
                // Serilog ile hata loglama
                Log.Error(ex, "Oturum kapatma işlemi sırasında bir hata oluştu");

                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapabilirsiniz.
            }
        }

        #endregion

    }
}
