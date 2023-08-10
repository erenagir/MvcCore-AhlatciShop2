using Ahlatci.Shop.Aplication.Models.Dtos.Account;
using Ahlatci.Shop.Aplication.Models.Dtos.Category;
using Ahlatci.Shop.Aplication.Models.Dtos.Cities;
using Ahlatci.Shop.Aplication.Models.Dtos.Products;
using Ahlatci.Shop.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.AutoMap
{
    public class DomainToDtoModel:Profile
    {
        public DomainToDtoModel()
        {
            // domaini dto ya cevir
            CreateMap<Category, CategoryDto>();
            CreateMap<Customer,CustomerDto>();
            CreateMap<Account, AccounDto>();
            CreateMap<City,CityDto>();
            CreateMap<Product,ProductDto>()
                .ForMember(x=>x.CategoryName,y=>y.MapFrom(e=>e.Category.Name));
               
        }
    }
}
