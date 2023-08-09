using Ahlatci.Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.Models.Dtos.Account
{
    public class AccounDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        
        public Roles Role { get; set; }
        public CustomerDto Customer { get; set; }
    }
}
