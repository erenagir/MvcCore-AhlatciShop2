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
    public class LoginValidator : AbstractValidator<LoginVM>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("şehir bilgisi boş bırakılamz");
            RuleFor(x => x.Password)
               .NotEmpty().WithMessage("şehir bilgisi boş bırakılamz");


        }
    }
}


