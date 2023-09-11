using LibraryCore.BusinessLayer.Results;
using LibraryCore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.BusinessLayer.Abstract
{
    public interface IPositionService
    {
        IDataResult<List<Position>> GetAllByStatus();//Posizyonlar (Kullanıcı-admin) Statuslerine göre getirir.
        IResult Add(Position position);//pozisyon eklemek için kullanılıyordu ancak 2 pozisyon yeterli oldugu için kullanılmıyor.
        IResult Update(Position position);//pozisyonu güncellemek için (admin - kullanıcı) yapmak için
        IDataResult<Position> GetById(int id);//pozisyon idleri ile kullanıcıları-adminleri getirme.
        IDataResult<Position> GetByName(string positionName);//pozisyon adı ile getirme.
    }
}
