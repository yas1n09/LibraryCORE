using LibraryCore.DataAccessLayer.Abstract;
using LibraryCore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Type = LibraryCore.EntityLayer.Concrete.Type;

namespace LibraryCore.DataAccessLayer.Concrete
{
    public class EfTypeDal : EfEntityRepositoryBase<Type, Context>, ITypeDal
    {
        
    }
}
