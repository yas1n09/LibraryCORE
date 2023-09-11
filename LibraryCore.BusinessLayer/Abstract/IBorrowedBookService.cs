using LibraryCore.BusinessLayer.Results;
using LibraryCore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.BusinessLayer.Abstract
{
    public interface IBorrowedBookService
    {
        IDataResult<List<BorrowedBook>> GetAllByFK();//foreignkeye bağlı tüm ödünç alınmış kitapların listesi.kullanıcının ödünç aldıgını getirmek için.
        IDataResult<List<BorrowedBook>> GetAllByStatusWithFK();//Statusu false olan ve hala kullanıcıda olan kitapları listeler.
        IDataResult<List<BorrowedBook>> GetAllByStatus2WithFK();
        IDataResult<List<BorrowedBook>> GetAllByStatus();//status lerine göre kitapları getirir.
        IResult Add(BorrowedBook borrowedBook); //kitap ödünç almak için.
        IResult Update(BorrowedBook borrowedBook);//ödünç kitabın durumunu güncellemek için.
        IResult Delete(BorrowedBook borrowedBook);//daha önceden ödünç alınmışve geri getirilmiş kitabın , ödünç alınıp getirldiği kaydı siler.
        IDataResult<BorrowedBook> GetById(int id);//ödünç kitapları id ile getirir.
        IDataResult<BorrowedBook> GetByUserId(int userId);//ödünç alan kullanıcın id parametresiyle kitabı getirir.
    }
}
