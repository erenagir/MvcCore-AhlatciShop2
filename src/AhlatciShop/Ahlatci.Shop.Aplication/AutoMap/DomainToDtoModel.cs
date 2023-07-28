using Ahlatci.Shop.Aplication.Models.Dtos;
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
        }
    }
}
