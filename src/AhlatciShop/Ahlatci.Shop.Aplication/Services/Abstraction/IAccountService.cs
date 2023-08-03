using Ahlatci.Shop.Aplication.Models.RequestModels.Account;
using Ahlatci.Shop.Aplication.Validators.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.Services.Abstraction
{
    public interface IAccountService
    {
         Task<bool> CreateUser(CreateUserVM createUserVM);

    }
}
