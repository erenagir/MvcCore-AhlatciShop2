using Ahlatci.Shop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Domain.Entities
{
    public class Address:BaseEntity
    {
        public int CityId { get; set; }
        public string Text { get; set; }
        public City City { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
