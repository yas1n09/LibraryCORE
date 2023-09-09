using LibraryCore.BusinessLayer.Results;
using LibraryCore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.BusinessLayer.Abstract
{
    public interface IAuthorService
    {
        IDataResult<List<Author>> GetAllByStatus();
        IResult Add(Author author);
        IResult Update(Author author);
        IDataResult<Author> GetById(int id);
    }
}
