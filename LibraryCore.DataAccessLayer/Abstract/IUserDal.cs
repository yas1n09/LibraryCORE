using LibraryCore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.DataAccessLayer.Abstract
{
    public interface IUserDal : IEntityRepository<User>// IEntityRepositorydeki tanımlanan ortak metodlar yeterli olmadıgında entitye göre metodlar her sınıfın kendi dal katmanında tanımlanabilir
    {
        List<User> GetAllByFK();
        List<User> GetAllByStatusWithFK();
        User GetByUserName(string userName);
    }
}
