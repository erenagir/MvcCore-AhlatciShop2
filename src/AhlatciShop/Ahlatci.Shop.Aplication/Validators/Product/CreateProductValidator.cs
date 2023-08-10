using Ahlatci.Shop.Aplication.Models.RequestModels.Categories;
using Ahlatci.Shop.Aplication.Models.RequestModels.Cities;
using Ahlatci.Shop.Aplication.Models.RequestModels.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.Validators.Product
{
    public class CreateProductValidator : AbstractValidator<CreatteProductVM>
    {
        public CreateProductValidator()
        {
            
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("ürün adı boş bırakılamaz .")
                .MaximumLength(20).WithMessage("Şehir adı en fazla 25 karakter olabilir");
            RuleFor(x => x.CategoryId)
               .NotEmpty().WithMessage("category id boş bırakılamaz .")
               .GreaterThan(0).WithMessage("categori id 0 dan büyük olmalıdır");
            RuleFor(x => x.Detail)
               .NotEmpty().WithMessage("ürün detay bilgisi boş bırakılamaz .");
                 
            RuleFor(x => x.UnitInStock)
               .NotEmpty().WithMessage("ürün stok bilgisi boş bırakılamaz .")
               .GreaterThan(0).WithMessage("categori id 0 dan büyük olmalıdır");
            RuleFor(x => x.UnitPrice)
              .NotEmpty().WithMessage("ürün fiyat bilgisi boş bırakılamaz .");
              
        }
    }
}
