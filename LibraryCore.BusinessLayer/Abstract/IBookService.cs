using LibraryCore.BusinessLayer.Results;
using LibraryCore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Kitapları yönetmek için metodların tanımlandığı interface
namespace LibraryCore.BusinessLayer.Abstract
{
    public interface IBookService
    {
        IDataResult<List<Book>> GetAllByStatus(); // Statusu true olan kitapları liste olarak döndürür
        IDataResult<List<Book>> GetAllByStatusWithFK(); //Kitapları veritabanından foreignkey (YAzar) ile cagırır.
        IDataResult<List<Book>> GetAllBySearch(string search); //string arayarak kitap ismi getirir.
        IResult Add(Book book);  //yeni kitap ekler.
        IResult Update(Book book);//kitap günceller.
        IDataResult<Book> GetById(int id);//kitabı id paramaetresine göre getirir.
        IDataResult<int> NumberOfBooksByAuthor(int authorId);//authorid sine baglı kitap sayısını getirir.
        IDataResult<int> NumberOfBooksByType(int typeId);//typeıd ye baglı kitap sayısını getirir.

        IResult DeleteById(int bookId); // kitap idsi ile silme işlemi yapar.(statusu false yapar)
    }
}
