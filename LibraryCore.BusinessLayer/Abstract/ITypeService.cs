using LibraryCore.BusinessLayer.Results;
using LibraryCore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = LibraryCore.EntityLayer.Concrete.Type;

namespace LibraryCore.BusinessLayer.Abstract
{
    public interface ITypeService
    {
        IDataResult<List<Type>> GetAllByStatus();
        IResult Add(Type type);
        IResult Update(Type type);
        IDataResult<Type> GetById(int id);
    }
}
