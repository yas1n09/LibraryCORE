using LibraryCore.DataAccessLayer.Abstract;
using LibraryCore.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.DataAccessLayer.Concrete
{
    public class EfBookDal : EfEntityRepositoryBase<Book, Context>, IBookDal
    {
        public List<Book> GetAllByFK(Expression<Func<Book, bool>> filter = null)
        {
            using (var context = new Context())
            {
                return filter == null
                    ? context.Books.Include(b => b.Author).Include(b => b.Type).OrderBy(b => b.Id).ToList()
                    : context.Books.Include(b => b.Author).Include(b => b.Type).OrderBy(b => b.Id).Where(filter).ToList();
            }
        }

        public List<Book> GetAllByStatusWithFK(Expression<Func<Book, bool>> filter = null)
        {
            using (var context = new Context())
            {
                return filter == null
                    ? context.Books.Include(b => b.Author).Include(b => b.Type).Where(b => b.Status == true).OrderBy(b => b.Id).ToList()
                    : context.Books.Include(b => b.Author).Include(b => b.Type).Where(b => b.Status == true).OrderBy(b => b.Id).Where(filter).ToList();
            }
        }
    }
}
