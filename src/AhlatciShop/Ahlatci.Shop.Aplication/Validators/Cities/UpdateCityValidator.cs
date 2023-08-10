using Ahlatci.Shop.Aplication.Models.RequestModels.Categories;
using Ahlatci.Shop.Aplication.Models.RequestModels.Cities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.Validators.Category
{
    public class UpdateCityValidator : AbstractValidator<UpdateCityVM>
    {
        public UpdateCityValidator()
        {
            RuleFor(x=>x.Id)
                .NotEmpty().WithMessage("id  boş bırakılamaz")
                .GreaterThan(0).WithMessage("id 0 dan büyük olmalıdır");
            RuleFor(x=>x.Name)
                .NotEmpty().WithMessage("Şehir adı boş bırakılamaz .")
                .MaximumLength(20).WithMessage("Şehir adı en fazla 20 karakter olabilir");
        }
    }
}
