using LibraryCore.BusinessLayer.Abstract;
using LibraryCore.BusinessLayer.Results;
using LibraryCore.DataAccessLayer.Abstract;
using LibraryCore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.BusinessLayer.Concrete
{
    public class BookManager : IBookService//book serviteki metodlar için miras alınır
    {
        IBookDal _bookDal;

        public BookManager(IBookDal bookDal)
        {
            _bookDal = bookDal;
        }

        public IResult Add(Book book)
        {
            book.Name.ToUpper();//kitap isimleri büyük harfle yeni ktap olarak kaydedilir.
            _bookDal.Add(book);
            return new SuccessResult();
        }

        public IResult DeleteById(int bookId)//kitap idsi ile kitabı siler.
        {


            try
            {
                var bookToDelete = _bookDal.Get(b => b.Id == bookId);
                if (bookToDelete == null)
                {
                    return new ErrorResult("Belirtilen kitap bulunamadı.");
                }

                _bookDal.Delete(bookToDelete);
                return new SuccessResult("Kitap başarıyla silindi.");
            }
            catch (Exception ex)
            {
                // Hata yönetimi burada yapılabilir, loglama vb.
                return new ErrorResult("Kitap silinirken bir hata oluştu.");
            }



        }

        public IDataResult<List<Book>> GetAllBySearch(string search) //aramas kısmına girilin stringi kitap isimlerinde arar blunan kitapları döndürür.
        {
            var result = _bookDal.GetAllByFK(b => b.Status == true && (b.Name.Contains(search.ToUpper()) || b.Author.FirstName.Contains(search.ToUpper()) || b.Author.LastName.Contains(search.ToUpper()) || b.Type.Name.Contains(search.ToUpper())));
            return new SuccessDataResult<List<Book>>(result);
        }

        public IDataResult<List<Book>> GetAllByStatus()//durumu true olan kitapları listeler
        {
            var result = _bookDal.GetAllByFK(b => b.Status == true);
            return new SuccessDataResult<List<Book>>(result);
        }

        public IDataResult<List<Book>> GetAllByStatusWithFK() //Bookdal dan kitapları yazarlarıyla birlikte döndürür
        {
            return new SuccessDataResult<List<Book>>(_bookDal.GetAllByStatusWithFK());
        }

        public IDataResult<Book> GetById(int id)//idsi ile kitap getirir
        {
            return new SuccessDataResult<Book>(_bookDal.Get(b => b.Id == id));
        }

        public IDataResult<int> NumberOfBooksByAuthor(int authorId)//yazarın tüm kitap sayısı
        {
            return new SuccessDataResult<int>(_bookDal.GetAll(b => b.AuthorId == authorId && b.Status == true).Count);
        }

        public IDataResult<int> NumberOfBooksByType(int typeId)//tür Id si ile durumu true ise getirir
        {
            return new SuccessDataResult<int>(_bookDal.GetAll(b => b.TypeId == typeId && b.Status == true).Count);
        }

        public IResult Update(Book book)
        {
            book.Name.ToUpper();
            _bookDal.Update(book);
            return new SuccessResult();
        }

    }
    
}
