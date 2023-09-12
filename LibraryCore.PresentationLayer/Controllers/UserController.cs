using FluentValidation;
using FluentValidation.Results;
using LibraryCore.BusinessLayer.Results;
using LibraryCore.BusinessLayer.Abstract;
using LibraryCore.EntityLayer.Concrete;
using LibraryCore.PresentationLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoverCore.ToastNotification.Abstractions;
using Serilog;
using System.Drawing;

namespace LibraryCore.PresentationLayer.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        INotyfService _notyf;
        IBookService _bookService;
        IUserService _userService;
        IBorrowedBookService _borowwedBookService;

        public UserController(INotyfService notyf, IBookService bookService, IUserService userService, IBorrowedBookService borowwedBookService)
        {
            _notyf = notyf;
            _bookService = bookService;
            _userService = userService;
            _borowwedBookService = borowwedBookService;
        }

        // Bu kod bloğu, kitapların listelendiği ve ödünç alındığı bir web uygulaması sayfasının kontrol işlemlerini içerir.
        // "Books" metodu, kitapları listeler ve arama işlevi ekler. Kullanıcı yetkilendirmesi yapılır ve kitaplar sıralanır.
        // "BorrowedBooks" metodu, ödünç alınmış kitapları listeler ve kullanıcı yetkilendirmesi yapar.
        // "BorrowBook" metodu, bir kitabı ödünç alırken kullanılır ve ödünç alma işlemi gerçekleştirilir.


        #region List Books


        public IActionResult Books(string searchString)
        {
            try
            {
                if (!CheckUser())
                {
                    Log.Error("Kullanıcı yetkilendirme hatası");
                    return RedirectToAction("Books", "Admin");
                }
                var model = new BookModel();
                if (String.IsNullOrEmpty(searchString))
                {

                    model.Books = _bookService.GetAllByStatus().Data.OrderBy(book => book.Name).ToList();
                    
                }
                else
                {
                    model.Books = _bookService.GetAllBySearch(searchString).Data.OrderBy(book => book.Name).ToList();
                }
                return View(model);
            }
            catch (Exception ex)
            {
                // Serilog ile hata loglama
                Log.Error(ex, "Kitap listesi görüntülenirken bir hata oluştu");

                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapabilirsiniz.
            }
        }


        public IActionResult BorrowedBooks()
        {
            try
            {
                if (!CheckUser())
                {
                    Log.Error("Kullanıcı yetkilendirme hatası");
                    return RedirectToAction("Books", "User");
                }
                var model = new BorrowedBookModel
                {
                    BorrowedBooks = _borowwedBookService.GetAllByStatusWithFK().Data
                };
                return View(model);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ödünç alınan kitaplar listelenirken bir hata oluştu");

                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapılabilir.
            }
        }






        public IActionResult BorrowBook(int id,DateTime returnDate)
        {
            try
            {
                if (!CheckUser())
                {
                    Log.Error("Kullanıcı yetkilendirme hatası");
                    return RedirectToAction("Books", "Admin");
                }

                if (_borowwedBookService.GetByUserId(Convert.ToInt32(HttpContext.Session.GetString("id"))).Data != null)
                {
                    _notyf.Error("Mevcut ödünç alınmış kitabınız bulunmaktadır.", 3);
                    return RedirectToAction("Books", "User");
                }

                var borrowedBook = new BorrowedBook
                {
                    BorrowDate = DateTime.Parse(DateTime.Now.ToShortDateString()),
                    ReturnDate = DateTime.Parse(DateTime.Now.AddDays(15).ToShortDateString()),

                    //ReturnDate = returnDate,
                    Status = true,
                    BookId = id,
                    UserId = Convert.ToInt32(HttpContext.Session.GetString("id"))
                };

                var book = _bookService.GetById(id).Data;
                book.Status = false;
                _bookService.Update(book);
                _borowwedBookService.Add(borrowedBook);
                TempData["SuccessMessage"] = "Kitap ödünç alındı.";
                _notyf.Success("Kitap ödünç alındı.", 3);
                return RedirectToAction("Books", "User");
            }
            catch (Exception ex)
            {
                // Serilog ile hata loglama
                Log.Error(ex, "Kitap ödünç alma işlemi sırasında bir hata oluştu");

                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapabilirsiniz.
            }
        }






        #endregion



        #region Edit Profile
        //profil düzenleme kısmı
        // Position u kontrol eder koşula uymazsa admin controllerdaki books kitap listeleme IActionResultuna atar
        //kullanıcının ödüç aldığı kitap varsa modele döndürür. View tarafında modelden ödünç alınan kitap gösterilir.
        public IActionResult EditProfil()
        {
            try
            {
                if (!CheckUser())
                {
                    Log.Error("Kullanıcı yetkilendirme hatası");
                    return RedirectToAction("Books", "Admin");
                }

                BorrowedBook borrowedBook = null;
                if (_borowwedBookService.GetByUserId(Convert.ToInt32(HttpContext.Session.GetString("id"))).Success)
                {
                    borrowedBook = _borowwedBookService.GetByUserId(Convert.ToInt32(HttpContext.Session.GetString("id"))).Data;
                }

                var model = new UserModel
                {
                    User = _userService.GetById(Convert.ToInt32(HttpContext.Session.GetString("id"))).Data,
                    BorrowedBook = borrowedBook
                };

                return View(model);
            }
            catch (Exception ex)
            {
                // Serilog ile hata loglama
                Log.Error(ex, "Profil düzenleme sayfası görüntülenirken bir hata oluştu");

                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapabilirsiniz.
            }
        }

        [HttpPost]
        public IActionResult EditProfil(User user) //kullanıcı bilgilerini güncelleyebilir.
        {
            try
            {
                BorrowedBook borrowedBook = null;
                if (_borowwedBookService.GetByUserId(Convert.ToInt32(HttpContext.Session.GetString("id"))).Success)
                {
                    borrowedBook = _borowwedBookService.GetByUserId(Convert.ToInt32(HttpContext.Session.GetString("id"))).Data;
                }

                if (ModelState.ErrorCount == 1)
                {
                    user.Status = true;
                    HttpContext.Session.SetString("name", user.FirstName);
                    HttpContext.Session.SetString("lastname", user.LastName);
                    _userService.Update(user);
                    _notyf.Success("Bilgileriniz başarıyla güncellendi.", 3);
                    return RedirectToAction("EditProfil", "User");
                }

                var model = new UserModel
                {
                    User = user,
                    BorrowedBook = borrowedBook
                };

                _notyf.Error("Profil Güncellenemedi.", 3);
                return View(model);
            }
            catch (Exception ex)
            {
                // Serilog ile hata loglama
                Log.Error(ex, "Profil düzenleme işlemi sırasında bir hata oluştu");

                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapabilirsiniz.
            }
        }


        //Bu kod bloğu, bir kullanıcının bir kitabı iade etme işlemini yönetir.İade işlemi gerçekleştirildiğinde, ilgili kitabın durumu güncellenir ve kullanıcının ödünç aldığı kitap kaydı güncellenir.Kullanıcı bilgilendirilir ve kullanıcının profil sayfasına yönlendirilir.Hata yönetimi ve kullanıcıya bilgilendirme işlemleri tüm metodlarda ortaktır ve yorum satırlarıyla belirtilmiştir.

        //alınan kitabı iade etme kısmı

        public IActionResult ReturnBack(int id)
        {
            try
            {
                if (!CheckUser())
                {
                    Log.Error("Kullanıcı yetkilendirme hatası");
                    return RedirectToAction("Books", "Admin");
                }

                var result = _borowwedBookService.GetByUserId(Convert.ToInt32(HttpContext.Session.GetString("id"))).Data;
                result.Status = false;
                result.ReturnDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                var book = _bookService.GetById(id).Data;
                book.Status = true;
                _bookService.Update(book);
                _borowwedBookService.Update(result);
                _notyf.Warning("Kitap iade edildi.", 3);
                return RedirectToAction("EditProfil", "User");
            }
            catch (Exception ex)
            {
                // Serilog ile hata loglama
                Log.Error(ex, "Kitap iade işlemi sırasında bir hata oluştu");

                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapabilirsiniz.
            }
        }


        #endregion



        //kullanıcı kontrolu
        #region CheckUser


        private bool CheckUser()
        {
            try
            {
                if (HttpContext.Session.GetString("position") != "KULLANICI")
                {
                    Log.Error("Kullanıcı yetkilendirme hatası");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                // Serilog ile hata loglama
                Log.Error(ex, "Kullanıcı yetkilendirme hatası");

                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return false; // Hata durumunu döndürün veya başka bir işlem yapabilirsiniz.
            }
        }



        #endregion
    }
}
