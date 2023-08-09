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
    public class ReisterValidator : AbstractValidator<ReisterVM>
    {
        public ReisterValidator()
        {
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

            RuleFor(x => x.Email)
             .NotEmpty().WithMessage("Email bilgisi boş bırakılamz")
             .MaximumLength(150).WithMessage("Email bilgis 150 karakterden fazla olamaz")
             .EmailAddress().WithMessage("gecerli bir eposta adresi girmelisiniz");

            RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("telefon bilgisi boş bırakılamz")
            .MaximumLength(13).WithMessage("telefon bilgis 13 karakterden fazla olamaz");


            RuleFor(x => x.Birthdate)
            .NotEmpty().WithMessage("doğum tarihi bilgisi boş bırakılamz");

            RuleFor(x => x.Gender)
            .NotEmpty().WithMessage("cinsiyet bilgisi boş bırakılamz")
            .IsInEnum().WithMessage("cinsiyet bilgisi gecerli değil");

            RuleFor(x => x.Username)
            .NotEmpty().WithMessage("kullanıcı adı bilgisi boş bırakılamz")
            .MaximumLength(10).WithMessage("kullanıcı adı bilgisi 10 karakterden fazla olamaz");


            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("şifre  bilgisi boş bırakılamz")
            .MaximumLength(10).WithMessage("şifre bilgisi 10 karakterden fazla olamaz");

            RuleFor(x => x.PasswordAgain)
            .Matches(x => x.Password).WithMessage("parola tekrarı parola ile aynı olmalır");

          

        }
    }
}


