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
        IDataResult<List<Type>> GetAllByStatus();//tüm kitap türlerini Status(tru-false) ile getirme
        IResult Add(Type type);//kitap türü ekleme
        IResult Update(Type type);//tür güncelleme
        IDataResult<Type> GetById(int id);//tür id si ile türleri getirme
    }
}
