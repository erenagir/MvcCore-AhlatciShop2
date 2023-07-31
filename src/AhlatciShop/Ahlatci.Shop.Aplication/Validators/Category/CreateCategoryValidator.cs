using Ahlatci.Shop.Aplication.Models.RequestModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.Validators.Category
{
    public class CreateCategoryValidator:AbstractValidator<CreateCategoryVM>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x=>x.CategoryName).NotEmpty().WithMessage("Kategori adı boş bırakılamaz .")
                .MaximumLength(100).WithMessage("kategory adı en fazla 100 karakter olabilir");
        }
    }
}
