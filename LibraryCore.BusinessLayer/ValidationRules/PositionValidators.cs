using FluentValidation;
using LibraryCore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.BusinessLayer.ValidationRules
{
    public class PositionValidators : AbstractValidator<Position>
    {
        public PositionValidators()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Pozisyon ismi boş bırakılamaz.");
            RuleFor(p => p.Name).MinimumLength(3).WithMessage("Pozisyon ismi en az 3 karakter içermelidir.");
        }
    }
}
