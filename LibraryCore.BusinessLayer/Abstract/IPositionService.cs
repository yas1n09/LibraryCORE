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
        IDataResult<List<Position>> GetAllByStatus();
        IResult Add(Position position);
        IResult Update(Position position);
        IDataResult<Position> GetById(int id);
        IDataResult<Position> GetByName(string positionName);
    }
}
