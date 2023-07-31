using Ahlatci.Shop.Aplication.Models.RequestModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.Validators.Category
{
    public class UpdateCategoryvalidator : AbstractValidator<UpdateCategoryVM>
    {
        public UpdateCategoryvalidator()
        {
            RuleFor(x => x.Id).NotEmpty()
                .WithMessage("kategory kimlik numarası boş bırakılamaz.")
               .GreaterThan(0)
               .WithMessage("kategory kimlik numarası sıfırdan büyük olmalıdır.");
            RuleFor(x => x.CategoryName).NotEmpty()
                .WithMessage("Kategori adı boş bırakılamaz .")
               .MaximumLength(100)
               .WithMessage("kategory adı en fazla 100 karakter olabilir");
        }
    }
}
