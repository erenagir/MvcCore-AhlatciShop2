using Ahlatci.Shop.Aplication.Models.Dtos.Cities;
using Ahlatci.Shop.Aplication.Models.Dtos.Products;
using Ahlatci.Shop.Aplication.Models.RequestModels.Cities;
using Ahlatci.Shop.Aplication.Models.RequestModels.Products;
using Ahlatci.Shop.Aplication.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.Services.Abstraction
{
    public interface IProductService
    {
        Task<Result<List<ProductDto>>> GetAllProduct();
        Task<Result<ProductDto>> GetProductById(int id);
        Task<Result<int>> CreateProduct(CreatteProductVM creatteProductVM );
        Task<Result<bool>> UpdateProduct(UpdateProductVM updateProductVM);
        Task<Result<bool>> DeleteProduct(int id);
    }
}
