using cShopSolution.Application.Catalog.Products.Dtos;
using cShopSolution.Application.Catalog.Products.Dtos.Publics;
using cShopSolution.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace cShopSolution.Application.Catalog.Products.InterfaceService
{
    public interface IPublicProductService
    {
        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetProductPagingRequest request);
    }
}
