using Ahlatci.Shop.Aplication.Models.Dtos.Category;
using Ahlatci.Shop.Aplication.Models.RequestModels.Categories;
using Ahlatci.Shop.Aplication.Models.RequestModels.Cities;
using Ahlatci.Shop.Aplication.Services.Abstraction;
using Ahlatci.Shop.Aplication.Wrapper;
using Ahlatci.Shop.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ahlatci.Shop.Api.Controllers
{
    [ApiController]
    [Route("city")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet("get")]

        public async Task<ActionResult<Result<List<CategoryDto>>>> GetAllCities()
        {
            var cities =await _cityService.GetAllCities();
           return Ok(cities);
        }
        [HttpGet("get/{id:int}")]

        public async Task<ActionResult<Result<CategoryDto>>> GetCategoryById(int id)
        {
            var city = await _cityService.GetCityId(id);
            return Ok(city);
        }
        [HttpPost("create")]

        public async Task<ActionResult<Result<int>>> CreateCity(CreateCityVM createCityVM)
        {
            var cityId = await _cityService.CreateCity(createCityVM);
            return Ok(cityId);
        }
        [HttpPut("update/{id:int}")]

        public async Task<ActionResult<Result<bool>>> UpdataCity(UpdateCityVM updateCityVM)
        {
            var result = await _cityService.UpdateCity(updateCityVM);
            return Ok(result);
        }
        [HttpDelete("delete/{id:int}")]

        public async Task<ActionResult<Result<bool>>> DeleteCity(int id)
        {
            var result = await _cityService.DeleteCity( id);
            return Ok(result);
        }

    }
}