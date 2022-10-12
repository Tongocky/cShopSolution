using cShopSolution.Application.Catalog.Products.Dtos;
using cShopSolution.Application.Catalog.Products.Dtos.Manages;
using cShopSolution.Application.Catalog.Products.Dtos.Publics;
using cShopSolution.ViewModels.Catalog.Products;
using cShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace cShopSolution.Application.Catalog.Products.InterfaceService
{
    public interface IManagedProductService
    {
        #region Create
        Task<int> Create(ProductCreateRequet requet);
        #endregion

        #region Update
        Task<int> Update(ProductUpdateRequest request);

        Task<bool> UpdatePrice(int ProductId, decimal newPeice);
        Task<bool> UpdateStock(int ProductId, int addedQuatity);
        #endregion

        #region Delete
        Task<int> Delete(int ProductId);
        #endregion

        #region Get
        Task<ProductViewModel> GetById(int productId, string LanguageId);
        Task<List<ProductViewModel>> GetAll();
        Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);
        Task AddViewCount(int ProductId);
        #endregion

        Task<int> AddImages(int productId, List<IFormFile> files);

        Task<int> RemoveImages(int imageId);

        Task<int> UpdateImage(int imageId, string caption, bool isDefault);

        Task<List<ProductImageViewModel>> GetListImage(int productId);
    }
}
