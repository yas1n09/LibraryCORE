using LibraryCore.BusinessLayer.Abstract;
using LibraryCore.BusinessLayer.Results;
using LibraryCore.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.BusinessLayer.Concrete
{
    public class TypeManager : ITypeService
    {
        ITypeDal _typeDal;

        public TypeManager(ITypeDal typeDal)
        {
            _typeDal = typeDal;
        }

        public IResult Add(EntityLayer.Concrete.Type type)
        {
            type.Name.ToUpper();
            _typeDal.Add(type);
            return new SuccessResult();
        }

        public IDataResult<List<EntityLayer.Concrete.Type>> GetAllByStatus()
        {
            return new SuccessDataResult<List<EntityLayer.Concrete.Type>>(_typeDal.GetAll(t => t.Status == true).OrderBy(t => t.Id).ToList());
        }

        public IDataResult<EntityLayer.Concrete.Type> GetById(int id)
        {
            return new SuccessDataResult<EntityLayer.Concrete.Type>(_typeDal.Get(t => t.Id == id));
        }

        public IResult Update(EntityLayer.Concrete.Type type)
        {
            type.Name.ToUpper();
            _typeDal.Update(type);
            return new SuccessResult();
        }
    }
}
