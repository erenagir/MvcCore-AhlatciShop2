using Ahlatci.Shop.Domain.Entities;
using Ahlatci.Shop.Domain.Service.Abstraction;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Domain.Service.Implementation
{
    public class LoggedUserService : ILoggedUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public LoggedUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public int? Id => GetClaim(ClaimTypes.Sid) != null ? int.Parse(GetClaim(ClaimTypes.Sid)) : null;

        public Roles? Role => GetClaim(ClaimTypes.Role) != null ? (Roles)Enum.Parse(typeof(Roles), GetClaim(ClaimTypes.Role)) : null;

        public string UserName => GetClaim(ClaimTypes.Name) != null ? GetClaim(ClaimTypes.Name) : null;

        public string Email => GetClaim(ClaimTypes.Email) != null ? GetClaim(ClaimTypes.Email) : null;


        private string GetClaim(string claimType)
        {
            return _contextAccessor?.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value;
        }
    }
}
