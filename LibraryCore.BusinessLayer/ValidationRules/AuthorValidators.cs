using FluentValidation;
using LibraryCore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.BusinessLayer.ValidationRules
{
    public class AuthorValidators : AbstractValidator<Author>
    {                                   //Fluent validationda Abstract sınıfından miras alınıp parametredeki entitiyler için validasyon kuralları.
        public AuthorValidators()
        {
            RuleFor(a => a.FirstName).NotEmpty().WithMessage("Yazar ismi boş bırakılamaz.");
            RuleFor(a => a.FirstName).MinimumLength(2).WithMessage("Yazar ismi en az 2 karakter içermelidir.");

            RuleFor(a => a.LastName).NotEmpty().WithMessage("Yazar soyismi boş bırakılamaz.");
            RuleFor(a => a.LastName).MinimumLength(2).WithMessage("Yazar soyismi en az 2 karakter içermelidir.");
        }
    }
}
