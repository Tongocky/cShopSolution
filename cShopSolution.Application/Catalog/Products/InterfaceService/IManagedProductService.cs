using cShapSolution.Data.Entities;
using cShopSolution.Application.Catalog.Products.Dtos;
using cShopSolution.Application.Catalog.Products.Dtos.Manages;
using cShopSolution.Application.Catalog.Products.Dtos.Publics;
using cShopSolution.ViewModels.Catalog.ProductImages;
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

        #endregion Create

        #region Update

        Task<int> Update(ProductUpdateRequest request);

        Task<bool> UpdatePrice(int ProductId, decimal newPeice);

        Task<bool> UpdateStock(int ProductId, int addedQuatity);

        #endregion Update

        #region Delete

        Task<int> Delete(int ProductId);

        #endregion Delete

        #region Get

        Task<ProductViewModel> GetById(int productId, string LanguageId);

        Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);

        Task AddViewCount(int ProductId);

        #endregion Get

        Task<int> RemoveImage(int imageId);

        Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request);

        Task<List<ProductImageViewModel>> GetListImage(int productId);

        Task<ProductImageViewModel> GetImageById(int imageId);

        Task<int> AddImage(int productId, ProductImageCreateRequest request);
    }
}