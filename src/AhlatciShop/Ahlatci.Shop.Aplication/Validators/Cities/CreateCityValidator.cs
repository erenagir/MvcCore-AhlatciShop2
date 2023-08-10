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
    public class CreateCityValidator:AbstractValidator<CreateCityVM>
    {
        public CreateCityValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Şeir adı boş bırakılamaz .")
                .MaximumLength(20).WithMessage("kategory adı en fazla 20 karakter olabilir");
        }
    }
}
