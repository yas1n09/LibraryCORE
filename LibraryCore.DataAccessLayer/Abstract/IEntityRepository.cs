using LibraryCore.EntityLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.DataAccessLayer.Abstract
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null);//Bu metod, veritabanından T türünde bir nesnenin tüm örneklerini döndürür. filter parametresi, döndürülecek nesneleri filtrelemek için kullanılır.
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);//diger dal interface leri bu sınıftan miras alır buradaki tüm metodları kullanabilirler.
        void Update(T entity);
        void Delete(T entity);
    }
}
