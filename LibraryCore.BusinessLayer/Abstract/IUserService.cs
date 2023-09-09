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
        IDataResult<User> CheckUser(User user);
        IResult Add(User user);
        IResult Update(User user);
        IResult CheckUsername(string userName);
        IDataResult<User> GetById(int userId);
        IDataResult<List<User>> GetAllByFK();
        IDataResult<List<User>> GetAllByStatus();
        IDataResult<List<User>> GetAllByStatusWithFK();
        IDataResult<int> NumberOfUsersByPosition(int positionId);
        IDataResult<User> GetByUsername(string userName);
    }
}
