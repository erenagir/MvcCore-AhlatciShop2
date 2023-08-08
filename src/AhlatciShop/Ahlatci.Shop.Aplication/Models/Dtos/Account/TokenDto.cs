using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.Models.Dtos.Account
{
    public class TokenDto
    {
        public string Token { get; set; }
        public DateTime Expiredate { get; set; }
        
    }
}
