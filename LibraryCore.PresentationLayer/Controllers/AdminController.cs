using LibraryCore.BusinessLayer.Abstract;
using LibraryCore.EntityLayer.Concrete;
using LibraryCore.PresentationLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoverCore.ToastNotification.Abstractions;
using Serilog;
using Type = LibraryCore.EntityLayer.Concrete.Type;

namespace LibraryCore.PresentationLayer.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {

        INotyfService _notyf;
        IBookService _bookService;
        IBorrowedBookService _borrowedBookService;
        IUserService _userService;
        IAuthorService _authorService;
        ITypeService _typeService;
        IPositionService _positionService;

        public AdminController(INotyfService notyf, IBookService bookService, IBorrowedBookService borrowedBookService, IUserService userService, IAuthorService authorService, ITypeService typeService, IPositionService positionService)
        {
            _notyf = notyf;
            _bookService = bookService;
            _borrowedBookService = borrowedBookService;
            _userService = userService;
            _authorService = authorService;
            _typeService = typeService;
            _positionService = positionService;
        }

        #region Books

        public IActionResult Books()
        {
            if (!CheckUser())
            {
                Log.Error("Kullanıcı yetkilendirme hatası"); 
                return RedirectToAction("Books", "User");
            }
            var model = new BookModel
            {
                Books = _bookService.GetAllByStatusWithFK().Data.OrderBy(book => book.Name).ToList()
            };
            return View(model);
        }
        public IActionResult AddBook()
        {
            if (!CheckUser())
            {
                Log.Error("Kullanıcı yetkilendirme hatası");
                return RedirectToAction("Books", "User");
            }

            // Diğer işlemlerinizi burada devam ettirin.

            List<AuthorWithFullName> authorWithFullName = new List<AuthorWithFullName>();

            foreach (var author in _authorService.GetAllByStatus().Data)
            {
                AuthorWithFullName authorName = new AuthorWithFullName { Id = author.Id, FullName = author.FirstName + " " + author.LastName };
                authorWithFullName.Add(authorName);
            }
            var model = new BookModel
            {
                ImageBook = new ImageBook(),
                Authors = authorWithFullName,
                Types = _typeService.GetAllByStatus().Data
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddBook(ImageBook imageBook)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    List<AuthorWithFullName> authorWithFullName = new List<AuthorWithFullName>();

                    foreach (var author in _authorService.GetAllByStatus().Data)
                    {
                        AuthorWithFullName authorName = new AuthorWithFullName { Id = author.Id, FullName = author.FirstName + " " + author.LastName };
                        authorWithFullName.Add(authorName);
                    }
                    var model = new BookModel
                    {
                        ImageBook = imageBook,
                        Authors = authorWithFullName,
                        Types = _typeService.GetAllByStatus().Data
                    };
                    return View(model);
                }

                Book book = new Book();
                if (imageBook.Image != null)
                {
                    var extension = Path.GetExtension(imageBook.Image.FileName);
                    var newImageName = Guid.NewGuid() + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    imageBook.Image.CopyTo(stream);
                    book.Image = newImageName;
                }
                else
                {
                    book.Image = "DefaultBook.jpg";
                }

                book.Name = imageBook.Name;
                book.AuthorId = imageBook.AuthorId;
                book.TypeId = imageBook.TypeId;
                book.Status = true;

                _bookService.Add(book);
                _notyf.Success("Kitap başarıyla eklendi", 3);
                return RedirectToAction("Books", "Admin");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Kitap eklenirken bir hata oluştu");

                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapılabilir.
            }
        }
        public IActionResult UpdateBook(int id)
        {
            try
            {
                if (!CheckUser())
                {
                    Log.Error("Kullanıcı yetkilendirme hatası");
                    return RedirectToAction("Books", "User");
                }

                List<AuthorWithFullName> authorWithFullName = new List<AuthorWithFullName>();

                foreach (var author in _authorService.GetAllByStatus().Data)
                {
                    AuthorWithFullName authorName = new AuthorWithFullName { Id = author.Id, FullName = author.FirstName + " " + author.LastName };
                    authorWithFullName.Add(authorName);
                }

                var book = _bookService.GetById(id).Data;
                var imageBook = new ImageBook { Id = book.Id, AuthorId = book.AuthorId, Name = book.Name, TypeId = book.TypeId };
                var model = new BookModel
                {
                    ImageBook = imageBook,
                    Authors = authorWithFullName,
                    Types = _typeService.GetAllByStatus().Data
                };

                return View(model);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Kitap güncellenirken bir hata oluştu");

                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapılabilir.
            }
        }
        [HttpPost]
        public IActionResult UpdateBook(ImageBook imageBook)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    List<AuthorWithFullName> authorWithFullName = new List<AuthorWithFullName>();

                    foreach (var author in _authorService.GetAllByStatus().Data)
                    {
                        AuthorWithFullName authorName = new AuthorWithFullName { Id = author.Id, FullName = author.FirstName + " " + author.LastName };
                        authorWithFullName.Add(authorName);
                    }
                    var model = new BookModel
                    {
                        ImageBook = imageBook,
                        Authors = authorWithFullName,
                        Types = _typeService.GetAllByStatus().Data
                    };
                    return View(model);
                }
                Book book = new Book();
                if (imageBook.Image != null)
                {
                    var extension = Path.GetExtension(imageBook.Image.FileName);
                    var newImageName = Guid.NewGuid() + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    imageBook.Image.CopyTo(stream);
                    book.Image = newImageName;
                }
                else
                {
                    book.Image = "DefaultBook.jpg";
                }
                book.Id = imageBook.Id;
                book.Name = imageBook.Name;
                book.AuthorId = imageBook.AuthorId;
                book.TypeId = imageBook.TypeId;
                book.Status = true;
                _bookService.Update(book);
                _notyf.Warning("Kitap başarıyla güncellendi", 3);
                return RedirectToAction("Books", "Admin");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Kitap güncellenirken bir hata oluştu");

                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapılabilir.
            }
        }

        public IActionResult DeleteBook(int id)
        {
            if (!CheckUser())
            {
                return RedirectToAction("Books", "User");
            }

            // Kitap silme işlemi burada gerçekleştirilir
            try
            {
                // Silme işlemi için _bookService veya ilgili servis sınıfınızı kullanabilirsiniz
                var result = _bookService.DeleteById(id);

                if (result.Success)
                {
                    _notyf.Success("Kitap başarıyla silindi", 3);
                }
                else
                {
                    _notyf.Error("Kitap silinirken bir hata oluştu", 3);
                }
            }
            catch (Exception ex)
            {
                // Hata durumunu loglamak için ex değişkenini kullanabilirsiniz.
                _notyf.Error("Kitap silinirken bir hata oluştu", 3);
                // Hata mesajını loglama işlemi burada yapılabilir
            }

            return RedirectToAction("Books", "Admin");
        }

        #endregion



        #region BorrowedBooks


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
                    BorrowedBooks = _borrowedBookService.GetAllByStatusWithFK().Data
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

        public IActionResult OldBorrowedBooks()
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
                    BorrowedBooks = _borrowedBookService.GetAllByStatus2WithFK().Data
                };
                return View(model);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Eski ödünç alınan kitaplar listelenirken bir hata oluştu");

                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapılabilir.
            }
        }
        public IActionResult RevokeBorrowedBook(int id)
        {
            try
            {
                if (!CheckUser())
                {
                    Log.Error("Kullanıcı yetkilendirme hatası");
                    return RedirectToAction("Books", "User");
                }
                var borrowBook = _borrowedBookService.GetById(id).Data;
                borrowBook.Status = false;
                borrowBook.ReturnDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                var book = _bookService.GetById(borrowBook.BookId).Data;
                book.Status = true;
                _bookService.Update(book);
                _borrowedBookService.Update(borrowBook);
                _notyf.Success("Kitap geri alındı.", 3);
                return RedirectToAction("BorrowedBooks", "Admin");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Kitap geri alınırken bir hata oluştu");

                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapılabilir.
            }
        }

        public IActionResult DeleteBorrowedBook(int id)
        {
            try
            {
                if (!CheckUser())
                {
                    Log.Error("Kullanıcı yetkilendirme hatası");
                    return RedirectToAction("Books", "User");
                }
                var borrowBook = _borrowedBookService.GetById(id).Data;
                _borrowedBookService.Delete(borrowBook);
                _notyf.Error("Kayıt silindi.", 3);
                return RedirectToAction("OldBorrowedBooks", "Admin");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ödünç alınan kitap silinirken bir hata oluştu");

                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapılabilir.
            }
        }




        #endregion



        #region Authors

        public IActionResult Authors()
        {
            try
            {
                if (!CheckUser())
                {
                    Log.Error("Kullanıcı yetkilendirme hatası");
                    return RedirectToAction("Books", "User");
                }
                var authors = _authorService.GetAllByStatus().Data;
                var authorsWithBooks = new List<AuthorWithBook>();
                foreach (var author in authors)
                {
                    authorsWithBooks.Add(new AuthorWithBook
                    {
                        Id = author.Id,
                        FirstName = author.FirstName,
                        LastName = author.LastName,
                        NumberOfBook = _bookService.NumberOfBooksByAuthor(author.Id).Data
                    });
                }
                var model = new AuthorModel
                {
                    AuthorWithBooks = authorsWithBooks
                };
                return View(model);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Yazarlar sayfası görüntülenirken bir hata oluştu");

                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapılabilir.
            }
        }
        public IActionResult AddAuthor()
        {
            try
            {
                if (!CheckUser())
                {
                    Log.Error("Kullanıcı yetkilendirme hatası");
                    return RedirectToAction("Books", "User");
                }
                var model = new AuthorModel
                {
                    Author = new Author()
                };
                return View(model);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Yazar ekleme sayfası görüntülenirken bir hata oluştu");

                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapılabilir.
            }
        }

        [HttpPost]
        public IActionResult AddAuthor(Author author)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var model = new AuthorModel
                    {
                        Author = author
                    };
                    return View(model);
                }
                author.Status = true;
                _authorService.Add(author);
                _notyf.Success("Yeni yazar başarıyla eklendi.", 3);
                return RedirectToAction("Authors"); // HTTP GET ile yazar listesine yönlendirin
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Yazar ekleme işlemi sırasında bir hata oluştu");

                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapılabilir.
            }
        }

        public IActionResult UpdateAuthor(int id)
        {
            try
            {
                if (!CheckUser())
                {
                    Log.Error("Kullanıcı yetkilendirme hatası");
                    return RedirectToAction("Books", "User");
                }
                var model = new AuthorModel
                {
                    Author = _authorService.GetById(id).Data
                };
                return View(model);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Yazar güncelleme sayfası görüntülenirken bir hata oluştu");

                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapılabilir.
            }
        }

        [HttpPost]
        public IActionResult UpdateAuthor(Author author)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var model = new AuthorModel
                    {
                        Author = author
                    };
                    return View(model);
                }
                author.Status = true;
                _authorService.Update(author);
                _notyf.Warning("Yazar başarıyla güncellendi.", 3);
                return RedirectToAction("Authors"); // HTTP GET ile yazar listesine yönlendirin
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Yazar güncelleme işlemi sırasında bir hata oluştu");

                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapılabilir.
            }
        }

        public IActionResult DeleteAuthor(int id)
        {
            if (!CheckUser())
            {
                return RedirectToAction("Books", "User");
            }
            var author = _authorService.GetById(id).Data;
            author.Status = false;
            _authorService.Update(author);
            _notyf.Error("Yazar başarıyla silindi.", 3);
            return RedirectToAction("Authors"); // HTTP GET ile yazar listesine yönlendirin
        }

        #endregion



        #region Types


        public IActionResult Types()
        {
            try
            {
                if (!CheckUser())
                {
                    Log.Error("Kullanıcı yetkilendirme hatası");
                    return RedirectToAction("Books", "User");
                }
                var types = _typeService.GetAllByStatus().Data;
                var typeWithBooks = new List<TypeWithBook>();
                foreach (var type in types)
                {
                    typeWithBooks.Add(new TypeWithBook
                    {
                        Id = type.Id,
                        Name = type.Name,
                        NumberOfBook = _bookService.NumberOfBooksByType(type.Id).Data
                    });
                }
                var model = new TypeModel
                {
                    TypeWithBooks = typeWithBooks
                };
                return View(model);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Türler sayfası görüntülenirken bir hata oluştu");

                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapılabilir.
            }
        }

        public IActionResult AddType()
        {
            try
            {
                if (!CheckUser())
                {
                    Log.Error("Kullanıcı yetkilendirme hatası");
                    return RedirectToAction("Books", "User");
                }
                var model = new TypeModel
                {
                    Type = new EntityLayer.Concrete.Type()
                };
                return View(model);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Tür ekleme sayfası görüntülenirken bir hata oluştu");

                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapılabilir.
            }
        }

        [HttpPost]
        public IActionResult AddType(Type type)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var model = new TypeModel
                    {
                        Type = type
                    };
                    return View(model);
                }
                type.Status = true;
                _typeService.Add(type);
                _notyf.Success("Yeni tür başarıyla eklendi.", 3);
                return RedirectToAction("Types", "Admin");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Tür ekleme işlemi sırasında bir hata oluştu");
                ModelState.AddModelError("", "Tür eklenirken bir hata oluştu. Lütfen tekrar deneyin.");
                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapılabilir.
            }
        }

        public IActionResult UpdateType(int id)
        {
            try
            {
                if (!CheckUser())
                {
                    Log.Error("Kullanıcı yetkilendirme hatası");
                    return RedirectToAction("Books", "User");
                }
                var model = new TypeModel
                {
                    Type = _typeService.GetById(id).Data
                };
                return View(model);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Tür güncelleme sayfası görüntülenirken bir hata oluştu");

                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapılabilir.
            }
        }

        [HttpPost]
        public IActionResult UpdateType(Type type)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var model = new TypeModel
                    {
                        Type = type
                    };
                    return View(model);
                }
                type.Status = true;
                _typeService.Update(type);
                _notyf.Success("Tür başarıyla güncellendi.", 3);
                return RedirectToAction("Types", "Admin");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Tür güncelleme işlemi sırasında bir hata oluştu");

                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapılabilir.
            }
        }

        public IActionResult DeleteType(int id)
        {
            try
            {
                if (!CheckUser())
                {
                    Log.Error("Kullanıcı yetkilendirme hatası");
                    return RedirectToAction("Books", "User");
                }
                var type = _typeService.GetById(id).Data;
                type.Status = false;
                _typeService.Update(type);
                _notyf.Error("Tür başarıyla silindi.", 3);
                return RedirectToAction("Types", "Admin");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Tür silme işlemi sırasında bir hata oluştu");

                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapılabilir.
            }
        }



        #endregion



        #region Positions

        public IActionResult Positions()
        {
            try
            {
                if (!CheckUser())
                {
                    Log.Error("Kullanıcı yetkilendirme hatası");
                    return RedirectToAction("Books", "User");
                }
                //if (!CheckStaff())
                //{
                //    Log.Error("Personel yetkilendirme hatası");
                //    return RedirectToAction("Books", "Admin");
                //}
                var positions = _positionService.GetAllByStatus().Data;
                var positionWithUsers = new List<PositionWithUser>();
                foreach (var position in positions)
                {
                    positionWithUsers.Add(new PositionWithUser
                    {
                        Id = position.Id,
                        Name = position.Name,
                        NumberOfUsers = _userService.NumberOfUsersByPosition(position.Id).Data
                    });
                }
                var model = new PositionModel
                {
                    PositionWithUsers = positionWithUsers
                };
                return View(model);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Pozisyonlar sayfası görüntülenirken bir hata oluştu");

                // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

                return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapılabilir.
            }
        }

        //public IActionResult AddPosition()
        //{
        //    try
        //    {
        //        if (!CheckUser())
        //        {
        //            Log.Error("Kullanıcı yetkilendirme hatası");
        //            return RedirectToAction("Books", "User");
        //        }
        //        var model = new PositionModel
        //        {
        //            Position = new Position()
        //        };
        //        return View(model);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex, "Pozisyon ekleme sayfası görüntülenirken bir hata oluştu");

        //        // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

        //        return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapılabilir.
        //    }
        //}

        //[HttpPost]
        //public IActionResult AddPosition(Position position)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            var model = new PositionModel
        //            {
        //                Position = position
        //            };
        //            return View(model);
        //        }
        //        position.Status = true;
        //        _positionService.Add(position);
        //        _notyf.Success("Yeni pozisyon başarıyla eklendi.", 3);
        //        return RedirectToAction("Positions", "Admin");
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex, "Pozisyon ekleme işlemi sırasında bir hata oluştu");

        //        // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

        //        return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapılabilir.
        //    }
        //}

        //public IActionResult UpdatePosition(int id)
        //{
        //    try
        //    {
        //        if (!CheckUser())
        //        {
        //            Log.Error("Kullanıcı yetkilendirme hatası");
        //            return RedirectToAction("Books", "User");
        //        }
        //        var model = new PositionModel
        //        {
        //            Position = _positionService.GetById(id).Data
        //        };
        //        return View(model);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex, "Pozisyon güncelleme sayfası görüntülenirken bir hata oluştu");

        //        // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

        //        return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapılabilir.
        //    }
        //}

        //[HttpPost]
        //public IActionResult UpdatePosition(Position position)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            var model = new PositionModel
        //            {
        //                Position = position
        //            };
        //            return View(model);
        //        }
        //        position.Status = true;
        //        _positionService.Update(position);
        //        _notyf.Warning("Pozisyon başarıyla güncellendi.", 3);
        //        return RedirectToAction("Positions", "Admin");
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex, "Pozisyon güncelleme işlemi sırasında bir hata oluştu");

        //        // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

        //        return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapılabilir.
        //    }
        //}

        //public IActionResult DeletePosition(int id)
        //{
        //    try
        //    {
        //        if (!CheckUser())
        //        {
        //            Log.Error("Kullanıcı yetkilendirme hatası");
        //            return RedirectToAction("Books", "User");
        //        }
        //        var position = _positionService.GetById(id).Data;
        //        position.Status = false;
        //        _positionService.Update(position);
        //        _notyf.Error("Pozisyon başarıyla silindi.", 3);
        //        return RedirectToAction("Positions", "Admin");
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex, "Pozisyon silme işlemi sırasında bir hata oluştu");

        //        // Hata mesajını kullanıcıya göstermek veya diğer işlemleri burada gerçekleştirebilirsiniz.

        //        return View("Error"); // Hata sayfasına yönlendirme veya başka bir işlem yapılabilir.
        //    }
        //}


        #endregion




        #region Statistics


        public IActionResult Statistic()
        {
            if (!CheckUser())
            {
                return RedirectToAction("Books", "User");
            }
            //if (!CheckStaff())
            //{
            //    return RedirectToAction("Books", "Admin");
            //}
            var model = new StatisticModel
            {
                NumberOfAuthors = _authorService.GetAllByStatus().Data.Count,
                NumberOfBooks = _bookService.GetAllByStatus().Data.Count,
                NumberOfBorrowedBooks = _borrowedBookService.GetAllByStatus().Data.Count,
                NumberOfPositions = _positionService.GetAllByStatus().Data.Count,
                NumberOfTypes = _typeService.GetAllByStatus().Data.Count,
                NumberOfUsers = _userService.GetAllByStatus().Data.Count
            };
            return View(model);
        }
        #endregion


        #region Users
        public IActionResult Users()
        {
            if (!CheckUser())
            {
                return RedirectToAction("Books", "User");
            }
            //if (!CheckStaff())
            //{
            //    return RedirectToAction("Books", "Admin");
            //}
            var model = new UserModel
            {
                Users = _userService.GetAllByStatusWithFK().Data
            };
            return View(model);
        }
        public IActionResult AddUser()
        {
            if (!CheckUser())
            {
                return RedirectToAction("Books", "User");
            }
            var model = new UserModel
            {
                User = new User(),
                Positions = _positionService.GetAllByStatus().Data
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult AddUser(User user)
        {
            if (!ModelState.IsValid)
            {
                var model = new UserModel
                {
                    User = user,
                    Positions = _positionService.GetAllByStatus().Data
                };
                return View(model);
            }
            user.Status = true;
            _userService.Add(user);
            _notyf.Success("Yeni kullanıcı başarıyla eklendi.", 3);
            return RedirectToAction("Users", "Admin");
        }
        public IActionResult UpdateUser(int id)
        {
            if (!CheckUser())
            {
                return RedirectToAction("Books", "User");
            }
            var model = new UserModel
            {
                User = _userService.GetById(id).Data,
                Positions = _positionService.GetAllByStatus().Data
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult UpdateUser(User user)
        {
            user.Status = true;
            _userService.Update(user);
            _notyf.Warning("Kullanıcı güncellendi.", 3);
            return RedirectToAction("Users", "Admin");
        }
        public IActionResult DeleteUser(int id)
        {
            if (!CheckUser())
            {
                return RedirectToAction("Books", "User");
            }
            var user = _userService.GetById(id).Data;
            user.Status = false;
            _userService.Update(user);
            _notyf.Error("Kullanıcı başarıyla silindi.", 3);
            return RedirectToAction("Users", "Admin");
        }


        #endregion



        #region Check User-Staff
        private bool CheckUser()
        {
            if (HttpContext.Session.GetString("position") == "KULLANICI")
            {
                return false;
            }
            return true;
        }
        // Check Staff
        private bool CheckStaff()
        {
            if (HttpContext.Session.GetString("position") == "PERSONEL")
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
