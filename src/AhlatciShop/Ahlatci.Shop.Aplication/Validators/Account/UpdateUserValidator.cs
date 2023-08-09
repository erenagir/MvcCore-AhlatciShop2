using Ahlatci.Shop.Aplication.Models.RequestModels.Account;
using Ahlatci.Shop.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.Validators.Account
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserVM>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Id)
               .NotEmpty().WithMessage("şehir bilgisi boş bırakılamz");
               
            RuleFor(x => x.CityId)
                .NotEmpty().WithMessage("şehir bilgisi boş bırakılamz")
                .LessThan(82).WithMessage("geçersiz bir il numarası gönderildi ");
            RuleFor(x => x.IdentityNumber)
               .NotEmpty().WithMessage("kimlik bilgisi boş bırakılamz")
               .MaximumLength(11).WithMessage("tc kimlik numarası 11 haneden buyuk olamaz")
               .MinimumLength(11).WithMessage("tc kimlik numarası 11 haneden küçük olamaz");
            RuleFor(x => x.Name)
              .NotEmpty().WithMessage("İsim bilgisi boş bırakılamz")
              .MaximumLength(30).WithMessage("isim bilgis 30 karakterden fazla olamaz");
            RuleFor(x => x.Surname)
             .NotEmpty().WithMessage("Soyisim bilgisi boş bırakılamz")
             .MaximumLength(30).WithMessage("Soyisim bilgis 30 karakterden fazla olamaz");

            RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("telefon bilgisi boş bırakılamz")
            .MaximumLength(13).WithMessage("telefon bilgis 13 karakterden fazla olamaz");


            RuleFor(x => x.Birthdate)
            .NotEmpty().WithMessage("doğum tarihi bilgisi boş bırakılamz");

            RuleFor(x => x.Gender)
            .NotEmpty().WithMessage("cinsiyet bilgisi boş bırakılamz")
            .IsInEnum().WithMessage("cinsiyet bilgisi gecerli değil");

            


            

          

        }
    }
}


