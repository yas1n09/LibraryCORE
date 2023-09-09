using LibraryCore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.DataAccessLayer.Abstract
{
    public interface IAuthorDal : IEntityRepository<Author>
    {
    }
}
