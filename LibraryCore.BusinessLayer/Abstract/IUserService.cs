using LibraryCore.BusinessLayer.Results;
using LibraryCore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.BusinessLayer.Abstract
{
    public interface IUserService
    {
        IDataResult<User> CheckUser(User user); //kullanıcı mı -admin mi oldugunu kontrol etmek için metod
        IResult Add(User user);//kullanıcı eklemek için metod
        IResult Update(User user);//kullanıcı bilgilerini güncellemek için metod.
        IResult CheckUsername(string userName);//benzersiz kullanıcı adı için kullanılan metod
        IDataResult<User> GetById(int userId);// kullanıcıları id ile getirme
        IDataResult<List<User>> GetAllByFK();//foreign key positionid ile listeleme
        IDataResult<List<User>> GetAllByStatus();//durumu true olan kullanıcıları listeleme
        IDataResult<List<User>> GetAllByStatusWithFK();//position id ye göre durumları true olanları listeleme
        IDataResult<int> NumberOfUsersByPosition(int positionId);//her pozisyonda kaç kullanıcı oldugunu gösteren metod.
        IDataResult<User> GetByUsername(string userName);//usernam parametresine göre kullanıcı getirme
    }
}
