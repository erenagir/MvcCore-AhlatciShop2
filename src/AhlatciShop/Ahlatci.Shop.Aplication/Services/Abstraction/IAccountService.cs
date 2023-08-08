﻿using Ahlatci.Shop.Aplication.Models.Dtos.Account;
using Ahlatci.Shop.Aplication.Models.RequestModels.Account;
using Ahlatci.Shop.Aplication.Validators.Category;
using Ahlatci.Shop.Aplication.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.Services.Abstraction
{
    public interface IAccountService
    {
         Task<Result<bool>> CreateUser(CreateUserVM createUserVM);
        Task<Result<TokenDto>> Login(LoginVM loginVM);
    }
}
