using Ahlatci.Shop.Aplication.Models.Dtos.Category;
using Ahlatci.Shop.Aplication.Models.RequestModels.Categories;
using Ahlatci.Shop.Aplication.Services.Abstraction;
using Ahlatci.Shop.Aplication.Wrapper;
using Ahlatci.Shop.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ahlatci.Shop.Api.Controllers
{
    [ApiController]
    [Route("category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("get")]

        public async Task<Result<List<CategoryDto>>> GetAllCategories()
        {
            var categories =await _categoryService.GetAllCategories();
            return categories;
        }
        [HttpGet("get/{id:int}")]

        public async Task<Result<CategoryDto>> GetCategoryById(int id)
        {
            var categories = await _categoryService.GetCategoryById(new GetCategoryByIdVM { Id= id});
            return categories;
        }
        [HttpPost("create")]

        public async Task<Result<int>> CreateCategory(CreateCategoryVM createCategoryVM)
        {
            var categoryId = await _categoryService.CreateCategory(createCategoryVM);
            return categoryId;
        }
        [HttpPut("update/{id:int}")]

        public async Task<Result<int>> UpdataCategory(UpdateCategoryVM updateCategoryVM)
        {
            var categoryId = await _categoryService.UpdateCategory(updateCategoryVM);
            return categoryId;
        }
        [HttpDelete("delete/{id:int}")]

        public async Task<Result<int>> Deletecategory(int id)
        {
            var categoryId = await _categoryService.DeleteCategory(new DeleteCategoryVM { Id=id});
            return categoryId;
        }

    }
}