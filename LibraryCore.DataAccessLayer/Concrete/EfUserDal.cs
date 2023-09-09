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
    public class EfUserDal : EfEntityRepositoryBase<User, Context>, IUserDal
    {
        public List<User> GetAllByFK()
        {
            using (var context = new Context())
            {
                return context.Users.Include(u => u.Position).OrderBy(u => u.Id).ToList();
            }
        }

        public List<User> GetAllByStatusWithFK()
        {
            using (var context = new Context())
            {
                return context.Users.Include(u => u.Position).Where(u => u.Status == true).OrderBy(u => u.Id).ToList();
            }
        }

        public User GetByUserName(string userName)
        {
            using (var context = new Context())
            {
                return context.Users.Include(u => u.Position).SingleOrDefault(u => u.UserName == userName);
            }
        }
    }
}
