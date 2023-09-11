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
    public class AuthorManager : IAuthorService //veritabanı ile etkileşim kurmak için kullanılan sınıflar (manager) 
    {                                           //author serviceden miras alıp orada tanımladıgımız metodları burada kullanmamıza yarar.
        IAuthorDal _authorDal;

        public AuthorManager(IAuthorDal authorDal)
        {
            _authorDal = authorDal;
        }

        public IResult Add(Author author)
        {
            author.FirstName.ToUpper();//adı ve soyadı büyük harfe cevirip yazarlara ekler.
            author.LastName.ToUpper();
            _authorDal.Add(author);
            return new SuccessResult();
        }

        public IDataResult<List<Author>> GetAllByStatus()//durumu true olan (pasif edilmemiş) tüm yazarları listeye atar.
        {
            return new SuccessDataResult<List<Author>>(_authorDal.GetAll(a => a.Status == true));
        }

        public IDataResult<Author> GetById(int id)//spesifik bir yazarı id si ile getirir.
        {
            return new SuccessDataResult<Author>(_authorDal.Get(a => a.Id == id));
        }

        public IResult Update(Author author)//yazar adı soyadı için güncelleme 
        {
            author.FirstName.ToUpper();
            author.LastName.ToUpper();
            _authorDal.Update(author);
            return new SuccessResult();
        }
    }
}
