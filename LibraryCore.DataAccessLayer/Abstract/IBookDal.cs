using LibraryCore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.DataAccessLayer.Abstract
{
    public interface IBookDal : IEntityRepository<Book>
    {
        List<Book> GetAllByFK(Expression<Func<Book, bool>> filter = null);
        List<Book> GetAllByStatusWithFK(Expression<Func<Book, bool>> filter = null);
    }
}
