using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Domain.Common
{
    public class AuditableEntity:BaseEntity
    {
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string modifiedBy { get; set; }
    }
}
