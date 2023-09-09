using LibraryCore.DataAccessLayer.Abstract;
using LibraryCore.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.DataAccessLayer.Concrete
{
    public class EfBorrowedBookDal : EfEntityRepositoryBase<BorrowedBook, Context>, IBorrowedBookDal
    {
        public List<BorrowedBook> GetAllByFK()
        {
            using (var context = new Context())
            {
                return context.BorrowedBooks.Include(b => b.Book).Include(b => b.User).OrderBy(b => b.Id).ToList();
            }
        }

        public List<BorrowedBook> GetAllByStatus2WithFK()
        {
            using (var context = new Context())
            {
                return context.BorrowedBooks.Include(b => b.Book).Include(b => b.User).Where(b => b.Status == false).OrderBy(b => b.Id).ToList();
            }
        }

        public List<BorrowedBook> GetAllByStatusWithFK()
        {
            using (var context = new Context())
            {
                return context.BorrowedBooks.Include(b => b.Book).Include(b => b.User).Where(b => b.Status == true).OrderBy(b => b.Id).ToList();
            }
        }

        public BorrowedBook GetByFK(int userId)
        {
            using (var context = new Context())
            {
                return context.BorrowedBooks.Include(b => b.Book).Include(b => b.User).SingleOrDefault(b => b.UserId == userId && b.Status == true);
            }
        }
    }
}
