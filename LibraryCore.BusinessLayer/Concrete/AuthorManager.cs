using LibraryCore.BusinessLayer.Abstract;
using LibraryCore.BusinessLayer.Results;
using LibraryCore.DataAccessLayer.Abstract;
using LibraryCore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.BusinessLayer.Concrete
{
    public class AuthorManager : IAuthorService
    {
        IAuthorDal _authorDal;

        public AuthorManager(IAuthorDal authorDal)
        {
            _authorDal = authorDal;
        }

        public IResult Add(Author author)
        {
            author.FirstName.ToUpper();
            author.LastName.ToUpper();
            _authorDal.Add(author);
            return new SuccessResult();
        }

        public IDataResult<List<Author>> GetAllByStatus()
        {
            return new SuccessDataResult<List<Author>>(_authorDal.GetAll(a => a.Status == true));
        }

        public IDataResult<Author> GetById(int id)
        {
            return new SuccessDataResult<Author>(_authorDal.Get(a => a.Id == id));
        }

        public IResult Update(Author author)
        {
            author.FirstName.ToUpper();
            author.LastName.ToUpper();
            _authorDal.Update(author);
            return new SuccessResult();
        }
    }
}
