using Ahlatci.Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Domain.Service.Abstraction
{
    public interface ILoggedUserService
    {
        int? Id { get; }
        Roles? Role { get; }
        string UserName { get; }
        string Email { get; }
    }
}
