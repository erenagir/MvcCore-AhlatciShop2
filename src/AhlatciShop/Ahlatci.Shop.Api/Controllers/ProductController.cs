using Ahlatci.Shop.Aplication.Models.Dtos.Category;
using Ahlatci.Shop.Aplication.Models.Dtos.Products;
using Ahlatci.Shop.Aplication.Models.RequestModels.Categories;
using Ahlatci.Shop.Aplication.Models.RequestModels.Cities;
using Ahlatci.Shop.Aplication.Models.RequestModels.Products;
using Ahlatci.Shop.Aplication.Services.Abstraction;
using Ahlatci.Shop.Aplication.Validators.Product;
using Ahlatci.Shop.Aplication.Wrapper;
using Ahlatci.Shop.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ahlatci.Shop.Api.Controllers
{
    [ApiController]
    [Route("product")]
    [Authorize(Roles="Admin")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("get")]
        [AllowAnonymous]
        public async Task<ActionResult<Result<List<ProductDto>>>> GetAllProducts()
        {
            var Products =await _productService.GetAllProduct ();
           return Ok(Products);
        }
        [HttpGet("get/{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Result<ProductDto>>> GetProductById(int id)
        {
            var city = await _productService.GetProductById(id);
            return Ok(city);
        }
        [HttpPost("create")]

        public async Task<ActionResult<Result<int>>> CreateProduct(CreatteProductVM creatteProductVM)
        {
            var cityId = await _productService.CreateProduct(creatteProductVM);
            return Ok(cityId);
        }
        [HttpPut("update/{id:int}")]

        public async Task<ActionResult<Result<bool>>> UpdataProduct(UpdateProductVM updateProductVM)
        {
            var result = await _productService.UpdateProduct(updateProductVM);
            return Ok(result);
        }
        [HttpDelete("delete/{id:int}")]

        public async Task<ActionResult<Result<bool>>> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProduct(id);
            return Ok(result);
        }

    }
}