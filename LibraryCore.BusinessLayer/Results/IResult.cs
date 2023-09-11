using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.BusinessLayer.Results
{
    public interface IResult//bolean olarak işlemin başarılı olup olmadığını ve diğer sınıflara miras veren interface
    {                       // bir bool değeri ve bir mesaj alır.bool değeri, işlemin başarılı olup olmadığını gösterir. Mesaj, işlemle ilgili herhangi bir hata mesajıdır.
        bool Success { get; }
        string Message { get; }
    }
}
