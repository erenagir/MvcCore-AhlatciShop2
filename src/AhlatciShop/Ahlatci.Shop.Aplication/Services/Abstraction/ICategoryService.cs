using Ahlatci.Shop.Aplication.Models.Dtos;
using Ahlatci.Shop.Aplication.Models.RequestModels;
using Ahlatci.Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.Services.Abstraction
{
    public interface ICategoryService
    {// dto servisin dışarıya gönderdiği bilgiler
     // VM sevisin dışardan aldığı modeller
        #region Select


        Task<List<CategoryDto>> GetAllCategories();
        Task<CategoryDto> GetCategoryById(GetCategoryByIdVM getCategoryByIdVM);

        #endregion
        #region Insert, update , Delete
        Task<int> CreateCategory(CreateCategoryVM createCategoryVM );
        Task<int> UpdateCategory(UpdateCategoryVM updateCategoryVM);
        Task<int> DeleteCategory(DeleteCategoryVM deleteCategoryVM);
        #endregion
    }
}
