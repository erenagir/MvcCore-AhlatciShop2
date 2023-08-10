using Ahlatci.Shop.Aplication.Models.Dtos.Cities;
using Ahlatci.Shop.Aplication.Models.RequestModels.Cities;
using Ahlatci.Shop.Aplication.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.Services.Abstraction
{
    public interface ICityService
    {
        Task<Result<List<CityDto>>> GetAllCities();
        Task<Result<CityDto>> GetCityId(int id);
        Task<Result<int>> CreateCity(CreateCityVM createCityVM);
        Task<Result<bool>> UpdateCity(UpdateCityVM updateCityVM);
        Task<Result<bool>> DeleteCity(int id);
    }
}
