using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.BusinessLayer.Results
{
    public interface IDataResult<T> : IResult// IREsulttan miras alır T parametresinde "Data" veri nesnesine ulaşmak için kullanılır.
    {                                           //
        T Data { get; }
    }
}
