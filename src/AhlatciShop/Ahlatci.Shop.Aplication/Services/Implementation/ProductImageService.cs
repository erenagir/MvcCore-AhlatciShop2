using Ahlatci.Shop.Aplication.Behaviors;
using Ahlatci.Shop.Aplication.Models.Dtos.ProductImages;
using Ahlatci.Shop.Aplication.Models.RequestModels.ProductImages;
using Ahlatci.Shop.Aplication.Services.Abstraction;
using Ahlatci.Shop.Aplication.Validators.ProductImages;
using Ahlatci.Shop.Aplication.Wrapper;
using Ahlatci.Shop.Domain.Entities;
using Ahlatci.Shop.Domain.UWork;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Ahlatci.Shop.Aplication.Services.Implementation
{

    public class ProductImageService : IProductImageService
    {
        private readonly IUWork _uWork;
        
        private readonly IMapper _mapper;
       
        public ProductImageService(IUWork uWork, IMapper mapper)
        {
            _uWork = uWork;
            _mapper = mapper;
        }
       
        
        [ValidationBehavior(typeof(GetProductImageValidator))]         
        public async Task<Result<List<ProductImageDto>>> GetAllProductImage(GetAllProductImageByProductVM getAllProductImageByProductVM)
        {
            var result = new Result<List<ProductImageDto>>();

            var productImageEntites = await _uWork.GetRepository<ProductImage>().GetByFilterAsync(x => x.ProductId == getAllProductImageByProductVM.ProductId);
            var productImageDtos = await productImageEntites.ProjectTo<ProductImageDto>(_mapper.ConfigurationProvider).ToListAsync();
            result.Data = productImageDtos;
            return result;
        }
        
        
        [ValidationBehavior(typeof(GetProductImageValidator))]
        public async Task<Result<List<ProductImageWithProductDto>>> GetProductImageWithProduct(GetAllProductImageByProductVM getAllProductImageByProductVM)
        {
            var result = new Result<List<ProductImageWithProductDto>>();

            var productImageEntites = await _uWork.GetRepository<ProductImage>().GetByFilterAsync(x => x.ProductId == getAllProductImageByProductVM.ProductId);
            var productImageDtos = await productImageEntites.ProjectTo<ProductImageWithProductDto>(_mapper.ConfigurationProvider).ToListAsync();
            result.Data = productImageDtos;
            return result;
        }


        [ValidationBehavior(typeof(CreateProductImageValidator))]
        public Task<Result<int>> CreateProductImage(CreateProductImageVM createProductImageVM)
        {
            throw new NotImplementedException();
        }

       
        
        [ValidationBehavior(typeof(DeleteProductImageValidator))]
        public Task<Result<int>> DeleteProductImage(DeleteProductImageVM deleteProductImageVM)
        {
            throw new NotImplementedException();
        }

    }
}
