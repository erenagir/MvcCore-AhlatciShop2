using Ahlatci.Shop.Aplication.Models.RequestModels.Account;
using Ahlatci.Shop.Aplication.Models.RequestModels.Categories;
using Ahlatci.Shop.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.AutoMap
{
    public class ViewModelToDomain : Profile
    {
        public ViewModelToDomain()
        {
            //kaynak ve hedef arasındaki proporty isimleri eşlezmezse manuel tanımlama yapmak gerekir

            CreateMap<CreateCategoryVM, Category>()
                .ForMember(x => x.Name, y => y.MapFrom(e => e.CategoryName));
            CreateMap<UpdateCategoryVM, Category>()
                .ForMember(x => x.Name, y => y.MapFrom(e => e.CategoryName));
            // account 
            CreateMap<ReisterVM, Customer>();
                
            CreateMap<ReisterVM, Account>()
                .ForMember(x=>x.Role,y=>y.MapFrom(e=>Roles.User));
            
            CreateMap<UpdateUserVM, Customer>();
                

        }
    }
}
