using Ahlatci.Shop.Aplication.Models.Dtos;
using Ahlatci.Shop.Aplication.Services.Abstraction;
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

        [HttpGet("GetAll")]

        public async Task<List<CategoryDto>> GetAllCategories()
        {
            var categories =await _categoryService.GetAllCategories();
            return categories;
        }
    }
}