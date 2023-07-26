using Ahlatci.Shop.Aplication.Models.Dtos;
using Ahlatci.Shop.Aplication.Models.RequestModels;
using Ahlatci.Shop.Aplication.Services.Abstraction;
using Ahlatci.Shop.Domain.Entities;
using Ahlatci.Shop.Persistence.Context;
using Microsoft.EntityFrameworkCore;
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
        public async Task<List<CategoryDto>> GetAllCategories()
        {
            var categories = await _context.Categories.Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
            return categories;
        }

        public async Task<CategoryDto> GetCategoryById(int id)
        {
            var categoryExists = await _context.Categories.AnyAsync(x => x.Id == id);
            if (!categoryExists)
            {
                throw new Exception($"{id} numaralı kadegöri bulunamadı");
            }
            var categoryEntity = await _context.Categories.FindAsync(id);
            var categoryDto = new CategoryDto
            {
                Id = id,
                Name = categoryEntity.Name
            };
            return categoryDto;

        }
        public async Task<int> CreateCategory(CreateCategoryVM createCategoryVM)
        {
            var categoryEntity = new Category { Name = createCategoryVM.CategoryName };
            await _context.Categories.AddAsync(categoryEntity);
            await _context.SaveChangesAsync();
            return categoryEntity.Id;
        }

        public async Task<int> DeleteCategory(int id)
        {
            var categoryExists = await _context.Categories.AnyAsync(x => x.Id == id);
            if (!categoryExists)
            {
                throw new Exception($"{id} numaralı kadegöri bulunamadı");
            }
            var existsCategory = await _context.Categories.FindAsync(id);
            existsCategory.IsDeleted = true;
            _context.Categories.Update(existsCategory);
            await _context.SaveChangesAsync();
            return existsCategory.Id;
        }
        public async Task<int> UpdateCategory(UpdateCategoryVM updateCategoryVM)
        {
            var categoryExists = await _context.Categories.AnyAsync(x => x.Id == updateCategoryVM.Id);
            if (!categoryExists)
            {
                throw new Exception($"{updateCategoryVM.Id} numaralı kadegöri bulunamadı");
            }

            var existsCategory = await _context.Categories.FindAsync(updateCategoryVM.Id);
            existsCategory.Name = updateCategoryVM.CategoryName;
            _context.Categories.Update(existsCategory);
            await _context.SaveChangesAsync();
            return existsCategory.Id;
        }


    }
}
