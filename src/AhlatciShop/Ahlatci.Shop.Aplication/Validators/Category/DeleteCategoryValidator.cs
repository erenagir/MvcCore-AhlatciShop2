using Ahlatci.Shop.Aplication.Models.RequestModels.Categories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.Validators.Category
{
    public class DeleteCategoryValidator:AbstractValidator<DeleteCategoryVM>
    {

        public DeleteCategoryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("kategory kimlik numarası boş bırakılamaz.")
                .GreaterThan(0).WithMessage("kategory kimlik numarası sıfırdan büyük olmalıdır.");
        }

        
    }
}
