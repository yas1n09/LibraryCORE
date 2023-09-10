using LibraryCore.BusinessLayer.Results;
using LibraryCore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.BusinessLayer.Abstract
{
    public interface IBookService
    {
        IDataResult<List<Book>> GetAllByStatus();
        IDataResult<List<Book>> GetAllByStatusWithFK();
        IDataResult<List<Book>> GetAllBySearch(string search);
        IResult Add(Book book);
        IResult Update(Book book);
        IDataResult<Book> GetById(int id);
        IDataResult<int> NumberOfBooksByAuthor(int authorId);
        IDataResult<int> NumberOfBooksByType(int typeId);

        IResult DeleteById(int bookId);
    }
}
