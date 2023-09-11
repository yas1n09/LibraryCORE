using LibraryCore.BusinessLayer.Results;
using LibraryCore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Yazarları yönetmek için metodların tanımlandığı interface

namespace LibraryCore.BusinessLayer.Abstract
{
    public interface IAuthorService
    {
        IDataResult<List<Author>> GetAllByStatus(); //Status u true olan yazarları liste olarak döndürür
        IResult Add(Author author);  //Id , İsim, Soyisim ile yeni bir yazar ekler.
        IResult Update(Author author); //Yazarın isim soyisimini günceller
        IDataResult<Author> GetById(int id); //yazarları Id sırasına göre getirir.
    }
}
