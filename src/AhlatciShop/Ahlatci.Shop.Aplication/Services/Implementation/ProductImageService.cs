using Ahlatci.Shop.Aplication.Behaviors;
using Ahlatci.Shop.Aplication.Exceptions;
using Ahlatci.Shop.Aplication.Models.Dtos.ProductImages;
using Ahlatci.Shop.Aplication.Models.RequestModels.ProductImages;
using Ahlatci.Shop.Aplication.Services.Abstraction;
using Ahlatci.Shop.Aplication.Validators.ProductImages;
using Ahlatci.Shop.Aplication.Wrapper;
using Ahlatci.Shop.Domain.Entities;
using Ahlatci.Shop.Domain.UWork;
using Ahlatci.Shop.Utils;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Ahlatci.Shop.Aplication.Services.Implementation
{

    public class ProductImageService : IProductImageService
    {
        private readonly IUWork _uWork;
        
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnviroment;
        private readonly IConfiguration _configuration;

        public ProductImageService(IUWork uWork, IMapper mapper, IHostingEnvironment hostingEnviroment, IConfiguration configuration)
        {
            _uWork = uWork;
            _mapper = mapper;
            _hostingEnviroment = hostingEnviroment;
            _configuration = configuration;
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
        public async Task<Result<int>> CreateProductImage(CreateProductImageVM createProductImageVM)
        {
            var productExists = await _uWork.GetRepository<Product>().AnyAsync(x => x.Id == createProductImageVM.ProductId);
            if (productExists)
            {
                throw new NotFoundException($"{createProductImageVM.ProductId} kimlik numaralı eleman bulunamadı");
            }
            var result= new Result<int>();
           
            // dosyanınn ismi belirleniyor
            var fileName = PathUtils.GenerateFileName(createProductImageVM.UploadedImage);
            var FilePath = Path.Combine(_hostingEnviroment.WebRootPath, _configuration["Paths:ProductImages"], fileName);

            // resmin fiziksel olarak silinmesi
            using(FileStream fs=new FileStream(FilePath,FileMode.Create))
            {
                createProductImageVM.UploadedImage.CopyTo(fs);
                fs.Close();

            }

            // dosyaya aid bilgilerin db ye yazılması
            var productImageEntity = _mapper.Map<ProductImage >(createProductImageVM);
            productImageEntity.Path= Path.Combine(_configuration["Paths:ProductImages"], fileName);
            _uWork.GetRepository<ProductImage>().Add(productImageEntity);
            await _uWork.ComitAsync();

            result.Data=productImageEntity.Id; 
            return result;

        }

       
        
        [ValidationBehavior(typeof(DeleteProductImageValidator))]
        public async Task<Result<int>> DeleteProductImage(DeleteProductImageVM deleteProductImageVM)
        {
           var result= new Result<int>();
            var existsProdutImage =await _uWork.GetRepository<ProductImage>().GetById(deleteProductImageVM.Id);
            if (existsProdutImage is null)
            {
                throw new NotFoundException("silinecek ürün bulunamadı");
            }
            
            // db silme
            _uWork.GetRepository<ProductImage>().Delete(existsProdutImage);
           await _uWork.ComitAsync();


            //fiziksel dosya silme
            var filePath = Path.Combine(_hostingEnviroment.WebRootPath, existsProdutImage.Path);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }


            result.Data= existsProdutImage.Id;
            return result;
        }

    }
}
