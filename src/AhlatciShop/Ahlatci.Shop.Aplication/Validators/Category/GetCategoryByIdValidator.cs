using Ahlatci.Shop.Aplication.Models.RequestModels;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.Validators.Category
{
    public class GetCategoryByIdValidator:AbstractValidator<GetCategoryByIdVM>
    {
        public GetCategoryByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("kategory kimlik numarası boş bırakılamaz.")
                .GreaterThan(0).WithMessage("kategory kimlik numarası sıfırdan büyük olmalıdır.");
        }

        internal ValidationResult Validate(IList<object> arguments)
        {
            throw new NotImplementedException();
        }
    }
}
