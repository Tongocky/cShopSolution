using cShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace cShopSolution.ViewModels.Catalog.Products
{
    public class GetPublicProductPagingRequest : PagingRequestBase
    {
        public int? CategoryId { get; set; }
    }
}
