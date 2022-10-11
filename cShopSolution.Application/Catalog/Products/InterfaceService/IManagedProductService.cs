using cShopSolution.Application.Catalog.Products.Dtos;
using cShopSolution.Application.Catalog.Products.Dtos.Manages;
using cShopSolution.Application.Dtos;
using System;
using System.Collections.Generic;
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

        #region GetList
        Task<List<ProductViewModel>> GetAll();
        Task<PagedResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request);
        Task AddViewCount(int ProductId);
        #endregion
    }
}
