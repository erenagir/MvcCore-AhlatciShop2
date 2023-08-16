using Ahlatci.Shop.Aplication.Models.Dtos.Cities;
using Ahlatci.Shop.Aplication.Models.RequestModels.Cities;
using Ahlatci.Shop.Aplication.Services.Abstraction;
using Ahlatci.Shop.Aplication.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.Services.Implementation
{
    public class CityService : ICityService
    {
        public Task<Result<int>> CreateCity(CreateCityVM createCityVM)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<CityDto>>> GetAllCities()
        {
            throw new NotImplementedException();
        }

        public Task<Result<CityDto>> GetCityId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> UpdateCity(UpdateCityVM updateCityVM)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> UpdateCity(int id)
        {
            throw new NotImplementedException();
        }
    }
}
