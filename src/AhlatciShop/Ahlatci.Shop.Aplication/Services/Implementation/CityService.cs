using Ahlatci.Shop.Aplication.Behaviors;
using Ahlatci.Shop.Aplication.Exceptions;
using Ahlatci.Shop.Aplication.Models.Dtos.Cities;
using Ahlatci.Shop.Aplication.Models.RequestModels.Cities;
using Ahlatci.Shop.Aplication.Services.Abstraction;
using Ahlatci.Shop.Aplication.Validators.Category;
using Ahlatci.Shop.Aplication.Wrapper;
using Ahlatci.Shop.Domain.Entities;
using Ahlatci.Shop.Domain.UWork;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.Services.Implementation
{
    public class CityService : ICityService
    {
        private readonly IUWork _uWork;
        private readonly IMapper _mapper;
        public CityService(IUWork uWork, IMapper mapper)
        {
            _uWork = uWork;
            _mapper = mapper;
        }





        [ValidationBehavior(typeof(CreateCityValidator))]
        public async Task<Result<List<CityDto>>> GetAllCities()
        {

            var result = new Result<List<CityDto>>();
            var citiesEntity = await _uWork.GetRepository<City>().GetAllAsync();
            var citiesDtos = citiesEntity.ProjectTo<CityDto>(_mapper.ConfigurationProvider).ToList();
            result.Data = citiesDtos;
            return result;

        }

        public async Task<Result<CityDto>> GetCityId(int id)
        {
            var existsCity = await _uWork.GetRepository<City>().GetById(id);

            var result = new Result<CityDto>();
            var cityDto = _mapper.Map<CityDto>(existsCity);
            result.Data = cityDto;
            return result;
        }


        [ValidationBehavior(typeof(CreateCityValidator))]
        public async Task<Result<int>> CreateCity(CreateCityVM createCityVM)
        {
            var existsCity = await _uWork.GetRepository<City>().AnyAsync(x => x.Name == createCityVM.Name);
            if (existsCity)
            {
                throw new AlreadyExistsException("şehir  önceden eklenmiştir");
            }
            var result = new Result<int>();
            var cityEntity = _mapper.Map<City>(createCityVM);
            _uWork.GetRepository<City>().Add(cityEntity);
            await _uWork.ComitAsync();
            result.Data = cityEntity.Id;
            return result;
        }



        public async Task<Result<bool>> DeleteCity(int id)
        {
            var existsCity = _uWork.GetRepository<City>().GetById(id);
            if (existsCity is null)
            {
                throw new NotFoundException($"{id} id numaralı şehir bulunmamaktadır");
            }
            var result = new Result<bool>();
            _uWork.GetRepository<City>().Delete(id);
            result.Data = await _uWork.ComitAsync();
            return result;
        }

        public async Task<Result<bool>> UpdateCity(UpdateCityVM updateCityVM)
        {
            var existsCity = await _uWork.GetRepository<City>().GetById(updateCityVM.Id);
            if (existsCity is null)
            {
                throw new NotFoundException($"{updateCityVM.Id} id numaralı şehir bulunmamaktadır");
            }
            var cityNameExists=_uWork.GetRepository<City>().AnyAsync(x=>x.Id==updateCityVM.Id && x.Name == updateCityVM.Name.ToUpper());
            if (cityNameExists is null)
            {
                throw new AlreadyExistsException($"{updateCityVM.Name} isminde şehir bulunmaktadır");
            }
            var result = new Result<bool>();
            _mapper.Map(updateCityVM, existsCity);
            _uWork.GetRepository<City>().Update(existsCity);
            result.Data = await _uWork.ComitAsync();

            return result;
        }
    }
}
