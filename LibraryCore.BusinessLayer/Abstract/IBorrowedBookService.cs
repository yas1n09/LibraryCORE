using LibraryCore.BusinessLayer.Results;
using LibraryCore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.BusinessLayer.Abstract
{
    public interface IBorrowedBookService
    {
        IDataResult<List<BorrowedBook>> GetAllByFK();
        IDataResult<List<BorrowedBook>> GetAllByStatusWithFK();
        IDataResult<List<BorrowedBook>> GetAllByStatus2WithFK();
        IDataResult<List<BorrowedBook>> GetAllByStatus();
        IResult Add(BorrowedBook borrowedBook);
        IResult Update(BorrowedBook borrowedBook);
        IResult Delete(BorrowedBook borrowedBook);
        IDataResult<BorrowedBook> GetById(int id);
        IDataResult<BorrowedBook> GetByUserId(int userId);
    }
}
