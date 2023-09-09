using FluentValidation;
using LibraryCore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.BusinessLayer.ValidationRules
{
    public class BookValidators : AbstractValidator<Book>
    {
        public BookValidators()
        {
            RuleFor(b => b.Name).NotEmpty().WithMessage("Kitap ismi boş bırakılamaz.");
            RuleFor(b => b.Name).MinimumLength(2).WithMessage("Kitap ismi en az 2 karakter içermeli.");

            
        }
    }
}
