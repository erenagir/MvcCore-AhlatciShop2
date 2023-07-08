using Ahlatci.Shop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Domain.Entities
{
    public class Category:AuditableEntity
    {
        public string Name { get; set; }
        //navigation property
        public ICollection<Product> Products { get; set; }
    }
}
