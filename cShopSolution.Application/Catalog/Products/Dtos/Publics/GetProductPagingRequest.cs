using cShopSolution.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace cShopSolution.Application.Catalog.Products.Dtos.Publics
{
    public class GetProductPagingRequest : PagingRequestBase
    {
        public int? CategoryId { get; set; }
    }
}
