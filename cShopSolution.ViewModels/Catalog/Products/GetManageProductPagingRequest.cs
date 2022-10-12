
using cShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace cShopSolution.Application.Catalog.Products.Dtos.Publics
{
    public class GetManageProductPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }

        public List<int> CategoryIds { get; set; }
    }
}
