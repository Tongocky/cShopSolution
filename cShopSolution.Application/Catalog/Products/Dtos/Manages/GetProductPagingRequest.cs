using cShopSolution.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace cShopSolution.Application.Catalog.Products.Dtos.Manages
{
    public class GetProductPagingRequest : PagingRequestBase
    {
        public string keyWork { get; set; }

        public string Name { get; set; }

        public List<int> CategoryIds { get; set; }
    }
}
