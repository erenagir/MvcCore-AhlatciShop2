using Ahlatci.Shop.Aplication.Exceptions;
using Ahlatci.Shop.Aplication.Models.Dtos;
using Ahlatci.Shop.Aplication.Models.RequestModels;
using Ahlatci.Shop.Aplication.Services.Abstraction;
using Ahlatci.Shop.Aplication.Validators.Category;
using Ahlatci.Shop.Aplication.Wrapper;
using Ahlatci.Shop.Domain.Entities;
using Ahlatci.Shop.Persistence.Context;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Ahlatci.Shop.Aplication.Behaviors;


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

        public async Task<Result<List<CategoryDto>>> GetAllCategories()
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
            var result = new Result<List<CategoryDto>>();
            var categoryDtos = await _context.Categories

                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            result.Data = categoryDtos;
            return result;
        }

        [ValidationBehavior(typeof(GetCategoryByIdValidator))]
        public async Task<Result<CategoryDto>> GetCategoryById(GetCategoryByIdVM getCategoryByIdVM)
        {
            var result = new Result<CategoryDto>();

            var categoryExists = await _context.Categories.AnyAsync(x => x.Id == getCategoryByIdVM.Id);
            if (!categoryExists)
            {
                throw new NotFoundException($"{getCategoryByIdVM.Id} numaralı kadegöri bulunamadı");
            }
            //var categoryEntity = await _context.Categories.FindAsync(id);
            //var categoryDto = new CategoryDto
            //{
            //    Id = id,
            //    Name = categoryEntity.Name
            //};
            //return categoryDto;
            var categoryEntity = await _context.Categories
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == getCategoryByIdVM.Id);
            result.Data = categoryEntity;
            return result;

        }

        [ValidationBehavior(typeof(CreateCategoryValidator))]
        public async Task<Result<int>> CreateCategory(CreateCategoryVM createCategoryVM)
        {
            var result = new Result<int>();
            //var categoryEntity = new Category { Name = createCategoryVM.CategoryName };
            var categoryEntity = _mapper.Map<CreateCategoryVM, Category>(createCategoryVM);
            await _context.Categories.AddAsync(categoryEntity);
            await _context.SaveChangesAsync();
            result.Data = categoryEntity.Id;
            return result;
        }


        [ValidationBehavior(typeof(DeleteCategoryValidator))]
        public async Task<Result<int>> DeleteCategory(DeleteCategoryVM deleteCategoryVM)
        {
            var result = new Result<int>();
            var categoryExists = await _context.Categories.AnyAsync(x => x.Id == deleteCategoryVM.Id);
            if (!categoryExists)
            {
                throw new Exception($"{deleteCategoryVM.Id} numaralı kadegöri bulunamadı");
            }
            var existsCategory = await _context.Categories.FindAsync(deleteCategoryVM.Id);
            existsCategory.IsDeleted = true;
            _context.Categories.Update(existsCategory);
            await _context.SaveChangesAsync();
            result.Data = existsCategory.Id;
            return result;
        }



        [ValidationBehavior(typeof(UpdateCategoryvalidator))]
        public async Task<Result<int>> UpdateCategory(UpdateCategoryVM updateCategoryVM)
        {
            var result = new Result<int>();
            var categoryExists = await _context.Categories.AnyAsync(x => x.Id == updateCategoryVM.Id);
            if (!categoryExists)
            {
                throw new Exception($"{updateCategoryVM.Id} numaralı kadegöri bulunamadı");
            }

            var updatedCategory = _mapper.Map<UpdateCategoryVM, Category>(updateCategoryVM);

            //var existsCategory = await _context.Categories.FindAsync(updateCategoryVM.Id);
            //existsCategory.Name = updateCategoryVM.CategoryName;


            _context.Categories.Update(updatedCategory);
            await _context.SaveChangesAsync();
            result.Data = updatedCategory.Id;
            return result;
        }




    }
}
