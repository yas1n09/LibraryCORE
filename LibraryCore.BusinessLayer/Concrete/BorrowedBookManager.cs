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
    public class BorrowedBookManager : IBorrowedBookService
    {
        IBorrowedBookDal _borrowedBook;

        public BorrowedBookManager(IBorrowedBookDal borrowedBook)
        {
            _borrowedBook = borrowedBook;
        }

        public IResult Add(BorrowedBook borrowedBook)//ekle
        {
            _borrowedBook.Add(borrowedBook);
            return new SuccessResult();
        }

        public IResult Delete(BorrowedBook borrowedBook)//sil
        {
            _borrowedBook.Delete(borrowedBook);
            return new SuccessResult();
        }

        public IDataResult<List<BorrowedBook>> GetAllByFK()
        {
            return new SuccessDataResult<List<BorrowedBook>>(_borrowedBook.GetAllByFK());
        }

        public IDataResult<List<BorrowedBook>> GetAllByStatus() //durumu true olan ödünç alınmış kitapları getirir
        {
            return new SuccessDataResult<List<BorrowedBook>>(_borrowedBook.GetAll(b => b.Status == true));
        }

        public IDataResult<List<BorrowedBook>> GetAllByStatus2WithFK()
        {
            return new SuccessDataResult<List<BorrowedBook>>(_borrowedBook.GetAllByStatus2WithFK());
        }

        public IDataResult<List<BorrowedBook>> GetAllByStatusWithFK()
        {
            return new SuccessDataResult<List<BorrowedBook>>(_borrowedBook.GetAllByStatusWithFK());
        }

        public IDataResult<BorrowedBook> GetById(int id)
        {
            return new SuccessDataResult<BorrowedBook>(_borrowedBook.Get(b => b.Id == id));
        }

        public IDataResult<BorrowedBook> GetByUserId(int userId)
        {
            var result = _borrowedBook.GetByFK(userId);
            if (result == null)
            {
                return new ErrorDataResult<BorrowedBook>(result);
            }
            return new SuccessDataResult<BorrowedBook>(result);
        }

        public IResult Update(BorrowedBook borrowedBook)
        {
            _borrowedBook.Update(borrowedBook);
            return new SuccessResult();
        }
    }
}
