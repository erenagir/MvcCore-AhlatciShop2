using Ahlatci.Shop.Aplication.Models.Dtos.Category;
using Ahlatci.Shop.Aplication.Models.Dtos.Products;
using Ahlatci.Shop.Aplication.Models.RequestModels.Categories;
using Ahlatci.Shop.Aplication.Models.RequestModels.Cities;
using Ahlatci.Shop.Aplication.Models.RequestModels.ProductImages;
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
    [Route("productImage")]
    [Authorize]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageService _productImageService;

        public ProductImageController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        [HttpGet("Get/{id:int}")]
        public async Task<ActionResult<Result<List<int>>>> GetProduct›mage(int id )
        {
            var result = await _productImageService.GetAllProductImage(new GetAllProductImageByProductVM { ProductId=id});
            return Ok(result);
        }
        [HttpGet("GetWithProduct/{id:int}")]
        public async Task<ActionResult<Result<int>>> GetProduct›mageWithProduct(int id )
        {
            var result = await _productImageService.GetProductImageWithProduct(new GetAllProductImageByProductVM { ProductId = id });
            return Ok(result);
        }


        [HttpPost("create")]
        public async Task<ActionResult<Result<int>>> CreateProductImage([FromForm]CreateProductImageVM createProductImageVM)
        {
            var result = await _productImageService.CreateProductImage(createProductImageVM);
            return Ok(result);
        }
        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult<Result<int>>> DeleteProductImage(int id )
        {
            var result = await _productImageService.DeleteProductImage(new DeleteProductImageVM { Id = id }) ;
            return Ok(result);
        }


    }
}