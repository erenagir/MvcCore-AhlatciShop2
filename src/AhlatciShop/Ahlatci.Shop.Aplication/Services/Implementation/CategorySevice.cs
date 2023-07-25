using Ahlatci.Shop.Aplication.Models.Dtos;
using Ahlatci.Shop.Aplication.Models.RequestModels;
using Ahlatci.Shop.Aplication.Services.Abstraction;
using Ahlatci.Shop.Domain.Entities;
using Ahlatci.Shop.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.Services.Implementation
{
    public class CategorySevice : ICategoryService
    {

        private readonly AhlatciContext _context;
        public CategorySevice(AhlatciContext context)
        {
            _context = context;
            
        }
        public int CreateCategory(CreateCategoryVM createCategoryVM)
        {
            var categoryEntity = new Category { Name = createCategoryVM.CategoryName };
            _context.Categories.Add(categoryEntity) ;
            _context.SaveChanges() ;
            return categoryEntity.Id ;
        }

        public int DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

        public List<CategoryDto> GetAllCategories()
        {
            throw new NotImplementedException();
        }

        public int UpdateCategory(UpdateCategoryVM updateCategoryVM)
        {
            throw new NotImplementedException();
        }
    }
}
