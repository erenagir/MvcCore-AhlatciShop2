using Ahlatci.Shop.Aplication.Models.RequestModels.ProductImages;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.Validators.ProductImages
{
    public class CreateProductImageValidator:AbstractValidator<CreateProductImageVM>
    {
        public CreateProductImageValidator()
        {
            var allowedContentTypes = new string[] { "image/jpg", "image/jpeg", "image/png", "image/gif", "image/tiff" };
            RuleFor(x=>x.ProductId)
                .NotEmpty().WithMessage("Ürün kimlik bilgisi boş olamaz");

            RuleFor(x => x.UploadedImage)
                .NotNull().WithMessage("resim dosyası seçilmelidir")
                .Must(x => x.Length < (1 * 1024 * 1024)).WithMessage("dosya boyutu 1MB den büyük olmamalıdır")
                .Must(x => allowedContentTypes.Contains(x.ContentType)).WithMessage("sadece resim dosyası seçilebilir");


        }
    }
}
