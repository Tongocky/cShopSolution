using cShapSolution.Data.Entities;
using cShapSolution.Data.EntitiesFramewor;
using cShopSolution.Application.Catalog.Products.Dtos;
using cShopSolution.Application.Catalog.Products.Dtos.Manages;
using cShopSolution.Application.Catalog.Products.InterfaceService;
using cShopSolution.Application.Dtos;
using cShopSolution.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cShopSolution.Application.Catalog.Products.ClassService
{
    public class ManagedProducService : IManagedProductService
    {
        private readonly cShopDbContext _context;

        public ManagedProducService(cShopDbContext context)
        {
            _context = context;

        }

        public async Task AddViewCount(int ProductId)
        {
            var product = await _context.Products.FindAsync(ProductId);
            product.ViewCount += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Create(ProductCreateRequet requet)
        {
            var product = new Product()
            {
                Price = requet.Price,
                OriginalPrice = requet.OriginalPrice,
                Stock = requet.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name = requet.Name,
                        Description = requet.Description,
                        Details = requet.Details,
                        SeoDescription = requet.SeoDescription,
                        SeoAlias = requet.SeoAlias,
                        SeoTitle = requet.SeoTitle,
                        LanguageId = requet.LanguageId
                    }
                }

            };
            _context.Products.Add(product);
            return await _context.SaveChangesAsync();
        }


        public async Task<int> Delete(int ProductId)
        {
            var product = await _context.Products.FindAsync(ProductId);
            if (product == null)
            {
                throw new CShopException($"Can not a product: {ProductId}");
            }
            else
            {
                _context.Products.Remove(product);
                return await _context.SaveChangesAsync();
            }

        }

        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request)
        {
            // 1 select
            var querry = from p in _context.Products
                         join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                         join pic in _context.productInCategories on p.Id equals pic.ProductId
                         join c in _context.Categories on pic.CategoryId equals c.Id
                         select new { p, pt, pic };

            // 2 filter
            if (string.IsNullOrEmpty(request.keyWork))
                querry = querry.Where(x => x.pt.Name.Contains(request.keyWork));

            if (request.CategoryIds.Count > 0)
            {
                querry = querry.Where(p => request.CategoryIds.Contains(p.pic.CategoryId));
            }

            // 3 Paging
            int totalRow = await querry.CountAsync();
            var datapage = await querry.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount
                }).ToListAsync();

            //4 Select and projection
            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = datapage
            }; return pagedResult;
        }


        public Task<List<ProductViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }



        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            var productTranslation = await _context.ProductTranslations.
                FirstOrDefaultAsync(x => x.ProductId == request.Id && x.LanguageId == request.LanguageId);

            if (product == null || productTranslation == null)
            {
                throw new CShopException($" Cannot find a product with id: {request.Id} ");

            }
            productTranslation.Name = request.Name;
            productTranslation.SeoAlias = request.SeoAlias;
            productTranslation.SeoDescription = request.SeoDescription;
            productTranslation.SeoTitle = request.SeoTitle;
            productTranslation.Description = request.Description;
            productTranslation.Details = request.Details;

            #region Save image
            //if (request.ThumbnailImage != null)
            //{
            //    var thumbnailImage = await _context.ProductImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.ProductId == request.Id);
            //    if (thumbnailImage != null)
            //    {
            //        thumbnailImage.FileSize = request.ThumbnailImage.Length;
            //        thumbnailImage.ImagePath = await this.SaveFile(request.ThumbnailImage);
            //        _context.ProductImages.Update(thumbnailImage);
            //    }
            //}
            #endregion

            return await _context.SaveChangesAsync();


        }

        public async Task<bool> UpdatePrice(int ProductId, decimal newPeice)
        {
            var product = await _context.Products.FindAsync(ProductId);

            if (product == null) throw new CShopException($"Cannot find a product with id: {ProductId}");
            product.Price = newPeice;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int ProductId, int addedQuatity)
        {

            var product = await _context.Products.FindAsync(ProductId);
            if (product == null) throw new CShopException($"Cannot find a product with id: {ProductId}");
            product.Stock += addedQuatity;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
