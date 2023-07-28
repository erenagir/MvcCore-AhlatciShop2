using Ahlatci.Shop.Aplication.Models.Dtos;
using Ahlatci.Shop.Aplication.Models.RequestModels;
using Ahlatci.Shop.Aplication.Services.Abstraction;
using Ahlatci.Shop.Domain.Entities;
using Ahlatci.Shop.Persistence.Context;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IMapper _mapper;
        public CategorySevice(AhlatciContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CategoryDto>> GetAllCategories()
        {
            //1.yol
            //var categories = await _context.Categories.Select(x => new CategoryDto
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //}).ToListAsync();
            //return categories;

            //2.yol
            //var categories=await _context.Categories.ToListAsync();
            ////_mapper.Map<T1,T2> t1 türündeki kaynak objeyi t2 türüne cevirir
            //var categoryDtos= _mapper.Map<List<CategoryDto>>(categories);
            //return categoryDtos;

            var categoryDtos =await _context.Categories
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return categoryDtos;
        }

        public async Task<CategoryDto> GetCategoryById(int id)
        {
            var categoryExists = await _context.Categories.AnyAsync(x => x.Id == id);
            if (!categoryExists)
            {
                throw new Exception($"{id} numaralı kadegöri bulunamadı");
            }
            //var categoryEntity = await _context.Categories.FindAsync(id);
            //var categoryDto = new CategoryDto
            //{
            //    Id = id,
            //    Name = categoryEntity.Name
            //};
            //return categoryDto;
            var categoryEntity=await _context.Categories
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x=> x.Id==id);
            return categoryEntity;

        }
        public async Task<int> CreateCategory(CreateCategoryVM createCategoryVM)
        {
            //var categoryEntity = new Category { Name = createCategoryVM.CategoryName };
            var categoryEntity = _mapper.Map<CreateCategoryVM, Category>(createCategoryVM);
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
            var updateCategory=_mapper.Map<UpdateCategoryVM, Category>(updateCategoryVM);

            //var existsCategory = await _context.Categories.FindAsync(updateCategoryVM.Id);
            //existsCategory.Name = updateCategoryVM.CategoryName;

            
            _context.Categories.Update(updateCategory);
            await _context.SaveChangesAsync();
            return updateCategory.Id;
        }


    }
}
