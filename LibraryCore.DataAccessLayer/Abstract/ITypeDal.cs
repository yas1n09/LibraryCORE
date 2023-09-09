using LibraryCore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = LibraryCore.EntityLayer.Concrete.Type;

namespace LibraryCore.DataAccessLayer.Abstract
{
    public interface ITypeDal : IEntityRepository<Type>
    {
    }
}
