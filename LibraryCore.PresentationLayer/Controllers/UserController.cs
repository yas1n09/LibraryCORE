using LibraryCore.BusinessLayer.Abstract;
using LibraryCore.EntityLayer.Concrete;
using LibraryCore.PresentationLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoverCore.ToastNotification.Abstractions;

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


        #region List Books


        public IActionResult Books(string searchString)
        {
            if (!CheckUser())
            {
                return RedirectToAction("Books", "Admin");
            }
            var model = new BookModel();
            if (String.IsNullOrEmpty(searchString))
            {
                model.Books = _bookService.GetAllByStatus().Data;
            }
            else
            {
                model.Books = _bookService.GetAllBySearch(searchString).Data;
            }
            return View(model);
        }
        public IActionResult BorrowBook(int id)
        {
            if (!CheckUser())
            {
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
                Status = true,
                BookId = id,
                UserId = Convert.ToInt32(HttpContext.Session.GetString("id"))
            };
            var book = _bookService.GetById(id).Data;
            book.Status = false;
            _bookService.Update(book);
            _borowwedBookService.Add(borrowedBook);
            _notyf.Success("Kitap ödünç alındı.", 3);
            return RedirectToAction("Books", "User");
        }


        #endregion



        #region Edit Profile


        public IActionResult EditProfil()
        {
            if (!CheckUser())
            {
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
        [HttpPost]
        public IActionResult EditProfil(User user)
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
        public IActionResult ReturnBack(int id)
        {
            if (!CheckUser())
            {
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


        #endregion




        #region CheckUser


        private bool CheckUser()
        {
            if (HttpContext.Session.GetString("position") != "KULLANICI")
            {
                return false;
            }
            return true;
        }



        #endregion
    }
}
