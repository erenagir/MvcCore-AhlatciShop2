using Ahlatci.Shop.Aplication.Models.RequestModels.ProductImages;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.Validators.ProductImages
{
    public class DeleteProductImageValidator:AbstractValidator<DeleteProductImageVM>
    {
        public DeleteProductImageValidator()
        {
            RuleFor(x=>x.Id)
                .NotEmpty().WithMessage("silinecek resim Id si boş olamaz ")
                .GreaterThan(0).WithMessage("silinecek ürün ıd si 0 dan büyük olmalıdır");
        }
    }
}
