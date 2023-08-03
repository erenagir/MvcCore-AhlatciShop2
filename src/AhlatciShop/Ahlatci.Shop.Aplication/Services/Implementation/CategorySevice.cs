using Ahlatci.Shop.Aplication.Exceptions;
using Ahlatci.Shop.Aplication.Services.Abstraction;
using Ahlatci.Shop.Aplication.Validators.Category;
using Ahlatci.Shop.Aplication.Wrapper;
using Ahlatci.Shop.Domain.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Ahlatci.Shop.Aplication.Behaviors;
using Ahlatci.Shop.Domain.Repositories;
using Ahlatci.Shop.Domain.UWork;
using Ahlatci.Shop.Aplication.Models.Dtos.Category;
using Ahlatci.Shop.Aplication.Models.RequestModels.Categories;

namespace Ahlatci.Shop.Aplication.Services.Implementation
{
    public class CategorySevice : ICategoryService
    {

        private readonly IMapper _mapper;
        private readonly IUWork _UnitWork;


        public CategorySevice(IMapper mapper, IUWork work)
        {

            _mapper = mapper;
            _UnitWork = work;
        }
        [PerformanceBehavior]
        public async Task<Result<List<CategoryDto>>> GetAllCategories()
        {
            var result = new Result<List<CategoryDto>>();
            var categoryEntites = await _UnitWork.GetRepository<Category>().GetAllAsync();
            var categoryDtos =await categoryEntites.ProjectTo<CategoryDto>(_mapper.ConfigurationProvider).ToListAsync();
            //var categoryDtos = _mapper.Map<List<CategoryDto>>(categoryEntites);
            result.Data = categoryDtos;
            return result;
        }

        [ValidationBehavior(typeof(GetCategoryByIdValidator))]
        public async Task<Result<CategoryDto>> GetCategoryById(GetCategoryByIdVM getCategoryByIdVM)
        {
            var result = new Result<CategoryDto>();

            var categoryExists = await _UnitWork.GetRepository<Category>().AnyAsync(x => x.Id == getCategoryByIdVM.Id);
            if (!categoryExists)
            {
                throw new NotFoundException($"{getCategoryByIdVM.Id} numaralı kadegöri bulunamadı");
            }
            
            var categoryEntity=await _UnitWork.GetRepository<Category>().GetById(getCategoryByIdVM.Id);
            var categoryDto=  _mapper.Map<CategoryDto>(categoryEntity);
            //var categoryEntity = await _context.Categories
            //    .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            //    .FirstOrDefaultAsync(x => x.Id == getCategoryByIdVM.Id);
            result.Data = categoryDto;
            return result;

        }

        [ValidationBehavior(typeof(CreateCategoryValidator))]
        public async Task<Result<int>> CreateCategory(CreateCategoryVM createCategoryVM)
        {
            var result = new Result<int>();
            
            var categoryEntity = _mapper.Map<CreateCategoryVM, Category>(createCategoryVM);
            await _UnitWork.GetRepository<Category>().add(categoryEntity);

           await _UnitWork.ComitAsync();
            result.Data = categoryEntity.Id;
            return result;
        }


        [ValidationBehavior(typeof(DeleteCategoryValidator))]
        public async Task<Result<int>> DeleteCategory(DeleteCategoryVM deleteCategoryVM)
        {
            var result = new Result<int>();
            var categoryExists = await _UnitWork.GetRepository<Category>().AnyAsync(x => x.Id == deleteCategoryVM.Id);
            if (!categoryExists)
            {
                throw new Exception($"{deleteCategoryVM.Id} numaralı kadegöri bulunamadı");
            }
            await _UnitWork.GetRepository<Category>().delete(deleteCategoryVM.Id);
            // var existsCategory = await _repository.GetById(deleteCategoryVM.Id);
            await _UnitWork.ComitAsync();

            result.Data = deleteCategoryVM.Id;
            return result;
        }



        [ValidationBehavior(typeof(UpdateCategoryvalidator))]
        public async Task<Result<int>> UpdateCategory(UpdateCategoryVM updateCategoryVM)
        {
            var result = new Result<int>();
            var categoryExists = await _UnitWork.GetRepository<Category>().AnyAsync(x => x.Id == updateCategoryVM.Id);
            if (!categoryExists)
            {
                throw new Exception($"{updateCategoryVM.Id} numaralı kadegöri bulunamadı");
            }

            var updatedCategory = _mapper.Map<UpdateCategoryVM, Category>(updateCategoryVM);

            //var existsCategory = await _context.Categories.FindAsync(updateCategoryVM.Id);
            //existsCategory.Name = updateCategoryVM.CategoryName;


            await _UnitWork.GetRepository<Category>().update(updatedCategory);
            await _UnitWork.ComitAsync();
            result.Data = updatedCategory.Id;
            return result;
        }




    }
}
