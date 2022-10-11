using System;
using System.Collections.Generic;
using System.Text;

namespace cShopSolution.Application.Catalog.Products.Dtos.Manages
{
    public class ProductCreateRequet
    {
        public string Name { get; set; }
        public decimal Price { set; get; }
        public string Description { get; set; }
        public string Details { get; set; } 
        public decimal OriginalPrice { set; get; }
        public int Stock { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }

        public string SeoAlias { get; set; }
        public string LanguageId { set; get; }
        public int ViewCount { set; get; }

    }
}
