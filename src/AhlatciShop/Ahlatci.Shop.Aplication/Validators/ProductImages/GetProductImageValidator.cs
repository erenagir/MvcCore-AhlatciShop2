using Ahlatci.Shop.Aplication.Models.RequestModels.ProductImages;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.Validators.ProductImages
{
    public class GetProductImageValidator:AbstractValidator<GetAllProductImageByProductVM>
    {
        public GetProductImageValidator()
        {

            RuleFor(x => x.ProductId)
                .NotNull().WithMessage("ürün kimlik numarası boş olamaz")
                .GreaterThan(0).WithMessage("ürün kimlik numarası 0 dan büyük olmalıdır");
            
        }
    }
}
