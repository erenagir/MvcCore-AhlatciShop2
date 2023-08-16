using Ahlatci.Shop.Aplication.Models.Dtos.ProductImages;
using Ahlatci.Shop.Aplication.Models.Dtos.Products;
using Ahlatci.Shop.Aplication.Models.RequestModels.ProductImages;
using Ahlatci.Shop.Aplication.Models.RequestModels.Products;
using Ahlatci.Shop.Aplication.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.Services.Abstraction
{
    public interface IProductImageService
    {
        Task<Result<List<ProductImageDto>>> GetAllProductImage(GetAllProductImageByProductVM getAllProductImageByProductVM);
        Task<Result<List<ProductImageWithProductDto>>> GetProductImageWithProduct(GetAllProductImageByProductVM getAllProductImageByProductVM);
        Task<Result<int>> CreateProductImage(CreateProductImageVM createProductImageVM);
        Task<Result<int>> DeleteProductImage(DeleteProductImageVM deleteProductImageVM);
    }
}
