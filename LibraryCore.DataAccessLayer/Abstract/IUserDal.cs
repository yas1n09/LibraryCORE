using LibraryCore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.DataAccessLayer.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<User> GetAllByFK();
        List<User> GetAllByStatusWithFK();
        User GetByUserName(string userName);
    }
}
