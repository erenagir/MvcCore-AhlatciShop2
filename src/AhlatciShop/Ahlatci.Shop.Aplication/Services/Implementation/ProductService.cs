using Ahlatci.Shop.Aplication.Behaviors;
using Ahlatci.Shop.Aplication.Exceptions;
using Ahlatci.Shop.Aplication.Models.Dtos.Products;
using Ahlatci.Shop.Aplication.Models.RequestModels.Products;
using Ahlatci.Shop.Aplication.Services.Abstraction;
using Ahlatci.Shop.Aplication.Validators.Product;
using Ahlatci.Shop.Aplication.Wrapper;
using Ahlatci.Shop.Domain.Entities;
using Ahlatci.Shop.Domain.UWork;
using Ahlatci.Shop.Persistence.Migrations;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IUWork _uwork;

        public ProductService(IUWork uwork, IMapper mapper)
        {
            _uwork = uwork;
            _mapper = mapper;
        }
        public async Task<Result<List<ProductDto>>> GetAllProduct()
        {
            var result = new Result<List<ProductDto>>();
            var productEntity = await _uwork.GetRepository<Product>().GetAllAsync("Category");
            var productDtos = productEntity.ProjectTo<ProductDto>(_mapper.ConfigurationProvider).ToList();
            result.Data = productDtos;
            return result;
        }

        public async Task<Result<ProductDto>> GetProductById(int id)
        {
            var result = new Result<ProductDto>();
            var productEntity = await _uwork.GetRepository<Product>().GetSingleByFilterAsync(x => x.Id == id, "Category");
            var productdto = _mapper.Map<ProductDto>(productEntity);
            result.Data = productdto;
            return result;
        }

       [ValidationBehavior(typeof(CreateProductValidator))]
        public async Task<Result<int>> CreateProduct(CreatteProductVM creatteProductVM)
        {
            var result = new Result<int>();
            var existsCategory = _uwork.GetRepository<Category>().GetById(creatteProductVM.CategoryId);
            if (existsCategory is null)
            {
                throw new NotFoundException($"{creatteProductVM.CategoryId} id li categori bulunamadı");
            }
            var productEntity = _mapper.Map<Product>(creatteProductVM);
            _uwork.GetRepository<Product>().Add(productEntity);
            await _uwork.ComitAsync();
            result.Data = productEntity.Id;


            return result;
        }
        [ValidationBehavior(typeof(UpdateProductValidator))]
        public async Task<Result<bool>> UpdateProduct(UpdateProductVM updateProductVM)
        {
            var existsProduct =await _uwork.GetRepository<Product>().GetById(updateProductVM.Id);
            if (existsProduct is null)
            {
                throw new NotFoundException($"{updateProductVM.Id} numaralı ürün bulunamadı ");
            }
            var existscategory = await _uwork.GetRepository<Category>().GetById(updateProductVM.CategoryId);
            if (existscategory==null)
            {
                throw new NotFoundException($"{updateProductVM.CategoryId} numaralı kategory bulunamadı ");

            }
            var result=new Result<bool>();
           var updatedProduct= _mapper.Map(updateProductVM, existsProduct);
            _uwork.GetRepository<Product>().Update(updatedProduct);
           result.Data= await _uwork.ComitAsync();
            return result;
        }
        public async Task<Result<bool>> DeleteProduct(int id)
        {
            var existsProduct =await _uwork.GetRepository<Product>().AnyAsync(x=>x.Id==id);
            if(!existsProduct)
            {
                throw new NotFoundException($"{id} numaralı ürün bulunamadı");
            }
            var result=new Result<bool>();
            _uwork.GetRepository<Product>().Delete(id);
            result.Data=await _uwork.ComitAsync();
            return result;
        }



    }
}
