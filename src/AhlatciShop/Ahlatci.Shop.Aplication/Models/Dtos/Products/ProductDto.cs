using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ahlatci.Shop.Aplication.Models.Dtos.Category;
using Ahlatci.Shop.Domain.Common;

namespace Ahlatci.Shop.Aplication.Models.Dtos.Products
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public int UnitInStock { get; set; }
        public decimal UnitPrice { get; set; }
        public string CategoryName { get; set; }
        //navigation property
        //public CategoryDto Category { get; set; }

    }
}
